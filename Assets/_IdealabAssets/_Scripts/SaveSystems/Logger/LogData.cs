using System.Collections.Generic;
using System;
using System.Globalization;

public class LogData
{
    private IDictionary<string, string> _trashDic;
    private DateTime _created, _startGame, _stoppedGame, _betweenTrash;
    private IList<TimeSpan> _timeBetweenTrash;
    private IDictionary<string, IList<string>> _trashDic2;
    private IDictionary<string, IList<TimeSpan>> _trashTimeDic;

    public LogData()
    {
        _trashDic = new Dictionary<string, string>();
        _timeBetweenTrash = new List<TimeSpan>();
        _trashDic2 = new Dictionary<string, IList<string>>();
        _trashTimeDic = new Dictionary<string, IList<TimeSpan>>();
        _created = DateTime.Now;
    }

    public LogData(IDictionary<string, string> trashDic, DateTime created, 
                    DateTime startedGame, DateTime stoppedGame, IList<TimeSpan> timeBetweenTrash)
    {
        _trashDic = trashDic;
        _created = created;
        _startGame = startedGame;
        _stoppedGame = stoppedGame;
        _timeBetweenTrash = timeBetweenTrash;
    }

    public DateTime CreatedTime
    {
        get => _created;
    }

    public DateTime StartedTime
    {
        get => _startGame;
    }

    public DateTime StoppedTime
    {
        get => _stoppedGame;
    }


    public IDictionary<string, IList<string>> TrashDic
    {
        get => _trashDic2;
    }

    public IDictionary<string, IList<TimeSpan>> TimeBetweenTrash
    {
        get => _trashTimeDic;
    }


    public void AddToDic(string trash, string trashBin)
    {
        if (_trashDic2.ContainsKey(trash))
        {
            _trashDic2[trash].Add(trashBin);
        }
        else 
        {
            IList<string> list = new List<string>();
            list.Add(trashBin);
            _trashDic2.TryAdd(trash, list);
        }

        AddTimeBetweenTrash(trash);
        StopedGame();
    }

    public void StartedGame()
    {
        _startGame = DateTime.Now;
        _betweenTrash = _startGame;
    }

    public void StopedGame() => _stoppedGame = DateTime.Now;

    public TimeSpan TutorialTime()
    {
        return (_startGame - _created);
    }

    public TimeSpan GameTime()
    {
        return (_stoppedGame - _startGame);
    }

    public TimeSpan TotalTime()
    {
        IList<TimeSpan> spans = new List<TimeSpan>();

        foreach (KeyValuePair<string, IList<TimeSpan>> kvp in _trashTimeDic) 
        {
            foreach (TimeSpan span in kvp.Value) 
            {
                spans.Add(span);
            }
        }

        TimeSpan totalTime = TimeSpan.Zero;
        foreach (TimeSpan span in spans) 
        {
            totalTime += span;
        }

        return totalTime;
    
    }

    private void AddTimeBetweenTrash(string trash)
    {
        DateTime timeNow = DateTime.Now;
        TimeSpan trashTime = timeNow - _betweenTrash;
        _betweenTrash = timeNow;

		if (_trashTimeDic.ContainsKey(trash))
		{
			_trashTimeDic[trash].Add(trashTime);
		}
		else
		{
			IList<TimeSpan> list = new List<TimeSpan>();
			list.Add(trashTime);
			_trashTimeDic.TryAdd(trash, list);
		}
	}

}