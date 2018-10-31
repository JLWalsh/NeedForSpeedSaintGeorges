using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {

    public Light sun;
    public Vector3 centreDeRotation;
	// Use this for initialization
	void Start ()
    {
        sun = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();
        centreDeRotation = new Vector3(sun.transform.position.x, 0, sun.transform.position.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(centreDeRotation, Vector3.right, 5f * Time.deltaTime);
        transform.LookAt(centreDeRotation);
	}
}
