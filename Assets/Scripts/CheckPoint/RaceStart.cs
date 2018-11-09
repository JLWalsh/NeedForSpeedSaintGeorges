using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStart : MonoBehaviour {

    public Race race;
    public Race previousRace;
    
    private Transform player;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<Transform>();
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && CanStartRace())
        {
            player.transform.position = transform.position;
            player.transform.rotation = transform.rotation;
            playerRigidbody.velocity = Vector3.zero;

            race.Begin();
        }
    }

    private bool CanStartRace()
    {
        if (!previousRace)
            return race.State != Race.RaceState.WON;

        return previousRace.State == Race.RaceState.WON && race.State != Race.RaceState.WON;
    }

}
