using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TrashManager : Singleton<TrashManager>
{

	//TODO: This is just for debugging it should be displayed elseware.
	public float _Points;
	private ISet<GameObject> _trashBins;


	#region UnityMethods
	protected override void Awake() 
	{
		base.Awake();
		InitTrashbins();
	}

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

	#endregion

	private void HandleTrashEvent(GameObject sender, float points) 
	{
		_Points += points;
	}

	private void CompletionEvent()
	{
		Debug.Log("All trash objects have been sorted");
	}

	private void InitTrashbins() 
	{
		_trashBins = new HashSet<GameObject>();
	
	}
}
