using System;
using JetBrains.Annotations;
using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using UnityEngine.Serialization;

public sealed class HighscoreTable : MonoBehaviour
{
    // This class uses a Singleton pattern to ensure one instance of the class,
    // avoiding the need to update "UpdateHighScorePoints()" on line 40 in the Update() function, which is CPU heavy.
    // By using a Singleton pattern here, we can make the UpdateHighScorePoints() method static and call it 
    // only when the score must be changed, improving overall performance.

    private static           HighscoreTable  s_instance;
    [SerializeField] private TextMeshProUGUI _totalScore;
    [SerializeField] private TextMeshProUGUI _scoreIncrementTracker;

    #region Unity Methods
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

    private void OnEnable()
    {
        TrashBin.s_OnTrashedEvent += HandleUpdateHighScorePoints;    
    }

    private void OnDisable()
    {
        TrashBin.s_OnTrashedEvent -= HandleUpdateHighScorePoints;
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    private static void HandleUpdateHighScorePoints(GameObject go, float points)
    {
        if (s_instance is null)
        {
            Debug.LogError("Error: An instance of HighscoreTable.cs does not currently exist.");
            return;
        }

        //if (points is null)
        //{
        //    Debug.LogError($"Error: DisplayErrorMessage encountered a null parameter. points: {points}");
        //    return;
        //}

        float? currentPoints = float.Parse(s_instance._totalScore.text);
        float? incrementedPoints = currentPoints + points;
        string formatScoreIncrementTracker = (points > 0) ? $"+{points}" : points.ToString();

        s_instance._totalScore.text = incrementedPoints.ToString();
        s_instance._scoreIncrementTracker.text = formatScoreIncrementTracker;
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