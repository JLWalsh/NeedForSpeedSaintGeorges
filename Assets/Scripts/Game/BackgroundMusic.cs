using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class BackgroundMusic : MonoBehaviour {

    public AudioClip[] songs;

    private AudioSource audioSource;
    private int currentSong;

    private void Start() {
        BackgroundMusic[] backgroundMusics = FindObjectsOfType<BackgroundMusic>();

        if(backgroundMusics.Length > 1) {
            Destroy(this);
        } else {
            DontDestroyOnLoad(this);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        currentSong = Random.Range(0, songs.Length);
    }

    private void Update() {
        if (songs.Length == 0 || audioSource.isPlaying)
            return;

        currentSong++;

        if(currentSong >= songs.Length) {
            currentSong = 0;
        }

        AudioClip song = songs[currentSong];

        audioSource.clip = song;
        audioSource.Play();
    }
}
