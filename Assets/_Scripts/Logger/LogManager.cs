using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : Singleton<LogManager>
{
	private IDictionary<string, string> _trashDic;

	private void AddToDic(string trash, string trashBin) 
	{
		_trashDic.TryAdd(trash, trashBin);

		foreach (KeyValuePair<string, string> kvp in _trashDic)
		{
			Debug.Log($"Trash: {kvp.Key}, TrashBin: {kvp.Value}");
		}
	}

	#region Unity Methods

	private void Start() 
	{
		_trashDic = new Dictionary<string, string>();
	}

	private void OnEnable()
	{
		TrashBin.s_OnLogEvent += AddToDic;
	}

	private void OnDisable()
	{
		TrashBin.s_OnLogEvent -= AddToDic;
	}

	#endregion
}
