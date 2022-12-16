using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    AudioSource _audioSource;
    bool _songIsPlaying;

    private void Awake()
    {
        _songIsPlaying = false;
    }

    public void PlaySong(AudioClip clip)
    {
        if (!_songIsPlaying)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
            _songIsPlaying = true;
        }
    }
}