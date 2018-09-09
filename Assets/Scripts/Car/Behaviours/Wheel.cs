using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

    public bool steering;
    public bool powered;
    public bool braking;
    public bool handbraking;

    public Transform mesh;

    private bool isSpinning;
    private float colliderCircumference;

    private WheelCollider wheelCollider;
    private Rigidbody attachedRigidbody;

    public WheelCollider WheelCollider { get { return wheelCollider; } }
    public bool IsSpinning { get { return isSpinning; } }

    private void Awake()
    {
        attachedRigidbody = GetComponentInParent<Rigidbody>();
        wheelCollider = GetComponentInChildren<WheelCollider>();

        colliderCircumference = Mathf.Pow(wheelCollider.radius, 2) * Mathf.PI;  
    }

    private void Update()
    {
        SyncMeshToCollider();
        CheckIsSpinning();
    }

    private void SyncMeshToCollider()
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        mesh.position = position;
        mesh.rotation = rotation;
    }

    private void CheckIsSpinning()
    {
        float speedUnitsPerMinute = attachedRigidbody.velocity.magnitude * 60f;
        float speedAtWheelRpm = wheelCollider.rpm * colliderCircumference;

        float speedDifference = speedAtWheelRpm - speedUnitsPerMinute;

        isSpinning = speedDifference > 0.1f;
    }
}
