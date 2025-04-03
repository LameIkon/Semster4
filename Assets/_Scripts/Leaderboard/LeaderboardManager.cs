using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _names;
    [SerializeField] private TextMeshProUGUI _colon;
    [SerializeField] private TextMeshProUGUI _scores;
    [SerializeField] private TextMeshProUGUI _timing;


    public void OnEnable()
    {
        //CompletionTracker.s_OnCompletion += CompletionEvent;
    }

    public void OnDisable()
    {
        //CompletionTracker.s_OnCompletion -= CompletionEvent;
    }


    private void Start()
    {
        //SaveSystem.DeleteAllSaveFiles();
        //Load();
        TestLeaderboard();
    }

    public void SaveScore(string username, int score, float totalTime)
    {
        LeaderboardDataSaver.Save(username, score, totalTime);
        Load();
    }

    private void Load()
    {
        List<LeaderboardDataSaver.SaveData> scores = LeaderboardDataSaver.Load();
        LoadUI(scores);
    }

    private void LoadUI(List<LeaderboardDataSaver.SaveData> scores)
    {
        _names.text = string.Empty;
        _colon.text = string.Empty;
        _scores.text = string.Empty;
        _timing.text = string.Empty;

        foreach (LeaderboardDataSaver.SaveData score in scores)
        {
            _names.text += $"{score._Username}\n";
            _colon.text += ":\n";
            _scores.text += $"{score._Score}\n";
            _timing.text += $"{score._TotalTime}s\n";
        }
    }

    void TestLeaderboard()
    {
        SaveScore("Player1", 1500, 30.5f);
        SaveScore("Player2", 1200, 25.2f);
        SaveScore("Player3", 2000, 10.0f);
        SaveScore("Player4", 2500, 45.0f);
        SaveScore("Player5", 2500, 45.0f);
        SaveScore("Player6", 2500, 45.0f);
        SaveScore("Player7", 2500, 45.0f);
        SaveScore("Player8", 2500, 45.0f);
        SaveScore("Player9", 2500, 45.0f);
        SaveScore("Player10", 2500, 25.0f);
        SaveScore("Player11", 2500, 45.0f);
        SaveScore("Player12", 2500, 45.0f);
    }
}