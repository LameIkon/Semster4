using UnityEngine;

public abstract class TutorialObjectiveBase : ScriptableObject
{
    public SOTutorialData SO_tutorialData;
    protected int _currentDataIndex = 0;

    public abstract void EnterState(TutorialManager manager);
    public virtual void ExecuteState(TutorialManager manager) { }
    public abstract void ExitState(TutorialManager manager);

    protected void UpdateText(TutorialManager manager)
    {
        manager._Descriptiontext.text = SO_tutorialData.SO_Description;
        manager._Objective.text = string.Format(SO_tutorialData.SO_Objectives[_currentDataIndex]._conditionDescription, SO_tutorialData.SO_Objectives[_currentDataIndex]._currentAmount, SO_tutorialData.SO_Objectives[_currentDataIndex]._requiredAmount);
        manager._ContinueButton.SetActive(SO_tutorialData.SO_ShowContinueButton); // Check if there are more 
        
        //if (!string.IsNullOrEmpty(SO_tutorialData[_currentDataIndex].SO_Objective)) // Ensure not empty or null
        //{
        //    manager._Objective.text = string.Format(SO_tutorialData[_currentDataIndex].SO_Objective, currentAmount, totalAmount); // What the UI Should display of elements. Manager will set it up currectly
        //}

    }
}
