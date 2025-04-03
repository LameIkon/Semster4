using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public static class LeaderboardDataSaver
{
    private static readonly string LeaderboardKey = "LeaderboardData"; // Playerprefs key
    private static int _dataStorageCapacity = 10;                      // Limiter for how much data can be shown on leaderboard


    [Serializable]
    public class SaveData // A Datafile 
    {
        public string _Username;
        public int _Score;
        public float _TotalTime;
    }

    [Serializable]
    public class LeaderboardData // Stores SaveData files
    {
        public List<SaveData> scores = new List<SaveData>();
    }

    public static void Save(string username, int score, float totaltime)
    {
        LeaderboardData leaderboard = LoadLeaderboard();

        bool usernameExists = false;
        foreach (SaveData data in leaderboard.scores)
        {
            if (CompareNames(data._Username, username)) // If the username exists, update the existing user with the new data
            {
                data._Score = score;
                data._TotalTime = totaltime;
                usernameExists = true;
                break;
            }
        }

        if (!usernameExists) // Add new user 
        {
            leaderboard.scores.Add(new SaveData
            {
                _Username = username,
                _Score = score,
                _TotalTime = totaltime
            });
        }

        leaderboard.scores.Sort(CompareScores); // Sort so the highest values comes first

        if (leaderboard.scores.Count > _dataStorageCapacity) // if we have more data than allowed saving
        {
            leaderboard.scores.RemoveRange(_dataStorageCapacity,
                    leaderboard.scores.Count - _dataStorageCapacity); // remove so until we have max allowed data from lowest to highest
        }

        // Save data
        string json = JsonUtility.ToJson(leaderboard);
        PlayerPrefs.SetString(LeaderboardKey, json);
        PlayerPrefs.Save();
    }

    private static bool CompareNames(string a, string b)
    {
        return a == b; // Return true if the usernames are the same
    }

    private static int CompareScores(SaveData a, SaveData b)
    {
        int scoreComparison = b._Score.CompareTo(a._Score); // Check which have the highest score
        if (scoreComparison != 0)                           // If they aren't equal:
        {
            return scoreComparison; // Return highest score
        }

        return a._TotalTime.CompareTo(b._TotalTime); // If score is equal then compare the total play time. Takes the one with lowest
    }

    public static List<SaveData> Load()
    {
        return LoadLeaderboard().scores;
    }

    private static LeaderboardData LoadLeaderboard()
    {
        if (!PlayerPrefs.HasKey(LeaderboardKey))
        {
            return new LeaderboardData(); // Return an empty data collection 
        }

        string json = PlayerPrefs.GetString(LeaderboardKey);
        return JsonUtility.FromJson<LeaderboardData>(json);
    }

    public static void DeleteAllSaveFiles() // For testing purpose, otherwise should not be used
    {
        PlayerPrefs.DeleteKey("LeaderboardData");
        PlayerPrefs.Save();
    }
}