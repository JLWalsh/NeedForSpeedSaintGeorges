using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointCheck : MonoBehaviour {

    public CheckpointCheck nextCheckpoint;
    public CheckpointCheck previousCheckpoint;

    public Transform arrow;
    public Transform checkedFlag;

    private bool isChecked = false;

    public bool IsChecked { get { return isChecked; } }

    private void Start()
    {
        if(nextCheckpoint != null)
        {
            arrow.LookAt(nextCheckpoint.transform.position);
            Destroy(checkedFlag.gameObject);
        } else if(previousCheckpoint != null) {
            checkedFlag.LookAt(previousCheckpoint.transform.position);
            Destroy(arrow.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && CanBeChecked())
        {
            isChecked = true;

            HideAllChildren();
        }
    }

    public bool IsRaceCompleted()
    {
        if (!isChecked)
            return false;

        if (nextCheckpoint != null)
            return nextCheckpoint.IsRaceCompleted();

        return true;
    }

    private bool CanBeChecked()
    {
        if (previousCheckpoint == null)
            return true;

        return previousCheckpoint.IsChecked;
    }

    private void HideAllChildren()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
            r.enabled = false; 
    }
}
