using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race : MonoBehaviour {

    public enum RaceState
    {
        NOT_STARTED,
        STARTED,
        WON,
    }

    public CheckpointCheck startCheckpoint;
    public float maxWinTime;
    public string raceName;

    public RaceState State { get { return state; } }
    public float CurrentTime { get { return currentTime; } }

    private RaceState state = RaceState.NOT_STARTED;
    private float currentTime;
    private GameObject player;

    private void Start()
    {
        startCheckpoint.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("MainPlayer");
    }

    public void Begin()
    {
        startCheckpoint.gameObject.SetActive(true);

        state = RaceState.STARTED;
    }

    public void Reset()
    {
        currentTime = 0;
        state = RaceState.NOT_STARTED;
        startCheckpoint.Reset();
        startCheckpoint.gameObject.SetActive(false);
    }

    void Update () {
        if (state != RaceState.STARTED)
            return;

        currentTime += Time.deltaTime;

        if(Input.GetButton("Respawn"))
        {
            CheckpointCheck lastChecked = startCheckpoint.FindLastChecked();

            if(lastChecked)
            {
                player.transform.position = lastChecked.transform.position + Vector3.up * 2f;
                player.transform.rotation = lastChecked.GetRotationTowardsNext();
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }

        if (!startCheckpoint.IsCourseCompleted())
            return;

        if(currentTime <= maxWinTime)
        {
            state = RaceState.WON;
            // TODO afficher message course reussie
        } else {
            Reset();
            // TODO afficher message course echouee
        }
    }
}
