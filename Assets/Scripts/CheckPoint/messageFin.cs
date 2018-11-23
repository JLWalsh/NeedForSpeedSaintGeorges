using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class messageFin : MonoBehaviour {
    public float nombreSecondes;
    public TextMeshProUGUI messageCourse;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        nombreSecondes -= Time.deltaTime;
        if (nombreSecondes <= 0)
        {
            Destroy(gameObject); 
        }
	}
}
