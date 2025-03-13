using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Trash : MonoBehaviour, ITrashable
{

	public SOTrashData _data; // All the data on the object is handle with a Flyweight pattern
	private AudioSource _audioSource;

	public float Trashing(SortingCategory type)
	{
		if (_data == null) throw new NullReferenceException();
		Destroy(gameObject);
		
		if (type == _data.SO_PreferdType)	// Will check the type it gets from the TrashBin to the Preferd and Okay types
		{
			return _data.SO_PreferdTypePoints;
		}
		else if (type == _data.SO_AcceptableType)
		{
			return _data.SO_AcceptableTypePoints;
		}

		return _data.SO_WrongTypePoints;

	}

	public AudioClip TrashingSound() 
	{
		if ( _data == null) throw new NullReferenceException();

		return _data.SO_TrashAudioClip;
	}

	public void PickUpSound()
	{
		if (_data == null) throw new NullReferenceException();

		if (_data.SO_PickUpAudioClip != null) 
		{
			_audioSource.clip = _data.SO_PickUpAudioClip;
			_audioSource.Play();
		}
	}

	#region UnityMethods

	private void Awake() 
	{
		gameObject.tag = "Trash";
	}

    #endregion



}
