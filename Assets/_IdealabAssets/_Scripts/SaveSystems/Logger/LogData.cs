using System.Collections.Generic;
using System;
using System.Globalization;

public class LogData
{
    private IDictionary<string, string> _trashDic;
    private DateTime _created, _startGame, _stoppedGame, _betweenTrash;
    private IList<TimeSpan> _timeBetweenTrash;
    private IList<float> _points;

    public LogData()
    {
        _trashDic = new Dictionary<string, string>();
        _timeBetweenTrash = new List<TimeSpan>();
        _points = new List<float>();
        _created = DateTime.Now;
    }

    public LogData(IDictionary<string, string> trashDic, DateTime created, DateTime startedGame, DateTime stoppedGame, IList<float> points,
                   IList<TimeSpan> timeBetweenTrash)
    {
        _trashDic = trashDic;
        _created = created;
        _startGame = startedGame;
        _stoppedGame = stoppedGame;
        _timeBetweenTrash = timeBetweenTrash;
        _points = points;
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

    public float TotalPoints
    {
        get
        {
            float total = 0;
            foreach (float point in _points)
            {
                total += point;
            }

            return total;
        }
    }

    public IDictionary<string, string> TrashDic
    {
        get => _trashDic;
    }

    public IList<TimeSpan> TimeBetweenTrash
    {
        get => _timeBetweenTrash;
    }

    public IList<float> Points
    {
        get => _points;
    }

    public void AddToDic(string trash, string trashBin, float points)
    {
        _trashDic.TryAdd(trash, trashBin);
        AddPoints(points);
        AddTimeBetweenTrash();
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

    private void AddTimeBetweenTrash()
    {
        DateTime timeNow = DateTime.Now;
        TimeSpan trashTime = timeNow - _betweenTrash;
        _betweenTrash = timeNow;
        _timeBetweenTrash.Add(trashTime);
    }

    private void AddPoints(float points)
    {
        _points.Add(points);
    }
}