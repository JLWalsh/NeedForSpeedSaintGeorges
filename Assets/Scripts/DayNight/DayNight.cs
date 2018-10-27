using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {

    public float vitesse;
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, vitesse * Time.deltaTime);
        transform.LookAt(Vector3.zero);
	}
}
