using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour {

    public ParticleSystem etoiles;
    public Light sun;

	// Use this for initialization
	void Start () {
        etoiles.Play();
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, Vector3.right, 5f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        if (sun.transform.eulerAngles.x <= 91)
        {
            etoiles.Stop());
        }
        else if (sun.transform.eulerAngles.x > 91)
        {
            etoiles.Play();
        }
    }

    void AfficherEtoiles()
    {

    }

    void EffacerEtoiles()
    {

    }
}
