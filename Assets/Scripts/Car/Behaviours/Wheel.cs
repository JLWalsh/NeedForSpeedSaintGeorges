using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

    public bool steering;
    public bool powered;
    public bool braking;
    public bool handbraking;

    public bool isSpinning;

    public Transform mesh;

    private WheelCollider wheelCollider;
    private Rigidbody parentRigidbody;
    private float colliderCircumference;

    public WheelCollider WheelCollider { get { return wheelCollider; } }

    private void Awake()
    {
        parentRigidbody = GetComponentInParent<Rigidbody>();
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
        float speedUnitsPerMinute = parentRigidbody.velocity.magnitude * 60f;
        float speedAtWheelRpm = wheelCollider.rpm * colliderCircumference;

        float speedDifference = speedAtWheelRpm - speedUnitsPerMinute;

        isSpinning = speedDifference > 0.1f;
        Debug.Log(isSpinning);
    }
}
