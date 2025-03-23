using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    //private static readonly string s_saveFolder = GetSavePath();
    private static readonly string LeaderboardKey = "LeaderboardData";
    private static int _dataStorageCapacity = 10;


    [System.Serializable]
    public class SaveData
    {
        public string _Username;
        public int _Score;
        public float _TotalTime;
    }

    [System.Serializable]
    public class LeaderboardData
    {
        public List<SaveData> scores = new List<SaveData>();
    }

    //private static string GetSavePath()
    //{

    //    if (Application.isEditor)  // In the Editor, save to the project's "Assets" folder
    //    {
    //        Debug.Log("saving in editor");
    //        string editorSavePath = Path.Combine(Application.dataPath, "SaveFiles", "leaderboard.json");
    //        EnsureDirectoryExists(editorSavePath);
    //        return editorSavePath; // Save within the project folder
    //    }
    //    else
    //    {
    //        Debug.Log("saving in build");
    //        string persistentSavePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
    //        EnsureDirectoryExists(persistentSavePath);
    //        return persistentSavePath;  // In a build, save to the persistent data path
    //    }
    //}

    //private static void EnsureDirectoryExists(string path)
    //{
    //    string directory = Path.GetDirectoryName(path);
    //    if (!Directory.Exists(directory))
    //    {
    //        Directory.CreateDirectory(directory); 
    //    }
    //}

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
            leaderboard.scores.RemoveRange(_dataStorageCapacity, leaderboard.scores.Count - _dataStorageCapacity); // remove so until we have max allowed data from lowest to highest
        }

        // Save data
        string json = JsonUtility.ToJson(leaderboard);
        //File.WriteAllText(s_saveFolder, json);
        PlayerPrefs.SetString(LeaderboardKey, json);
        PlayerPrefs.Save();
    }

    private static bool CompareNames(string a, string b)
    {
        return a == b;  // Return true if the usernames are the same
    }

    private static int CompareScores(SaveData a, SaveData b)
    {
        int scoreComparison = b._Score.CompareTo(a._Score); // Check which have the highest score
        if (scoreComparison != 0) // if they arent equal:
        {
            return scoreComparison; // Return highest score
        }
        return a._TotalTime.CompareTo(b._TotalTime); // If score is equal then compare the total play time
    }

    public static List<SaveData> Load()
    {
        return LoadLeaderboard().scores;
    }

    private static LeaderboardData LoadLeaderboard()
    {
        //if (!File.Exists(s_saveFolder))
        //{
        //    return new LeaderboardData();
        //}

        //string json = File.ReadAllText(s_saveFolder);
        //return JsonUtility.FromJson<LeaderboardData>(json);

        if (!PlayerPrefs.HasKey(LeaderboardKey))
        {
            return new LeaderboardData();
        }

        string json = PlayerPrefs.GetString(LeaderboardKey);
        return JsonUtility.FromJson<LeaderboardData>(json);
    }

    public static void DeleteAllSaveFiles()
    {
        PlayerPrefs.DeleteKey("LeaderboardData");
        PlayerPrefs.Save();
    }
}
