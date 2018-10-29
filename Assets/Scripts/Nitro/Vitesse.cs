using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitesse : MonoBehaviour {

    public float vitesseNitro;
    public float durationNitro;
    public float durationRecharge;
    public float nombreUtilisationsMax;

    public int NitroUtilisees { get { return nitroUtilisees; } }
    public float ProgressionRechargeNitro { get { return 1f - (tempsRecharge / durationRecharge); } }

    private float tempsRestant;
    private float tempsRecharge;
    
    private int nitroUtilisees;
    private bool nitroEnable;
    private Rigidbody voiture;

	void Start () {
        nitroEnable = true;
        voiture = GetComponent<Rigidbody>();
	}
	
	void Update () {
		if (Input.GetButton("Nitro") && PeutActiverNitro())
        {
            ActiverNitro();
        }

        if(tempsRestant > 0)
        {
            AppliquerNitro();
        }

        tempsRecharge--;
        tempsRecharge = Mathf.Max(tempsRecharge, 0);
	}

    bool PeutActiverNitro()
    {
        return tempsRecharge == 0 && tempsRestant == 0 && nitroUtilisees < nombreUtilisationsMax;
    }

    void Recharger()
    {
        tempsRecharge = 0;
    }

    void ActiverNitro()
    {
        tempsRestant = durationNitro;
        tempsRecharge = durationRecharge;
        nitroUtilisees++;
    }

    void AppliquerNitro()
    {
        voiture.AddForce(transform.forward * vitesseNitro);
        tempsRestant--;

        tempsRestant = Mathf.Max(tempsRestant, 0);
    }
}
