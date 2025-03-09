using System;
using JetBrains.Annotations;
using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using UnityEngine.Serialization;

public class HighscoreTable : MonoBehaviour
{
    // This class uses a Singleton pattern to ensure one instance of the class,
    // avoiding the need to update "UpdateHighScorePoints()" on line 40 in the Update() function, which is CPU heavy.
    // By using a Singleton pattern here, we can make the UpdateHighScorePoints() method static and call it 
    // only when the score must be changed, improving overall performance.

    private static           HighscoreTable  s_instance; 
    [SerializeField] private TextMeshProUGUI _totalScore;
    [SerializeField] private TextMeshProUGUI _scoreIncrementTracker;

    private void Awake()
    {
        if (s_instance == null) 
            s_instance = this;

        else Destroy(gameObject);
    }

    private void Start()
    {
        if (_totalScore is not null)
            _totalScore.text = 0.ToString();

        if (_scoreIncrementTracker is not null)
            _scoreIncrementTracker.text = String.Empty;
    }
    
    public static void UpdateHighScorePoints(float? points)
    {
        if (s_instance is null)
            Debug.LogError("Error: An instance of HighscoreTable.cs does not currently exist.");
        
        if (points is null)
            Debug.LogError("Error: DisplayErrorMessage had an unexpected error.");
        
        float? currentPoints               = float.Parse(s_instance._totalScore.text);
        float? incrementedPoints           = currentPoints + points.Value;
        String formatScoreIncrementTracker = (points > 0) ? $"+{points}" : points.ToString();

        s_instance._totalScore.text             = incrementedPoints.ToString();
        s_instance._scoreIncrementTracker.text  = formatScoreIncrementTracker;
        s_instance._scoreIncrementTracker.color = ApplyTextColor();
        return;

        Color ApplyTextColor()
        {
            return points switch
            {
                > 0 => Color.green,
                < 0 => Color.red,
                _   => Color.white
            };
        }
    }
}