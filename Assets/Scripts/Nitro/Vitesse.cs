using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitesse : MonoBehaviour {

    public float vitesseNitro;
    public float tempsRecharge;
    public float tempsRestant;
    public bool nitroEnable;
    public Rigidbody voiture;
	// Use this for initialization
	void Start () {
        vitesseNitro = 20000;
        tempsRecharge = 100;
        tempsRestant = 1000;
        nitroEnable = true;
        voiture.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftShift))
        {
            if (nitroEnable == true)
            {
                UtiliserNitro();
            }
        }
        if (tempsRecharge == 0)
        {
            nitroEnable = false;
            tempsRestant--;
            print(tempsRestant);
            if (tempsRestant == 0)
            {
                nitroEnable = true;
                tempsRecharge = 100;
                tempsRestant = 1000;
            }
        }
	}

    void UtiliserNitro()
    {
       tempsRecharge--;
       print(tempsRecharge);
       voiture.AddForce(transform.forward * vitesseNitro);
    }
}
