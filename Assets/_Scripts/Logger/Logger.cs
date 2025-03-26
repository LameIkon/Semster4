using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

public class Logger : ILogable
{
	private readonly string _appPath;
	private readonly LogData _data;
	private readonly string _id;
	private readonly string _logDir; 
	private readonly LogWriter _writer;

	private const string _logs = "LoggerData";

	public Logger(string appPath, LogData logData) 
	{
		_appPath = appPath;
		_data = logData;
		_id = _data.CreatedTime.ToString("yyyyMMddHHmmss");

		string logPath = Path.Combine(_appPath, _logs);
		Directory.CreateDirectory(logPath);

		_logDir = Path.Combine(logPath, _id);
		Directory.CreateDirectory(_logDir);

		_writer = new LogWriter(_logDir);
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
		
		WriteLog("\n------- Game Stats -------");
		WriteLog($"Created:        {_data.CreatedTime.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.GetCultureInfo("da-DK"))}");
		WriteLog($"Started:        {_data.StartedTime.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.GetCultureInfo("da-DK"))}");
		WriteLog($"Stopped:        {_data.StoppedTime.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.GetCultureInfo("da-DK"))}");
		WriteLog($"Game Time:      {_data.GameTime()}");
		WriteLog($"Tutorial Time:  {_data.TutorialTime()}");
		WriteLog($"Total Points:   {_data.TotalPoints}");
	}

	private void WriteLog(string logMessage) 
	{
		_writer.WriteTrashLog(logMessage);
	}
}


public class LogWriter 
{
	private readonly string _logDir;

	private const string _trashLog = "TrashLog";
	private const string _trashExtention = ".csv";
	private const string _timeLog = "TimeLog";
	private const string _timeExtention = ".txt";

	public LogWriter(string logDir) 
	{
		_logDir = logDir;
	}


	public void WriteTrashLog(string logMessage) 
	{
		string trashFile = string.Concat(_trashLog, _trashExtention);
		string fullPath = Path.Combine(_logDir, trashFile);

		using (StreamWriter writer = new StreamWriter(fullPath, true))
		{
			writer.WriteLine(logMessage);
		}
	}

	public void WriteTimeLog(string timeMessage) 
	{
	
	}

	private string GetTrashPath() 
	{
		string trashFile = string.Concat(_trashLog, _trashExtention);
		return Path.Combine(_logDir, trashFile);
	}

	private string GetTimePath() 
	{
		string trashFile = string.Concat(_timeLog, _timeExtention);
		return Path.Combine(_logDir, trashFile);
	}

}


public class LogReader 
{


}
