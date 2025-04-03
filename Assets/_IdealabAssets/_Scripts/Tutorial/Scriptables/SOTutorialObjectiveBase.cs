using System.Collections.Generic;
using UnityEngine;

public abstract class SOTutorialObjectiveBase : ScriptableObject
{
    public SOTutorialData SO_tutorialData; // Scriptable
    protected SOTutorialData _runtimeTutorialData; // Cloned tutorial data
    protected int _currentPage;

    public abstract void EnterState(TutorialManager manager);
    public virtual void ExecuteState(TutorialManager manager) { }
    public abstract void CompleteState(TutorialManager manager);
    public abstract void ExitState(TutorialManager manager);

    public void NextPage()
    {
        _currentPage++;
        if (_currentPage < _runtimeTutorialData.SO_Pages.Count) // Check if there are more pages left
        {
            Debug.Log("Next page");
            UpdateText(TutorialManager.S_Instance);
            TutorialManager.S_Instance.ShowProgression();
        }
        else // If not then exit state
        {
            Debug.Log("No page left");
            ExitState(TutorialManager.S_Instance);
        }
    }

    protected void UpdateText(TutorialManager manager)
    {
        Debug.Log(_currentPage);

        SOObjectivePage currentPageData = _runtimeTutorialData.SO_Pages[_currentPage];

        manager._Descriptiontext.text = currentPageData.SO_Description;
        if (!currentPageData.SO_KeepPreviousTaskDescription) // Keep or remove it. Done in inspector
        {
            manager._Objective.text = string.Empty;
        } 

        foreach (SOObjectiveCondition currentCondition in currentPageData.SO_Tasks)
        {
            string textColor = (currentCondition._currentAmount >= currentCondition._requiredAmount) ? "green" : "yellow"; // Decide the color. It knows what color just by using the tags

            manager._Objective.text += string.Format("<color={0}>{1}/{2}</color> {3}\n", // It takes the 3 below values and insert it in corresponding formation
                textColor, // Color of values (0)
                currentCondition._currentAmount, // Current amount (1)
                currentCondition._requiredAmount, // Max amount (2)
            currentCondition.SO_conditionDescription); // Task Description (3)
        }
        
        manager._ContinueButton.SetActive(!currentPageData.HasTasks()); // Show only if there are no tasks
    }

    protected void CheckCompletion() // Check if all conditions are complete
    {
        foreach (SOObjectiveCondition condition in _runtimeTutorialData.SO_Pages[_currentPage].SO_Tasks)
        {
            if (!condition._isCompleted) // if any task is not complete
            {
                return; // Stop here and dont continue
            }
        }
        CompleteState(TutorialManager.S_Instance); // Complete objective
    }


    public void CloneData()
    {
        _runtimeTutorialData = Instantiate(SO_tutorialData); // Clone the SO
        _currentPage = 0; // Reset Page number

        //foreach (ObjectiveCondition condition in SO_tutorialData.SO_Tasks)
        //{
        //    _runtimeTutorialData.SO_Tasks.Add(Instantiate(condition)); // Just clone the object
        //}

        for (int page = 0; page < SO_tutorialData.SO_Pages.Count; page++)
        {
            _runtimeTutorialData.SO_Pages[page] = Instantiate(SO_tutorialData.SO_Pages[page]);

            for (int task = 0; task < SO_tutorialData.SO_Pages[page].SO_Tasks.Count; task++)
            {
                _runtimeTutorialData.SO_Pages[page].SO_Tasks[task] = Instantiate(SO_tutorialData.SO_Pages[page].SO_Tasks[task]);
            }
        }
    }
}
