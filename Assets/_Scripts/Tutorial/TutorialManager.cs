using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] private SOTutorialData[] SO_tutorialData;
    [SerializeField] private GameObject[] _highlightDots;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _continueButton;
    private int _index;



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
            case 0:
                // Press continue button
                _continueButton.SetActive(true);
                break;
            case 1:
                _continueButton.SetActive(false);
                CheckHoldingObject(); // Complete Objective to continue
                break;
            case 2:
                _continueButton.SetActive(true);
                // Press continue button
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

    private void CheckHoldingAndInspecting()
    {
        //if (PlayerVR.S_Instance.Is)
    }
}
