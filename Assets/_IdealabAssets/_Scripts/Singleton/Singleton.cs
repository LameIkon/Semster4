using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
	protected static T S_instance;

	public static bool S_HasInstance
	{
		get { return S_instance != null; }
	}

	public static T TryGetInstance()
	{
		return S_HasInstance ? S_instance : null;
	}

	public static T S_Instance 
	{
		get 
		{
			if (S_instance == null)
			{
				S_instance = FindAnyObjectByType<T>();
				if (S_instance == null) 
				{
					GameObject go = new GameObject(typeof(T).Name);
					S_instance = go.AddComponent<T>();
				}
			}

			return S_instance;
		}
	
	}

	protected virtual void Awake() 
	{
		
	}

	protected virtual void Init() 
	{
		if (!Application.isPlaying) 
		{
			return;
		}

		S_instance = this as T;
	}
}
