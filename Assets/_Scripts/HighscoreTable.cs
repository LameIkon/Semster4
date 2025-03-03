using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HighscoreTable : MonoBehaviour
{
    // This class uses a Singleton pattern to ensure one instance of the class,
    // avoiding the need to update "UpdateHighScorePoints()" in the Update() function, which is expensive.
    // By using the Singleton pattern, we can make the UpdateHighScorePoints() method static and call it 
    // only when the score should be changed, improving performance.

    [SerializeField] private TextMeshProUGUI _totalScore;
    [SerializeField] private TextMeshProUGUI _scoreIncremenTracker;
    private static           HighscoreTable  _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (_totalScore is not null)
        {
            _totalScore.text = 0.ToString();
        }

        if (_scoreIncremenTracker is not null)
        {
            _scoreIncremenTracker.text = "";
        }
    }

    public static void UpdateHighScorePoints(float? points)
    {
        if (_instance is null) return;

        float  currentPoints     = float.Parse(_instance._totalScore.text);
        float? incrementedPoints = currentPoints;

        incrementedPoints                    += points;
        _instance._totalScore.text           =  incrementedPoints.ToString();
        _instance._scoreIncremenTracker.text =  FormatScoreIncrementTracker();
        return;
        
        String FormatScoreIncrementTracker()
        {
            return points switch
            {
                
            };
        }
    }
    
}