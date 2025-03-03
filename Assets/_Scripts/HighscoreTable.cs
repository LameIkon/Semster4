using TMPro;
using UnityEngine;

public class HighscoreTable : MonoBehaviour
{
    // This class uses a Singleton pattern to ensure one instance of the class,
    // avoiding the need to update "UpdateHighScorePoints()" in the Update() function, which is expensive.
    // By using the Singleton pattern, we can make the UpdateHighScorePoints() method static and call it 
    // only when the score should be changed, improving performance.

    [SerializeField] private TextMeshProUGUI _pointCounter;
    private static           HighscoreTable  _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        _pointCounter.text = 0.ToString();
    }

    public static void UpdateHighScorePoints(float? point)
    {
        if (_instance is null) return;

        float  currentPoints     = float.Parse(_instance._pointCounter.text);
        float? incrementedPoints = currentPoints;

        incrementedPoints            += point;
        _instance._pointCounter.text =  incrementedPoints.ToString();
    }
}