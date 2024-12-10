using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource radioAudioSource;

    private void Start()
    {
        radioAudioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        radioAudioSource.Play();
    }

    public void PauseAudio()
    {
        radioAudioSource.Pause();
    }

    // Change to mute later?
    public void StopAudio()
    {
        radioAudioSource.Stop();
    }
}
