using System.Collections.Generic;
using System;

public class LogData
{
	private IDictionary<string, string> _trashDic;
	private IList<string> _logList;
	private DateTime _created, _startGame, _stoppedGame, _betweenTrash;
	private float _totalPoints;

	public LogData()
	{
		_trashDic = new Dictionary<string, string>();
		_logList = new List<string>();
		_created = DateTime.Now;
		_totalPoints = 0;
	}

	public DateTime CreatedTime{ get => _created; }
	public DateTime StartedTime { get => _startGame; }
	public DateTime StoppedTime { get => _stoppedGame; }
	public DateTime BetweenTrashTime { get => _betweenTrash; }
	public string TotalPoints { get => _totalPoints.ToString(); }

	public void AddToDic(string trash, string trashBin, float points)
	{
		if (_trashDic.TryAdd(trash, trashBin)) 
		{
			_logList.Add( $"Trash: {trash}, TrashBin: {trashBin}, points: {points}");
			_totalPoints += points;
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
		return $"{_logList[_logList.Count - 1]}, time from last item trashed: {TimeBetweenTrash()}";
	}

	public void StartedGame() 
	{
		_startGame = DateTime.Now;
		_betweenTrash = _startGame;
	}
	public void StopedGame() => _stoppedGame = DateTime.Now;

	public string TutorialTime() 
	{
		return $"{FormatTime(_startGame - _created)}";
	}

	public string GameTime() 
	{
		return $"{FormatTime(_stoppedGame - _startGame)}";
	}

	private string TimeBetweenTrash() 
	{
		DateTime timeNow = DateTime.Now;
		TimeSpan trashTime = timeNow - _betweenTrash;
		_betweenTrash = timeNow;

		return $"{FormatTime(trashTime)}";

	}

	private string FormatTime(TimeSpan time) 
	{
		return time.ToString("mm':'ss'.'fff");
	}
}
