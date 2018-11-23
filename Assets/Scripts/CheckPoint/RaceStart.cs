using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStart : MonoBehaviour {

    public Race race;
    public Race previousRace;
    public TextMesh texte;
    
    private Transform player;
    private Rigidbody playerRigidbody;
    private bool isVisible;

    private void Start()
    {
        isVisible = CanStartRace();
        SetVisibility();
        texte.text = race.raceName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<Transform>();
            playerRigidbody = player.GetComponent<Rigidbody>();
        }
        if (other.gameObject.tag == "Player" && CanStartRace())
        {
            player.transform.position = transform.position;
            player.transform.rotation = transform.rotation;
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;

            race.Begin();
        }
    }

    private void Update()
    {
        if (isVisible != CanStartRace())
        {
            isVisible = CanStartRace();
            SetVisibility();
        }
    }

    private void SetVisibility()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            r.enabled = isVisible;
        }
    }

    private bool CanStartRace()
    {
        if (!previousRace) {
            return race.State != Race.RaceState.WON && race.State != Race.RaceState.STARTED;
        }

        return previousRace.State == Race.RaceState.WON && race.State != Race.RaceState.WON && race.State != Race.RaceState.STARTED;
    }

}
