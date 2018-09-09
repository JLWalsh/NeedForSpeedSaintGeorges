using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInfo : MonoBehaviour {

    private Rigidbody crigidbody;

    private void Awake()
    {
        crigidbody = GetComponent<Rigidbody>();
    }

    void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(Screen.width / 3, Screen.height / 2, 300, 30), "Speed: " + (crigidbody.velocity.magnitude * 3.6f).ToString() + " km/h");
    }
}
