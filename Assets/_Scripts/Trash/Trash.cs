using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, ITrashable
{

	[SerializeField] private SOTrashData _data; // All the data on the object is handle with a Flyweight pattern

	public float Trashing(SortingCategory type)
	{
		if (_data == null) throw new NullReferenceException();
		Destroy(gameObject);
		
		if (type == _data._PreferdType)	// Will check the type it gets from the TrashBin to the Preferd and Okay types
		{
			return _data._PreferdTypePoints;
		}
		else if (type == _data._AcceptableType)
		{
			return _data._AcceptableTypePoints;
		}

		return _data._WrongTypePoints;

	}

	public AudioClip TrashAudio() 
	{
		if (_data == null) throw new NullReferenceException();

		return _data._TrashAudioClip;
	}
}
