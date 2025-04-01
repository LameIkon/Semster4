using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    [Header("Main Components")]

    // Current Objective
    TutorialObjectiveBase _currentObjective;

    // All Objectives 
    public List<TutorialObjectiveBase> _allObjectives;

    [Header("UI Components")]
    public GameObject _ContinueButton;
    public TextMeshProUGUI _Descriptiontext;
    public TextMeshProUGUI _Objective;
    [SerializeField] private GameObject[] _highlightDots;


    private void Start()
    {
        _currentObjective = _allObjectives[0]; // Start at the first index. Rest will assign by themselves
        _currentObjective.EnterState(this);
    }


    private int _currenProgressDisplay = 0;
    public void ShowProgression()
    {
        if (_currenProgressDisplay > 0) // if current step is higher than 0. Prevents out of bounds
        {
            _highlightDots[_currenProgressDisplay - 1].SetActive(false); // previous set to false
        }
        _highlightDots[_currenProgressDisplay].SetActive(true); // Set new one to true
        _currenProgressDisplay++;      
    }

    public void NextObjective()
    {
        if (_currenProgressDisplay < _allObjectives.Count)
        {
            _currentObjective = _allObjectives[_currenProgressDisplay];
            _currentObjective.EnterState(this);
        }
    }

    //[SerializeField] private SOTutorialData[] SO_tutorialData;
    //private PlayerVR _playerVR;
    //private int _index;

    //[Header("Objectives")]
    //[SerializeField] private SOTutorialData[] SO_tutorialObjectiveTwo;
    //[SerializeField] private TextMeshProUGUI _Objectivetexttwo;


    //private const int _totalTrashAmountThrownOut = 3; // Counted as 4, since index 0 counts also
    //private int _currentTrashThrownOut;

    //private const int _totalInspectedAmount = 3; // Counted as 4, since index 0 counts also
    //private int _currentInspectedObjects;

    //private bool Objecttwo;
    //private bool ObjectThree;

    //public void OnContinueButtonPressed() // Button
    //{
    //    ContinueTutorial();
    //}

    //public void OnEnable()
    //{
    //    TrashBin.s_OnTrashedEvent += SecondObjective; // Adds the HandleTrashEvent to the Action OnTrashedEvent
    //    TrashBin.s_OnTrashedEvent += ThirdObjectiveTrashObjects;
    //}

    //public void OnDisable()
    //{
    //    TrashBin.s_OnTrashedEvent -= SecondObjective;
    //    TrashBin.s_OnTrashedEvent -= ThirdObjectiveTrashObjects;
    //}

    //private void ContinueTutorial()
    //{
    //    _index++;

    //    if (_index < SO_tutorialData.Length)  // Next
    //    {
    //        SetTutorialQuest(_index);
    //    }
    //    else // Finished
    //    {
    //        Debug.Log("something will happen else");
    //    }
    //}

    //private void SetTutorialQuest(int index)
    //{
    //    _Descriptiontext.text = SO_tutorialData[index].SO_Description;
    //    if (index > 0) // Prevent no negative number
    //    {
    //        _highlightDots[index-1].SetActive(false); // Deactive previous dot
    //    }
    //    _highlightDots[index].SetActive(true); // Activate next dot.
    //}

    //private void Update()
    //{
    //    // This will call the check method only for the current quest
    //    switch (_index)
    //    {
    //        case 0: // Press continue button
    //            _ContinueButton.SetActive(true);
    //            break;
    //        case 1: // Complete Objective to continue. Hold Object
    //            _ContinueButton.SetActive(false);
    //            FirstObjective(); 
    //            break;
    //        case 2: // Press continue button
    //            _ContinueButton.SetActive(true);
    //            break;
    //        case 3: // Complete Objective to continue. Trash Objects
    //            _ContinueButton.SetActive(false);
    //            Objecttwo = true;
    //            break;
    //        case 4: // Press continue button
    //            _ContinueButton.SetActive(true);
    //            break;
    //        case 5: // Press Continue button
    //            _ContinueButton.SetActive(true);
    //            break;
    //        case 6: // Complete Objective to contine. Inspect and trash objects
    //            _ContinueButton.SetActive(false);
    //            ObjectThree = true;
    //            _Objective.gameObject.SetActive(true);
    //            _Objectivetexttwo.gameObject.SetActive(true);
    //            ThirdObjectiveInspectObjects();
    //            break;
    //        case 7: // Press Continue button
    //            _ContinueButton.SetActive(true);
    //            break;
    //        default:
    //            _ContinueButton.SetActive(false);
    //            _Objective.gameObject.SetActive(false);
    //            _Objectivetexttwo.gameObject.SetActive(false);
    //            break;
    //    }
    //}


    //private void FirstObjective() // Hold Object
    //{
    //    if (_playerVR.IsHoldingObject())
    //    {
    //        ContinueTutorial();
    //    }
    //}

    //private void SecondObjective(GameObject sender, float points) // Trash object
    //{
    //    if (!Objecttwo) return;

    //    Debug.Log(_currentTrashThrownOut);
    //    if (_currentTrashThrownOut == _totalTrashAmountThrownOut)
    //    {
    //        ContinueTutorial();
    //        _currentTrashThrownOut = 0; // Resetted for other use cases
    //        Objecttwo = false; // this method can not be called anymore
    //        return;
    //    }
    //    else
    //    {
    //        _Descriptiontext.text = SO_tutorialObjectiveTwo[_currentTrashThrownOut].SO_Description;
    //        _currentTrashThrownOut++;
    //    }

    //}
    //private bool _triggerOnce = true; // temporary
    //private bool _wasInspectingButtonPressed = false; // temporary
    //private void ThirdObjectiveInspectObjects()
    //{
    //    if (_triggerOnce)
    //    {
    //        _Objective.text = ($"<b><color=#FFFF00>{_currentInspectedObjects}/{_totalInspectedAmount}</color> inspiceret affald</b>");
    //        _Objectivetexttwo.text = ($"<b><color=#FFFF00>{_currentTrashThrownOut}/{_totalTrashAmountThrownOut}</color> affald sorteret</b>");
    //        _triggerOnce = false;
    //    }

    //    bool isButtonPressed = _playerVR._IsInspectingObjectButton;
    //    if (isButtonPressed && !_wasInspectingButtonPressed && _playerVR.IsHoldingObject()) // Inspect
    //    {
    //        _currentInspectedObjects++;
    //        if (_currentInspectedObjects < _totalInspectedAmount) // Yellow text
    //        {
    //            _Objective.text = ($"<b><color=#FFFF00>{_currentInspectedObjects}/{_totalInspectedAmount}</color> inspiceret affald</b>");
    //        }
    //        else // Green text
    //        {
    //            _Objective.text = ($"<b><color=#00FF00>{_currentInspectedObjects}/{_totalInspectedAmount}</color> inspiceret affald</b>");
    //        }        
    //    }

    //    _wasInspectingButtonPressed = isButtonPressed;

    //    if (_currentTrashThrownOut >= _totalTrashAmountThrownOut && _currentInspectedObjects >= _totalInspectedAmount) // When both inspection and trashing is complete
    //    {
    //        ContinueTutorial();
    //        ObjectThree = false; // this method can not be called anymore
    //    }
    //}

    //private void ThirdObjectiveTrashObjects(GameObject sender, float points) //trash object
    //{
    //    if (!ObjectThree) return;

    //    _currentTrashThrownOut++;
    //    if (_currentTrashThrownOut < _totalTrashAmountThrownOut) // Yellow Text
    //    {
    //        _Objectivetexttwo.text = ($"<b><color=#FFFF00>{_currentTrashThrownOut}/{_totalTrashAmountThrownOut}</color> affald sorteret</b>");
    //    }
    //    else // Green Text
    //    {
    //        _Objectivetexttwo.text = ($"<b><color=#00FF00>{_currentTrashThrownOut}/{_totalTrashAmountThrownOut}</color> affald sorteret</b>");
    //    }       

    //    if (_currentTrashThrownOut >= _totalTrashAmountThrownOut && _currentInspectedObjects >= _totalInspectedAmount) // When both inspection and trashing is complete
    //    {
    //        ContinueTutorial();
    //        ObjectThree = false; // this method can not be called anymore
    //    }
    //}

    //private void FourthObjective() // Open door
    //{

    //}
}
