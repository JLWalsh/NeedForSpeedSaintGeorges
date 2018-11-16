using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceUI : MonoBehaviour {

    public TextMeshProUGUI timeRemainingText;
    public Color hasTimeColor;
    public Color outOfTimeColor;

    private Race currentRace;

    public void RenderFor(Race race)
    {
        currentRace = race;
    }

    private void Update()
    {
        if(currentRace != null && currentRace.State == Race.RaceState.STARTED)
        {
            timeRemainingText.text = "Temps restant: " + Mathf.FloorToInt(currentRace.TimeRemaining).ToString() + "s";
            timeRemainingText.color = Color.Lerp(outOfTimeColor, hasTimeColor, currentRace.RelativeTimeRemaining);
        } else {
            timeRemainingText.text = "";
        }
    }
}
