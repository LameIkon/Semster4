using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public abstract class TutorialObjectiveBase
{
    public SOTutorialData[] SO_tutorialData;
    protected int _currentDataIndex = 0;

    public abstract void EnterState(TutorialManager manager);
    public abstract void ExecuteState(TutorialManager manager);
    public abstract void ExitState(TutorialManager manager);

    protected void UpdateText(TutorialManager manager, int currentAmount, int totalAmount)
    {
        manager._Descriptiontext.text = SO_tutorialData[_currentDataIndex].SO_Description;
        manager._ContinueButton.SetActive(SO_tutorialData[_currentDataIndex].SO_ShowContinueButton); // Check if there are more 
        
        if (!string.IsNullOrEmpty(SO_tutorialData[_currentDataIndex].SO_Objective)) // Ensure not empty or null
        {
            manager._Objective.text = string.Format(SO_tutorialData[_currentDataIndex].SO_Objective, currentAmount, totalAmount); // What the UI Should display of elements. Manager will set it up currectly
        }

    }
}
