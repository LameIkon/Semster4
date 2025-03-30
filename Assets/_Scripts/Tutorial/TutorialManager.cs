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
    private int _index;

    private Action _currentTutorialIndex;



    private void Start()
    {
        _text.text = SO_tutorialData[_index].SO_Description;
    }


    public void OnContinueButtonPressed() // Button
    {
        if (_index < _highlightDots.Length) // Previous
        {
            _highlightDots[_index].SetActive(false);
        }

        _index++;

        if (_index < SO_tutorialData.Length)  // Next
        {
            SetTutorialQuest();
        }
        else // Finished
        {
            Debug.Log("something will happen else");
        }
    }

    private void SetTutorialQuest()
    {
        _text.text = SO_tutorialData[_index].SO_Description;
        _highlightDots[_index].SetActive(true);

        switch (_index)  // Assign quest
        {
            case 0:
                _currentTutorialIndex = CheckHoldingObject;
                break;
            case 1:
                _currentTutorialIndex = CheckHoldingAndInspecting;
                break;
            default:
                _currentTutorialIndex = null;
                break;
        }
    }
  

    private void CheckHoldingObject()
    {
        //if (PlayerVR.S_Instance.IsHoldingObject())
        //{

        //}
    }

    private void CheckHoldingAndInspecting()
    {
        //if (PlayerVR.S_Instance.Is)
    }
}
