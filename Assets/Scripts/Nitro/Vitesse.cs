﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitesse : MonoBehaviour {

    public float vitesseNitro;
    public float durationNitro;
    public float durationRecharge;
    public int nombreUtilisationsMax;

    public int NitroRestantes { get { return nombreUtilisationsMax - nitroUtilisees; } }
    public float ProgressionRechargeNitro { get { return 1f - (tempsRecharge / durationRecharge); } }

    private float tempsRestant;
    private float tempsRecharge;
    
    private int nitroUtilisees;

    public Renderer flamme1;
    public Renderer flamme2;
    private Rigidbody voiture;
    public AudioSource sonNitro;

	void Start () {
        flamme1.enabled = false;
        flamme2.enabled = false;
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
        if (tempsRestant == 0)
        {
            ArreterFlammes();
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
        AfficherFlammes();
        nitroUtilisees++;
    }

    void AppliquerNitro()
    {
        voiture.AddForce(transform.forward * vitesseNitro);
        tempsRestant--;

        tempsRestant = Mathf.Max(tempsRestant, 0);
    }

    void AfficherFlammes()
    {
        flamme1.enabled = true;
        flamme2.enabled = true;
        sonNitro.Play();
    }

    void ArreterFlammes()
    {
            flamme1.enabled = false;
            flamme2.enabled = false;
    }
}
