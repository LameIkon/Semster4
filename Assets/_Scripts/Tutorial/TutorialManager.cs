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
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _continueButton;
    private int _index;

    // Second tutorial parts
    [Header("Second Objective")]
    [SerializeField] private SOTutorialData[] SO_tutorialObjectiveTwo;
    private int _totalTrashAmountThrownOut = 3; // Counted as 4, since index 0 counts also
    private int _currentTrashThrownOut;

    public void OnEnable()
    {
        TrashBin.s_OnTrashedEvent += ChecksortingTrashAmount; // Adds the HandleTrashEvent to the Action OnTrashedEvent
    }

    public void OnDisable()
    {
        TrashBin.s_OnTrashedEvent -= ChecksortingTrashAmount;
    }

    private void Start()
    {
        SetTutorialQuest(_index);
    }


    public void OnContinueButtonPressed() // Button
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
        _text.text = SO_tutorialData[index].SO_Description;
        _highlightDots[index-1].SetActive(false); // Deactive previous dot
        _highlightDots[index].SetActive(true); // Activate next dot.

        //switch (index)  // Assign quest
        //{
        //    case 0:
        //        CheckHoldingObject();
        //        break;
        //    case 1:
        //        //_currentTutorialIndex = CheckHoldingAndInspecting;
        //        break;
        //    default:
        //        //_currentTutorialIndex = null;
        //        break;
        //}
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
                CheckHoldingObject(); 
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


    private void CheckHoldingObject()
    {
        if (PlayerVR.S_Instance.IsHoldingObject())
        {
            OnContinueButtonPressed();
        }
    }

    private void ChecksortingTrashAmount(GameObject sender, float points)
    {
        Debug.Log(_currentTrashThrownOut);
        if (_currentTrashThrownOut == _totalTrashAmountThrownOut)
        {
            OnContinueButtonPressed();
            return;
        }
        else
        {
            _text.text = SO_tutorialObjectiveTwo[_currentTrashThrownOut].SO_Description;
            _currentTrashThrownOut++;
        }

    }

    private void CheckHoldingAndInspecting()
    {
        //if (PlayerVR.S_Instance.Is)
    }
}
