using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip[] songs;

    public float volume;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        _audioSource.Play();
    }

    public void PauseAudio()
    {
        _audioSource.Pause();
    }

    // Change to mute later?
    public void StopAudio()
    {
        _audioSource.Stop();
    }
    public void ChangeSong(int songPicked)
    {
        _audioSource.clip = songs[songPicked];
        PlayAudio();
    }

    public void Update()
    {
        _audioSource.volume = volume;

        if (!_audioSource.isPlaying)
        {
            ChangeSong(Random.Range(0, songs.Length));
        }
    }
}
