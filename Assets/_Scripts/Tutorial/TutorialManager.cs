using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    [Header("Main Components")]
    [SerializeField] private SOTutorialData[] SO_tutorialData;
    [SerializeField] private GameObject[] _highlightDots;
    [SerializeField] private TextMeshProUGUI _Descriptiontext;
    [SerializeField] private GameObject _continueButton;
    private PlayerVR _playerVR;
    private int _index;

    [Header("Objectives")]
    [SerializeField] private SOTutorialData[] SO_tutorialObjectiveTwo;
    [SerializeField] private TextMeshProUGUI _ObjectivetextOne;
    [SerializeField] private TextMeshProUGUI _Objectivetexttwo;


    private const int _totalTrashAmountThrownOut = 3; // Counted as 4, since index 0 counts also
    private int _currentTrashThrownOut;

    private const int _totalInspectedAmount = 3; // Counted as 4, since index 0 counts also
    private int _currentInspectedObjects;

    public void OnEnable()
    {
        TrashBin.s_OnTrashedEvent += SecondObjective; // Adds the HandleTrashEvent to the Action OnTrashedEvent
        TrashBin.s_OnTrashedEvent += ThirdObjectiveTrashObjects;
    }

    public void OnDisable()
    {
        TrashBin.s_OnTrashedEvent -= SecondObjective;
        TrashBin.s_OnTrashedEvent -= ThirdObjectiveTrashObjects;
    }

    private void Start()
    {
        _playerVR = PlayerVR.S_Instance;
        SetTutorialQuest(_index);
    }


    public void OnContinueButtonPressed() // Button
    {
        ContinueTutorial();
    }

    private void ContinueTutorial()
    {
        _index++;

        if (_index < SO_tutorialData.Length)  // Next
        {
            SetTutorialQuest(_index);
        }
        else // Finished
        {
            Debug.Log("something will happen else");
        }
    }

    private void SetTutorialQuest(int index)
    {
        _Descriptiontext.text = SO_tutorialData[index].SO_Description;
        _highlightDots[index-1].SetActive(false); // Deactive previous dot
        _highlightDots[index].SetActive(true); // Activate next dot.
    }

    private void Update()
    {
        // This will call the check method only for the current quest
        switch (_index)
        {
            case 0: // Press continue button
                _continueButton.SetActive(true);
                break;
            case 1: // Complete Objective to continue
                _continueButton.SetActive(false);
                FirstObjective(); 
                break;
            case 2: // Press continue button
                _continueButton.SetActive(true);
                break;
            case 3: // Complete Objective to continue. 2nd Objective
                _continueButton.SetActive(false);
                break;
            case 4: // Press continue button
                _continueButton.SetActive(true);
                break;
            default:
                break;
        }
    }


    private void FirstObjective() // Hold Object
    {
        if (_playerVR.IsHoldingObject())
        {
            ContinueTutorial();
        }
    }

    private void SecondObjective(GameObject sender, float points) // Trash object
    {
        Debug.Log(_currentTrashThrownOut);
        if (_currentTrashThrownOut == _totalTrashAmountThrownOut)
        {
            ContinueTutorial();
            _currentTrashThrownOut = 0; // Resetted for other use cases
            return;
        }
        else
        {
            _Descriptiontext.text = SO_tutorialObjectiveTwo[_currentTrashThrownOut].SO_Description;
            _currentTrashThrownOut++;
        }

    }

    private void ThirdObjectiveInspectObjects()
    {
        if (_playerVR._IsInspectingObjectButton && _playerVR.IsHoldingObject()) // Inspect
        {
            if (_currentInspectedObjects < _totalInspectedAmount) // Yellow text
            {
                _ObjectivetextOne.text = ($"<b><color=#FFFF00>{_currentInspectedObjects}</color>/{_totalInspectedAmount} inspiceret affald</b>");
            }
            else // Green text
            {
                _ObjectivetextOne.text = ($"<b><color=#00FF00>{_currentInspectedObjects}</color>/{_totalInspectedAmount} inspiceret affald</b>");
            }
            _currentInspectedObjects++;
        }

        if (_currentTrashThrownOut >= _totalTrashAmountThrownOut && _currentInspectedObjects >= _totalInspectedAmount) // When both inspection and trashing is complete
        {
            ContinueTutorial();
        }
    }

    private void ThirdObjectiveTrashObjects(GameObject sender, float points) //trash object
    {
        if (_currentTrashThrownOut < _totalTrashAmountThrownOut) // Yellow Text
        {
            _Objectivetexttwo.text = ($"<b><color=#FFFF00>{_currentInspectedObjects}</color>/{_totalInspectedAmount} affald sorteret</b>");
        }
        else // Green Text
        {
            _ObjectivetextOne.text = ($"<b><color=#00FF00>{_currentInspectedObjects}</color>/{_totalInspectedAmount} affald sorteret</b>");
        }
        _currentTrashThrownOut++;

        if (_currentTrashThrownOut >= _totalTrashAmountThrownOut && _currentInspectedObjects >= _totalInspectedAmount) // When both inspection and trashing is complete
        {
            ContinueTutorial();
        }
    }

    private void FourthObjective() // Open door
    {

    }
}
