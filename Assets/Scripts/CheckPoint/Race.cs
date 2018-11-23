using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race : MonoBehaviour {

    public enum RaceState
    {
        NOT_STARTED,
        STARTED,
        WON,
        STARTING,
    }

    public CheckpointCheck startCheckpoint;
    public float maxWinTime;
    public string raceName;
    public float temps;
    public GameObject messagePrefab;

    public RaceState State { get { return state; } }
    public float TimeRemaining { get { return maxWinTime - currentTime; } }
    public float RelativeTimeRemaining { get { return 1f - (currentTime / maxWinTime);  } }

    private RaceState state = RaceState.NOT_STARTED;
    private float currentTime;
    private RaceUI raceUI;
    private GameObject player;
    private float compteurDepart;

    private void Start()
    {      
        raceUI = FindObjectOfType<RaceUI>();
        startCheckpoint.gameObject.SetActive(false);
    }

    public void Begin()
    {
        compteurDepart = temps;
        startCheckpoint.gameObject.SetActive(true);
        state = RaceState.STARTING;
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
        if (state == RaceState.STARTING)
        {
            int tempsRestant = Mathf.FloorToInt(compteurDepart);
            if(tempsRestant == 0)
            {
                raceUI.tempsDepart.text = "GO!";
            } else
            {
                raceUI.tempsDepart.text = tempsRestant.ToString();
            }
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("MainPlayer");
            }
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            compteurDepart -= Time.deltaTime;
            if (compteurDepart <= 0)
            {
                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                raceUI.tempsDepart.text = "";
                state = RaceState.STARTED;
            }
        }
        if (state != RaceState.STARTED)
            return;

        currentTime += Time.deltaTime;

        if(Input.GetButton("Respawn"))
        {
            RespawnInRace();
        }

        if(currentTime <= maxWinTime && startCheckpoint.IsCourseCompleted())
        {
            state = RaceState.WON;
            GameObject Message = Instantiate(messagePrefab);
            messageFin msg = Message.GetComponent<messageFin>();
            msg.nombreSecondes = 3;
            msg.messageCourse.text = "Course reussie!";
        }

        if (currentTime >= maxWinTime && !startCheckpoint.IsCourseCompleted()) {
            Reset();
            GameObject Message = Instantiate(messagePrefab);
            messageFin msg = Message.GetComponent<messageFin>();
            msg.nombreSecondes = 3;
            msg.messageCourse.text = "Course perdue! Reassayez!";
        }
    }

    private void RespawnInRace()
    {
        CheckpointCheck lastChecked = startCheckpoint.FindLastChecked();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("MainPlayer");
        }
        if (lastChecked)
        {
            player.transform.position = lastChecked.transform.position + Vector3.up * 2f;
            player.transform.rotation = lastChecked.GetRotationTowardsNext();
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
