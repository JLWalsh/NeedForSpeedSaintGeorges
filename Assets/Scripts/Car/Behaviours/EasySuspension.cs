using UnityEngine;

// Credit goes to Unity Technologies for this script
// Merged with this script taken from: https://forum.unity.com/threads/how-to-make-a-physically-real-stable-car-with-wheelcolliders.50643/

[ExecuteInEditMode]
public class EasySuspension : MonoBehaviour
{
	[Range(0.1f, 20f)]
	[Tooltip("Natural frequency of the suspension springs. Describes bounciness of the suspension.")]
	public float naturalFrequency = 10;

	[Range(0f, 3f)]
	[Tooltip("Damping ratio of the suspension springs. Describes how fast the spring returns back after a bounce. ")]
	public float dampingRatio = 0.8f;

	[Range(-1f, 1f)]
	[Tooltip("The distance along the Y axis the suspension forces application point is offset below the center of mass")]
	public float forceShift = 0.03f;

	[Tooltip("Adjust the length of the suspension springs according to the natural frequency and damping ratio. When off, can cause unrealistic suspension bounce.")]
	public bool setSuspensionDistance = true;

    public SwayBarAxle[] axles;

    Rigidbody m_Rigidbody;

    void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody> ();
    }
    
	void Update () 
    {
		// Work out the stiffness and damper parameters based on the better spring model.
		foreach (WheelCollider wc in GetComponentsInChildren<WheelCollider>()) 
        {
			JointSpring spring = wc.suspensionSpring;

            float sqrtWcSprungMass = Mathf.Sqrt (wc.sprungMass);
            spring.spring = sqrtWcSprungMass * naturalFrequency * sqrtWcSprungMass * naturalFrequency;
            spring.damper = 2f * dampingRatio * Mathf.Sqrt(spring.spring * wc.sprungMass);

			wc.suspensionSpring = spring;

			Vector3 wheelRelativeBody = transform.InverseTransformPoint(wc.transform.position);
            float distance = m_Rigidbody.centerOfMass.y - wheelRelativeBody.y + wc.radius;

			wc.forceAppPointDistance = distance - forceShift;

			// Make sure the spring force at maximum droop is exactly zero
			if (spring.targetPosition > 0 && setSuspensionDistance)
				wc.suspensionDistance = wc.sprungMass * Physics.gravity.magnitude / (spring.targetPosition * spring.spring);
		}
	}

    private void FixedUpdate()
    {
        foreach (SwayBarAxle axle in axles)
        {
            PerformAntiRoll(axle);
        }
    }

    private void PerformAntiRoll(SwayBarAxle axle)
    {
        WheelHit hit = new WheelHit();
        float leftWheelTravel = 1.0f;
        float rightWheelTravel = 1.0f;

        var groundedL = axle.leftWheel.GetGroundHit(out hit);
        if (groundedL)
            leftWheelTravel = (-axle.leftWheel.transform.InverseTransformPoint(hit.point).y - axle.leftWheel.radius) / axle.leftWheel.suspensionDistance;

        var groundedR = axle.rightWheel.GetGroundHit(out hit);
        if (groundedR)
            rightWheelTravel = (-axle.rightWheel.transform.InverseTransformPoint(hit.point).y - axle.rightWheel.radius) / axle.rightWheel.suspensionDistance;

        var antiRollForce = (leftWheelTravel - rightWheelTravel) * axle.antiRoll;

        if (groundedL)
            m_Rigidbody.AddForceAtPosition(axle.leftWheel.transform.up * -antiRollForce,
                   axle.leftWheel.transform.position);
        if (groundedR)
            m_Rigidbody.AddForceAtPosition(axle.rightWheel.transform.up * antiRollForce,
                   axle.rightWheel.transform.position);
    }

    [System.Serializable]
    public class SwayBarAxle
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;

        public float antiRoll;
    }
}
