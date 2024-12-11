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
        if (!_audioSource.isPlaying)
        {
            ChangeSong(Random.Range(0, songs.Length));
        }
        
    }

    public void ChangeSong(int songPicked)
    {
        _audioSource.clip = songs[songPicked];
        _audioSource.Play();
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
