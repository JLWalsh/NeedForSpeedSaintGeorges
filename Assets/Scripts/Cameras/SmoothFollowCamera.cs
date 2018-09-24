using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class SmoothFollowCamera : MonoBehaviour {

    private enum LookDirection { BEHIND, FORWARD, LEFT, RIGHT };

    public Vector3 minOffset;
    public Vector3 maxOffset;
    public float velocityAtMaxOffset;

    private Rigidbody targetRigidbody;
    private Camera targetCamera;

    private void Start()
    {
        targetCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        targetRigidbody = GetComponent<Rigidbody>();
    }

    void Update () {
        float speed = targetRigidbody.velocity.magnitude;

        Vector3 speedOffset = Vector3.Lerp(minOffset, maxOffset, speed / velocityAtMaxOffset);
        Vector3 offsetWithRotation = GetCameraRotation() * speedOffset;



        targetCamera.transform.position = transform.position + offsetWithRotation;
        targetCamera.transform.LookAt(transform, transform.up);
	}

    private Quaternion GetCameraRotation()
    {
       switch(GetLookDirection())
        {
            case LookDirection.FORWARD:
                return Quaternion.LookRotation(-transform.forward, transform.up);
            case LookDirection.RIGHT:
                return Quaternion.LookRotation(-transform.right, transform.up);
            case LookDirection.LEFT:
                return Quaternion.LookRotation(transform.right, transform.up);
            case LookDirection.BEHIND:
            default:
                return Quaternion.LookRotation(transform.forward, transform.up);
        }
    }

    private LookDirection GetLookDirection()
    {
        bool lookLeft = Input.GetButton("LookLeft");
        bool lookRight = Input.GetButton("LookRight");

        if (lookLeft && lookRight)
            return LookDirection.FORWARD;

        if (lookLeft)
            return LookDirection.LEFT;

        if (lookRight)
            return LookDirection.RIGHT;

        return LookDirection.BEHIND;
    }
}
