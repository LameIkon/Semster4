using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TrashManager : MonoBehaviour
{
	//TODO: This GameObject should be have a Singleton Pattern but I cannot remember how to implement it in Unity right now.


	//TODO: This is just for debugging it should be displayed elseware.
	public float _Points;


	public void OnEnable() 
	{
		TrashBin.OnTrashedEvent += HandleTrashEvent; // Adds the HandleTrashEvent to the Action OnTrashedEvent
		CompletionTracker.OnCompletion += CompletionEvent;
	}

	public void OnDisable()
	{
		TrashBin.OnTrashedEvent -= HandleTrashEvent;
        CompletionTracker.OnCompletion -= CompletionEvent;
    }

	private void HandleTrashEvent(GameObject sender, float points) 
	{
		_Points += points;
	}

	private void CompletionEvent()
	{
		Debug.Log("All trash objects have been sorted");
	}
}
