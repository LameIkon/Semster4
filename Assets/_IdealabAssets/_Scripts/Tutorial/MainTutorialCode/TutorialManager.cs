using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    [Header("Main Components")]
    // All Objectives 
    public List<SOTutorialObjectiveBase> _allObjectives;
    private SOTutorialObjectiveBase _currentObjective; // Current Objective

    [Header("UI Components")]
    public GameObject _ContinueButton;
    public TextMeshProUGUI _Descriptiontext;
    public TextMeshProUGUI _Objective;
    [SerializeField] private GameObject[] _highlightDots;

    private int _currentObjectiveIndex;
    private int _currenProgressDisplay;

    private void Start()
    {
        _currentObjective = _allObjectives[0]; // Start at the first index. Rest will assign by themselves
        _currentObjective.EnterState(this);
        ShowProgression();
    }

    public void ShowProgression()
    {
        Debug.Log(_currenProgressDisplay);
        if (_currenProgressDisplay > 0) // if current step is higher than 0. This prevents out of bounds
        {
            _highlightDots[_currenProgressDisplay - 1].SetActive(false); // previous set to false
        }

        if (_highlightDots.Length <= _currenProgressDisplay) return;

        _highlightDots[_currenProgressDisplay].SetActive(true); // Set new one to true
        _currenProgressDisplay++;
    }

    public void NextObjective()
    {
        Debug.Log("Try next objective");
        _currentObjectiveIndex++;
        if (_currentObjectiveIndex < _allObjectives.Count)
        {
            _currentObjective = _allObjectives[_currentObjectiveIndex];
            _currentObjective.EnterState(this);
            ShowProgression();
        }
        else
        {
            _currentObjective = null;
        }
    }

    public void NextPage() // Button
    {
        if (_currentObjective != null)
        {
            _currentObjective.NextPage();
        }
    }
}
