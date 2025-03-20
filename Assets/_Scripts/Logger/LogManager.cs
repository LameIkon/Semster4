using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : Singleton<LogManager>
{
	private string _appPath;
	private LogData _data;
	private Logger _logger;


	#region Unity Methods

	private void Start() 
	{
		_appPath = Application.persistentDataPath;
		_data = new LogData();
		_logger = new Logger(_appPath, _data);
	}

	private void OnEnable()
	{
		TrashBin.s_OnLogEvent += Add;
	}

	private void OnDisable()
	{
		TrashBin.s_OnLogEvent -= Add;
	}

	private void Add(string trash, string trashBin) 
	{
		_data.AddToDic(trash, trashBin);
		_logger.Log();
	}

	//private void OnApplicationQuit()
	//{
	//	_logger.Log();
	//}

	#endregion
}
