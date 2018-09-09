using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class SmoothFollowCamera : MonoBehaviour {

    public Camera targetCamera;
    public Vector3 minOffset;
    public Vector3 maxOffset;

    private Rigidbody targetRigidbody;

    private void Start()
    {
        targetRigidbody = GetComponent<Rigidbody>();
    }

    void Update () {
        float speed = targetRigidbody.velocity.magnitude;

        Vector3 speedOffset = Vector3.Lerp(minOffset, maxOffset, speed / 100f);

        Vector3 offsetWithRotation = Quaternion.LookRotation(transform.forward, transform.up) * speedOffset;


        targetCamera.transform.position = offsetWithRotation + transform.position;
        targetCamera.transform.LookAt(transform, transform.up);
	}
}
