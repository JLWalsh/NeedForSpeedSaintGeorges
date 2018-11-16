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
    public float TimeRemaining { get { return maxWinTime - currentTime; } }
    public float RelativeTimeRemaining { get { return 1f - (currentTime / maxWinTime);  } }

    private RaceState state = RaceState.NOT_STARTED;
    private float currentTime;
    private RaceUI raceUI;
    private GameObject player;

    private void Start()
    {
        raceUI = FindObjectOfType<RaceUI>();
        startCheckpoint.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("MainPlayer");
    }

    public void Begin()
    {
        startCheckpoint.gameObject.SetActive(true);
        state = RaceState.STARTED;
        raceUI.RenderFor(this);
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
            RespawnInRace();
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

    private void RespawnInRace()
    {
        CheckpointCheck lastChecked = startCheckpoint.FindLastChecked();

        if (lastChecked)
        {
            player.transform.position = lastChecked.transform.position + Vector3.up * 2f;
            player.transform.rotation = lastChecked.GetRotationTowardsNext();
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
