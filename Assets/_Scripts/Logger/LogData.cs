using System.Collections.Generic;
using System;

public class LogData
{
	private IDictionary<string, string> _trashDic;
	private IList<string> _logList;
	
	public LogData() 
	{
		_trashDic = new Dictionary<string, string>();
		_logList = new List<string>();
	}


	public void AddToDic(string trash, string trashBin)
	{
		if (_trashDic.TryAdd(trash, trashBin)) 
		{
			_logList.Add( $"Trash: {trash}, TrashBin: {trashBin}");
		}
	}

	public IList<string> GetData() 
	{
		IList<string> data = new List<string>();

		foreach (KeyValuePair<string, string> kvp in _trashDic) 
		{
			data.Add($"Trash: {kvp.Key}, TrashBin: {kvp.Value}");
		}

		return data;
	}

	public string GetLastEntry() 
	{
		return _logList[_logList.Count - 1 ];
	}

}
