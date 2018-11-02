using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundAudio : MonoBehaviour {

    public AudioClip[] songs;

    private AudioSource audioSource;
    private int currentSong;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentSong = Random.Range(0, songs.Length - 1);

        BackgroundAudio[] backgroundAudios = GameObject.FindObjectsOfType<BackgroundAudio>();
        if(backgroundAudios.Length > 1)
        {
            Destroy(this);
        } else
        {
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        if (audioSource.isPlaying)
            return;

        currentSong++;
        if(currentSong >= songs.Length)
        {
            currentSong = 0;
        }

        audioSource.clip = songs[currentSong];
        audioSource.Play();
    }
}
