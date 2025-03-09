using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource[]))]
public class AudioManager : MonoBehaviour
{
    private AudioSource[] _audioSources;

    private void PlaySounds() 
    {
        _audioSources[1].Play();
    
    }

    private void Reset()
    {
        AudioSource[] audioSourcesOnObject = GetComponents<AudioSource>();

        for (int i = audioSourcesOnObject.Length; i < 4; i++)
        {
            gameObject.AddComponent<AudioSource>();
        }
    }
}
