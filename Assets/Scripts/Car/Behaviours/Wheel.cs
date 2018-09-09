using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

    public bool steering;
    public bool powered;
    public bool braking;
    public bool handbraking;

    public Transform mesh;

    private WheelCollider wheelCollider;

    public WheelCollider WheelCollider { get { return wheelCollider; } }

    private void Awake()
    {
        wheelCollider = GetComponentInChildren<WheelCollider>();
    }

    private void Update()
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        mesh.position = position;
        mesh.rotation = rotation;
    }
}
