using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class CompletionTrackerBoard : MonoBehaviour
{
	private TrackerBoards _trackerBoards;
	private CompletionTrackering _completionTracker;

	public static Action s_OnComplete; //Event that triggers when all trash is sorted

	#region Unity Methods

	private void Start()
	{
		GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("Trash");

		_trackerBoards = new TrackerBoards(GetComponentsInChildren<TextMeshProUGUI>(), trashObjects);
		_completionTracker = new CompletionTrackering(trashObjects);
	}

	private void OnEnable()
	{
		//TrashBin.s_OnTrashedEvent2 += HandleRemoveItemFromList; // Depending on how it should look on the board;
		TrashBin.s_OnTrashedEvent2 += HandleCrossOutFromBoard;
	}

	private void OnDisable()
	{
		//TrashBin.s_OnTrashedEvent2 -= HandleRemoveItemFromList;
		TrashBin.s_OnTrashedEvent2 -= HandleCrossOutFromBoard;
	}

	#endregion

	private void HandleRemoveItemFromBoard(float points, SOTrashData data)
	{
		_trackerBoards.RemoveTrash(data);
		PickUpTrash(data);
	}

	private void HandleCrossOutFromBoard(float points, SOTrashData data)
	{
		_trackerBoards.CrossOut(data);
		PickUpTrash(data);
	}

	private void PickUpTrash(SOTrashData data) 
	{
		_completionTracker.PickUpTrash(data);
		IsAllTrashPickedUp();

		void IsAllTrashPickedUp() 
		{
			if (_completionTracker.IsAllTrashPickedUp == true) 
			{
				s_OnComplete?.Invoke();
			}
		}
	}

}


public sealed class TrackerBoards
{
	private readonly IList<TextMeshProUGUI> _boards;
	private IList<string> _trashObjects;

	public TrackerBoards(TextMeshProUGUI[] boards, GameObject[] trashObjects)
	{
		_boards = CreateBoardList(boards);
		_trashObjects = CreateTrashList(trashObjects);
		FillBoards();
	}

	public void CrossOut(SOTrashData toCrossOut)
	{
		if (_trashObjects.Contains(toCrossOut.SO_Name))
		{
			for (int i = 0; i < _trashObjects.Count; i++)
			{
				if (_trashObjects[i] == toCrossOut.SO_Name)
				{
					_trashObjects[i] = Format(toCrossOut.SO_Name);
					break;
				}
			}

		}
		FillBoards();
		return;

		string Format(string text)
		{
			return $"<s>{text}</s>";
		}
	}

	public void RemoveTrash(SOTrashData toTrash) 
	{
		if (_trashObjects.Contains(toTrash.SO_Name)) 
		{
			_trashObjects.Remove(toTrash.SO_Name);
		}
		FillBoards();
	}

	private void FillBoards() 
	{
		foreach (TextMeshProUGUI board in _boards)
		{
			board.text = string.Empty;
		}

		for (int i = 0; i < _trashObjects.Count; i++)
		{
			_boards[i % _boards.Count].text += $"{_trashObjects[i]} \n"; // Implemented as a circular array, it will wrap around and fill out the boards depending on how many there are.
		}

	}

	private IList<TextMeshProUGUI> CreateBoardList(TextMeshProUGUI[] guis)
	{
		IList<TextMeshProUGUI> list = new List<TextMeshProUGUI>();

		foreach (TextMeshProUGUI gui in guis)
		{
			if (gui.text == string.Empty)
			{
				list.Add(gui);
			}
		}

		return list;
	}

	private IList<string> CreateTrashList(GameObject[] trashObjects) 
	{
		IList<string> list = new List<string>();

		foreach (GameObject trash in trashObjects)
		{
			SOTrashData trashData = trash.GetComponent<ITrashable>().TrashData();

			list.Add(trashData.SO_Name);
		}

		return list;
	}
}

public sealed class CompletionTrackering
{
	private ISet<string> _trashSet;

	public CompletionTrackering(GameObject[] trashObjects) 
	{
		_trashSet = CreateTrashSet(trashObjects);
	}

	public bool IsAllTrashPickedUp { get => _trashSet.Count > 0; }
	public int TrashLeft { get => _trashSet.Count; }

	public void PickUpTrash(SOTrashData trash) 
	{
		if (_trashSet.Contains(trash.SO_Name)) 
		{
			_trashSet.Remove(trash.SO_Name);
		}
	}

	private ISet<string> CreateTrashSet(GameObject[] trashObjects) 
	{
		ISet<string> set = new HashSet<string>();

		foreach (GameObject trash in trashObjects) 
		{
			set.Add(trash.GetComponent<ITrashable>().TrashData().SO_Name);
		}

		return set;

	}

}