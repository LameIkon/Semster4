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

		_logDir = CreateLogDir(CreateRootDir());

		_writer = new LogWriter(_logDir);
	}

	public void Log()
	{
		_writer.WriteLog(_data.TrashDic, _data.Points, _data.TimeBetweenTrash, _data.CreatedTime, _data.StartedTime, _data.StoppedTime);
	}

	public void GameStarted() 
	{
		_data.StartedGame();
	}

	public void GameFinished() 
	{
		_data.StopedGame();
		
		//WriteLog("\n------- Game Stats -------");
		//WriteLog($"Created:        {_data.CreatedTime.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.GetCultureInfo("da-DK"))}");
		//WriteLog($"Started:        {_data.StartedTime.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.GetCultureInfo("da-DK"))}");
		//WriteLog($"Stopped:        {_data.StoppedTime.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.GetCultureInfo("da-DK"))}");
		//WriteLog($"Game Time:      {_data.GameTime()}");
		//WriteLog($"Tutorial Time:  {_data.TutorialTime()}");
		//WriteLog($"Total Points:   {_data.TotalPoints}");
	}


	private string CreateRootDir() 
	{
        string logPath = Path.Combine(_appPath, _logs);
        Directory.CreateDirectory(logPath);
		return logPath;
    }

	private string CreateLogDir(string logPath) 
	{
        string logDir = Path.Combine(logPath, _id);
        Directory.CreateDirectory(logDir);
		return logDir;
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


	public void WriteLog(IDictionary<string, string> trashLog, IList<float> points, IList<TimeSpan> timeBetweenTrash, params DateTime[] dateTimes) 
	{
		WriteTrashLog(trashLog, points, timeBetweenTrash);
		WriteTimeLog(dateTimes);
	}

	private void WriteTrashLog(IDictionary<string, string> trashLog, IList<float> points, IList<TimeSpan> timeBetweenTrash) 
	{
		ClearLog(GetTrashLogPath());

		IList<string> logs = new List<string>();

		foreach (KeyValuePair<string, string> trashLogItem in trashLog)
		{
			logs.Add($"{trashLogItem.Key},{trashLogItem.Value},");
		}
		for (int i = 0; i < points.Count; i++) 
		{
			logs[i] += $"{points[i]},{timeBetweenTrash[i]}";
		}

		using (StreamWriter writer = new StreamWriter(GetTrashLogPath(), true))
		{
			writer.WriteLine("Trash,TrashBin,Points,TimeBetween");

			foreach (string log in logs) 
			{
				writer.WriteLine(log);
			}
		}
	}

	private void WriteTimeLog(params DateTime[] dateTimes) 
	{
		ClearLog(GetTimeLogPath());

		using (StreamWriter writer = new StreamWriter(GetTimeLogPath(), true))
		{
			writer.WriteLine($"Created,StartGame,StoppedGame");
			string logDate = string.Empty;
			for (int i = 0; i < dateTimes.Length; i++)
			{
				if (i > 0)
				{
					logDate += $",{dateTimes[i].ToString()}";
				}
				else
				{
					logDate += dateTimes[i].ToString();
				}
			}
			writer.WriteLine(logDate);
		}
	}

	private string GetTrashLogPath() 
	{
		string trashFile = string.Concat(_trashLog, _trashExtention);
		return Path.Combine(_logDir, trashFile);
	}

	private string GetTimeLogPath() 
	{
		string trashFile = string.Concat(_timeLog, _timeExtention);
		return Path.Combine(_logDir, trashFile);
	}

	private void ClearLog(string filePath) 
	{
		using (StreamWriter writer = new StreamWriter(filePath, false)) 
		{
			writer.Write(string.Empty);
		}
	}
}


public class LogReader 
{
	private string _logDir;

	private const string _trashLog = "TrashLog";
	private const string _trashExtention = ".csv";
	private const string _timeLog = "TimeLog";
	private const string _timeExtention = ".txt";


	public LogData GetLogData(string dirPath) 
	{
		_logDir = dirPath;

		LogData logData = ReadFile();

		return logData;
	}


	private LogData ReadFile()
	{
		IDictionary<string, string> trashDic = new Dictionary<string, string>();
		DateTime created = DateTime.Now;
		DateTime startGame = DateTime.Now;
		DateTime stoppedGame = DateTime.Now;
		IList<TimeSpan> timeBetweenTrash = new List<TimeSpan>();
		IList<float> points = new List<float>();

		using (StreamReader reader = new StreamReader(GetTrashLogPath())) 
		{
			bool firstLine = true;
			while (!reader.EndOfStream) 
			{
				if (!firstLine)
				{
					try
					{
						string line = reader.ReadLine();
						string[] tokens = line.Split(',');
						trashDic.Add(tokens[0], tokens[1]);
						points.Add(float.Parse(tokens[2]));
						timeBetweenTrash.Add(TimeSpan.Parse(tokens[3]));
					}
					catch (FormatException ex) 
					{
						
					}

				}
				else
				{ 
					firstLine = false;
				}
			
			}
		}

		using (StreamReader reader = new StreamReader(GetTimeLogPath()))
		{
			bool firstLine = true;
			while (!reader.EndOfStream)
			{
				if (!firstLine)
				{
					try
					{
						string line = reader.ReadLine();
						string[] tokens = line.Split(',');
						created = DateTime.Parse(tokens[0]);
						startGame = DateTime.Parse(tokens[1]);
						stoppedGame = DateTime.Parse(tokens[2]);
					}
					catch (FormatException ex)
					{

					}

				}
				else
				{
					firstLine = false;
				}

			}

		}

		return new LogData(trashDic, created, startGame, stoppedGame, points, timeBetweenTrash);

	}

	private string GetTrashLogPath()
	{
		string trashFile = string.Concat(_trashLog, _trashExtention);
		return Path.Combine(_logDir, trashFile);
	}

	private string GetTimeLogPath()
	{
		string trashFile = string.Concat(_timeLog, _timeExtention);
		return Path.Combine(_logDir, trashFile);
	}
}
