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

    private bool Objecttwo;
    private bool ObjectThree;

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
        _ObjectivetextOne.gameObject.SetActive(false);
        _Objectivetexttwo.gameObject.SetActive(false);
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
        if (index > 0) // Prevent no negative number
        {
            _highlightDots[index-1].SetActive(false); // Deactive previous dot
        }
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
            case 1: // Complete Objective to continue. Hold Object
                _continueButton.SetActive(false);
                FirstObjective(); 
                break;
            case 2: // Press continue button
                _continueButton.SetActive(true);
                break;
            case 3: // Complete Objective to continue. Trash Objects
                _continueButton.SetActive(false);
                Objecttwo = true;
                break;
            case 4: // Press continue button
                _continueButton.SetActive(true);
                break;
            case 5: // Press Continue button
                _continueButton.SetActive(true);
                break;
            case 6: // Complete Objective to contine. Inspect and trash objects
                _continueButton.SetActive(false);
                ObjectThree = true;
                _ObjectivetextOne.gameObject.SetActive(true);
                _Objectivetexttwo.gameObject.SetActive(true);
                ThirdObjectiveInspectObjects();
                break;
            case 7: // Press Continue button
                _continueButton.SetActive(true);
                break;
            default:
                _continueButton.SetActive(false);
                _ObjectivetextOne.gameObject.SetActive(false);
                _Objectivetexttwo.gameObject.SetActive(false);
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
        if (!Objecttwo) return;

        Debug.Log(_currentTrashThrownOut);
        if (_currentTrashThrownOut == _totalTrashAmountThrownOut)
        {
            ContinueTutorial();
            _currentTrashThrownOut = 0; // Resetted for other use cases
            Objecttwo = false; // this method can not be called anymore
            return;
        }
        else
        {
            _Descriptiontext.text = SO_tutorialObjectiveTwo[_currentTrashThrownOut].SO_Description;
            _currentTrashThrownOut++;
        }

    }
    private bool _triggerOnce = true; // temporary
    private bool _wasInspectingButtonPressed = false; // temporary
    private void ThirdObjectiveInspectObjects()
    {
        if (_triggerOnce)
        {
            _ObjectivetextOne.text = ($"<b><color=#FFFF00>{_currentInspectedObjects}/{_totalInspectedAmount}</color> inspiceret affald</b>");
            _Objectivetexttwo.text = ($"<b><color=#FFFF00>{_currentTrashThrownOut}/{_totalTrashAmountThrownOut}</color> affald sorteret</b>");
            _triggerOnce = false;
        }

        bool isButtonPressed = _playerVR._IsInspectingObjectButton;
        if (isButtonPressed && !_wasInspectingButtonPressed && _playerVR.IsHoldingObject()) // Inspect
        {
            _currentInspectedObjects++;
            if (_currentInspectedObjects < _totalInspectedAmount) // Yellow text
            {
                _ObjectivetextOne.text = ($"<b><color=#FFFF00>{_currentInspectedObjects}/{_totalInspectedAmount}</color> inspiceret affald</b>");
            }
            else // Green text
            {
                _ObjectivetextOne.text = ($"<b><color=#00FF00>{_currentInspectedObjects}/{_totalInspectedAmount}</color> inspiceret affald</b>");
            }        
        }

        _wasInspectingButtonPressed = isButtonPressed;

        if (_currentTrashThrownOut >= _totalTrashAmountThrownOut && _currentInspectedObjects >= _totalInspectedAmount) // When both inspection and trashing is complete
        {
            ContinueTutorial();
            ObjectThree = false; // this method can not be called anymore
        }
    }

    private void ThirdObjectiveTrashObjects(GameObject sender, float points) //trash object
    {
        if (!ObjectThree) return;

        _currentTrashThrownOut++;
        if (_currentTrashThrownOut < _totalTrashAmountThrownOut) // Yellow Text
        {
            _Objectivetexttwo.text = ($"<b><color=#FFFF00>{_currentTrashThrownOut}/{_totalTrashAmountThrownOut}</color> affald sorteret</b>");
        }
        else // Green Text
        {
            _Objectivetexttwo.text = ($"<b><color=#00FF00>{_currentTrashThrownOut}/{_totalTrashAmountThrownOut}</color> affald sorteret</b>");
        }       

        if (_currentTrashThrownOut >= _totalTrashAmountThrownOut && _currentInspectedObjects >= _totalInspectedAmount) // When both inspection and trashing is complete
        {
            ContinueTutorial();
            ObjectThree = false; // this method can not be called anymore
        }
    }

    private void FourthObjective() // Open door
    {

    }
}
