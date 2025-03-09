using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioMixerGroup _AudioMixer;


    private void HandlePlaySounds(AudioClip clip, Vector3 position) 
    {
        AudioSource.PlayClipAtPoint(clip, position, 1f);
    }


    #region UnityMethods
    private void Start() 
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = _AudioMixer;
    }
    
    private void Reset()
    {

    }

	private void OnEnable()
	{
        TrashBin._OnTrashAudioEvent += HandlePlaySounds;
	}

	private void OnDisable()
	{
		TrashBin._OnTrashAudioEvent -= HandlePlaySounds;
	}

	#endregion
}
