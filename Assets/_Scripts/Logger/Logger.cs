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
		_id = DateTime.Now.ToString("yyyyMMddHHmmss");
	}
	public void Log()
	{
		string file = string.Concat(_id, _fileExtention);
		string fullPath = Path.Combine(_appPath, _appDir, file);

		using (StreamWriter writer = new StreamWriter(fullPath, true))
		{
			writer.WriteLine(_data.GetLastEntry());
		}
	}

	//public void Log()
	//{
	//	string file = string.Concat(_id, _fileExtention);
	//	string fullPath = Path.Combine(_appPath, _appDir, file);

	//	using (StreamWriter writer = new StreamWriter(fullPath, true)) 
	//	{
	//		foreach (string line in _data.GetData()) 
	//		{
	//			writer.WriteLine(line);
	//		}
	//	}
	//}	
	
}
