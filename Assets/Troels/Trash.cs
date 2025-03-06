using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, ITrashable
{

	[SerializeField] private SOTrashContainer _container; // All the data on the object is handle with a Flyweight pattern

	public float Trashing(TrashType type)
	{
		if (_container == null) throw new NullReferenceException();
		
		if (type == _container._PreferdType)	// Will check the type it gets from the TrashBin to the Preferd and Okay types
		{
			return _container._PreferdTypePoints;
		}
		else if (type == _container._OkayType)
		{
			return _container._OkayTypePoints;
		}

		return _container._WrongTypePoints;
	}
}
