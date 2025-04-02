using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialObjectiveBase : ScriptableObject
{
    public SOTutorialData SO_tutorialData; // Scriptable
    protected SOTutorialData _runtimeTutorialData; // Cloned tutorial data
    protected int _currentPage;

    public abstract void EnterState(TutorialManager manager);
    public virtual void ExecuteState(TutorialManager manager) { }
    public abstract void ExitState(TutorialManager manager);

    public void NextPage()
    {
        Debug.Log("Next page");
        _currentPage++;
        UpdateText(TutorialManager.S_Instance);
        TutorialManager.S_Instance.ShowProgression();
    }

    protected void UpdateText(TutorialManager manager)
    {
        Debug.Log(_currentPage);
        manager._Descriptiontext.text = _runtimeTutorialData.SO_Description[_currentPage];
        manager._Objective.text = string.Empty;

        bool hasTasks = _currentPage == _runtimeTutorialData.SO_TaskIndex;

        if (hasTasks)
        {
            foreach (ObjectiveCondition currentCondition in _runtimeTutorialData.SO_Tasks)
            {
                string textColor = (currentCondition._currentAmount >= currentCondition._requiredAmount) ? "green" : "yellow"; // Decide the color. It knows what color just by using the tags

                manager._Objective.text += string.Format("<color={0}>{1}/{2}</color> {3}\n", // It takes the 3 below values and insert it in corresponding formation
                    textColor, // Color of values (0)
                    currentCondition._currentAmount, // Current amount (1)
                    currentCondition._requiredAmount, // Max amount (2)
                currentCondition._conditionDescription); // Task Description (3)

                hasTasks = true;

            }
        }
        manager._ContinueButton.SetActive(!hasTasks); // Show only if there are no tasks
    }


    public void CloneData()
    {
        _runtimeTutorialData = Instantiate(SO_tutorialData); // Clone the SO
        _runtimeTutorialData.SO_Tasks = new List<ObjectiveCondition>(); // Create a new list
        _currentPage = 0; // Reset Page number

        foreach (ObjectiveCondition condition in SO_tutorialData.SO_Tasks)
        {
            _runtimeTutorialData.SO_Tasks.Add(Instantiate(condition)); // Just clone the object
        }
    }
}
