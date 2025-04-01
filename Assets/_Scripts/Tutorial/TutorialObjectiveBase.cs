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

        ObjectiveCondition currentCondition = SO_tutorialData.SO_Objectives[_currentDataIndex];

        manager._Objective.text = string.Format("{0}/{1} {2}", // It takes the 3 below values and insert it in corresponding formation
            currentCondition._currentAmount, // Current amount (0)
            currentCondition._requiredAmount, // Max amount (1)
        currentCondition._conditionDescription); // Task Description (2)
        manager._ContinueButton.SetActive(SO_tutorialData.SO_ShowContinueButton); // Check if there are more 
    }
}
