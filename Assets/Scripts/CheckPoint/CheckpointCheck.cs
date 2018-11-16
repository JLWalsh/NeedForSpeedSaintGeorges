using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointCheck : MonoBehaviour {

    private AudioSource sonCheckpoint;
    public CheckpointCheck nextCheckpoint;
    public CheckpointCheck previousCheckpoint;

    public Transform arrow;
    public Transform checkedFlag;

    private bool isChecked = false;
    private bool visible = true;
    private bool son = false;

    public bool IsChecked { get { return isChecked; } }

    private void Start()
    {
        sonCheckpoint = GetComponentInChildren<AudioSource>();
        UpdateVisibility();

        if (nextCheckpoint != null)
        {
            arrow.LookAt(nextCheckpoint.transform.position);
            Destroy(checkedFlag.gameObject);
        } else if(previousCheckpoint != null) {
            checkedFlag.LookAt(previousCheckpoint.transform.position);
            Destroy(arrow.gameObject);
        }
    }

    public void Reset()
    {
        isChecked = false;

        if(nextCheckpoint != null)
        {
            nextCheckpoint.Reset();
        }
    }

    private void Update()
    {
        if (son == true)
        {
            sonCheckpoint.Play();
            son = false;
        }
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        bool newVisibility = CanRender();

        if (newVisibility != visible && !isChecked)
        {
            SetVisibility(newVisibility);
            visible = newVisibility;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && CanBeChecked())
        {
            isChecked = true;
            son = true;
            SetVisibility(false);
        }
    }

    public bool IsCourseCompleted()
    {
        if (!isChecked)
            return false;

        if (nextCheckpoint != null)
            return nextCheckpoint.IsCourseCompleted();

        return true;
    }

    private bool CanBeChecked()
    {
        if (previousCheckpoint == null)
            return !isChecked;

        return previousCheckpoint.IsChecked && !isChecked;
    }

    private bool CanRender()
    {
        if(previousCheckpoint != null)
        {
            return previousCheckpoint.IsChecked;
        }

        return !isChecked;
    }

    private void SetVisibility(bool visible)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            r.enabled = visible;
        }
    }
}
