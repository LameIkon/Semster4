using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackerBoard : MonoBehaviour
{

	private IList<TextMeshProUGUI> _trashBoards;
	private IList<string> _trashObjects;

	#region Unity Methods

	private IEnumerator Start()
	{
		_trashBoards = CreateBoardList(GetComponentsInChildren<TextMeshProUGUI>());
		
		yield return null;

		_trashObjects = new List<string>();

		GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("Trash");

		foreach (GameObject trash in trashObjects)
		{
			SOTrashData trashData = trash.GetComponent<ITrashable>().TrashData();

			_trashObjects.Add(trashData.SO_Name);
		}

		yield return null;

		FillBoardsWithObjects();
	}

	private void OnEnable()
	{
		//TrashBin.s_OnTrashedEvent2 += RemoveItemFromList; // Depending on how it should look on the board;
		TrashBin.s_OnTrashedEvent2 += CrossOutFromList;
	}

	private void OnDisable()
	{
		//TrashBin.s_OnTrashedEvent2 -= RemoveItemFromList;
		TrashBin.s_OnTrashedEvent2 -= CrossOutFromList;
	}

	#endregion

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


	private void FillBoardsWithObjects() 
	{
		foreach (TextMeshProUGUI board in _trashBoards)
		{
			board.text = string.Empty;
		}

		for (int i = 0; i < _trashObjects.Count; i++) 
		{
			_trashBoards[i % _trashBoards.Count].text += $"{_trashObjects[i]} \n"; // Implemented as a circular array, it will wrap around and fill out the boards depending on how many there are.
		}

	}

	private void RemoveItemFromList(float points, SOTrashData data) 
	{
		if (_trashObjects.Contains(data.SO_Name)) 
		{
			_trashObjects.Remove(data.SO_Name);
		}
		FillBoardsWithObjects();
	}

	private void CrossOutFromList(float points, SOTrashData data) 
	{
		if (_trashObjects.Contains(data.SO_Name)) 
		{
			for (int i = 0; i < _trashObjects.Count; i++) 
			{
				if (_trashObjects[i] == data.SO_Name) 
				{
					_trashObjects[i] = Format(data.SO_Name);
					break;
				} 	
			}

		}

		FillBoardsWithObjects();

		string Format(string text) 
		{
			return $"<s>{text}</s>";
		}
	
	}

}
