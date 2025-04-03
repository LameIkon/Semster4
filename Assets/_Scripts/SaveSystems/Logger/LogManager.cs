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
        CompletionTrackerBoard.s_OnComplete += HandleEndGame;
        GameStarter.s_OnGameStartEvent += HandleStartGame;
    }

    private void OnDisable()
    {
        TrashBin.s_OnLogEvent -= Add;
        CompletionTrackerBoard.s_OnComplete -= HandleEndGame;
        GameStarter.s_OnGameStartEvent -= HandleStartGame;
    }


    private void OnApplicationQuit()
    {
        _logger.GameFinished();
    }
#endregion

    private void Add(string trash, string trashBin, float points)
    {
        _data.AddToDic(trash, trashBin, points);
        _logger.Log();
    }

    private void HandleStartGame()
    {
        _logger.GameStarted();
    }

    private void HandleEndGame()
    {
        _logger.GameFinished();
    }
}