using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Logger : ILogable
{
	private readonly string _appPath;
	private readonly LogData _data;
	private const string _appDir = "LoggerData";
	private const string _fileExtention = ".txt";
	private readonly string _id;

	public Logger(string appPath, LogData logData) 
	{
		_appPath = appPath;
		_data = logData;
		_id = _data.CreatedTime.ToString("yyyyMMddHHmmss");
	}

	public void Log()
	{
		WriteLog(_data.GetLastEntry());
	}

	public void GameStarted() 
	{
		_data.StartedGame();
	}

	public void GameFinished() 
	{
		_data.StopedGame();
		WriteLog($"Created: {_data.CreatedTime.ToString()}");
		WriteLog($"Started: {_data.StartedTime.ToString()}");
		WriteLog($"Stopped: {_data.StoppedTime.ToString()}");
		WriteLog($"GameTime: {_data.GameTime()}");
		WriteLog($"Tutorial time: {_data.TutorialTime()}");
		WriteLog($"Total points: {_data.TotalPoints}");
	}

	private void WriteLog(string LogMessage) 
	{
		string file = string.Concat(_id, _fileExtention);
		string fullPath = Path.Combine(_appPath, _appDir, file);

		using (StreamWriter writer = new StreamWriter(fullPath, true))
		{
			writer.WriteLine(LogMessage);
		}
	}

	
}
