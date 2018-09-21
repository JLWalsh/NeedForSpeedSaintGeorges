using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

    public bool steering;
    public bool powered;
    public bool braking;
    public bool handbraking;

    public float handbrakeFrictionMultiplier;

    public Transform mesh;

    private bool isSpinning;
    private bool isLocked;
    private float colliderCircumference;

    private WheelCollider wheelCollider;
    private WheelFrictionCurve originalFrictionCurve;
    private Rigidbody attachedRigidbody;

    public WheelCollider WheelCollider { get { return wheelCollider; } }
    public bool IsSpinning { get { return isSpinning; } }
    public bool IsLocked { get { return isLocked; } }

    public void EnableHandbrakeFriction()
    {
        WheelFrictionCurve newCurve = wheelCollider.sidewaysFriction;
        newCurve.stiffness = originalFrictionCurve.stiffness * handbrakeFrictionMultiplier;

        wheelCollider.sidewaysFriction = newCurve;
    }

    public void DisableHandbrakeFriction()
    {
        wheelCollider.sidewaysFriction = originalFrictionCurve;
    }

    private void Awake()
    {
        attachedRigidbody = GetComponentInParent<Rigidbody>();
        wheelCollider = GetComponentInChildren<WheelCollider>();
        originalFrictionCurve = wheelCollider.sidewaysFriction;

        colliderCircumference = Mathf.Pow(wheelCollider.radius, 2) * Mathf.PI;  
    }

    private void Update()
    {
        SyncMeshToCollider();
        UpdateMovementFlags();
    }

    private void SyncMeshToCollider()
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        mesh.position = position;
        mesh.rotation = rotation;
    }

    private void UpdateMovementFlags()
    {
        float speedUnitsPerMinute = attachedRigidbody.velocity.magnitude * 60f;
        float speedAtWheelRpm = wheelCollider.rpm * colliderCircumference;

        float speedDifference = speedAtWheelRpm - speedUnitsPerMinute;

        isSpinning = speedDifference > 0.1f;
        isLocked = speedAtWheelRpm == 0f && speedUnitsPerMinute > 0.1f;
    }
}
