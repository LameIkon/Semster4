using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/ObjectiveOne")]
public class TutorialObjectiveOne : TutorialObjectiveBase
{

    public override void EnterState(TutorialManager manager) // Start objective
    {
        Debug.Log("Tutorial: Hold an Object");
        UpdateText(manager);

        PlayerVR.OnGripStateChanged += HandleGripStateChanged;
    }

    //public override void ExecuteState(TutorialManager manager) // On progression on objective.... This is currently redundant
    //{
    //    SO_tutorialData.ExecuteCondition(manager, _currentDataIndex);

    //    ObjectiveCondition condition = SO_tutorialData.SO_Objectives[_currentDataIndex];
    //    if (condition._isCompleted)
    //    {
    //        if (_currentDataIndex < SO_tutorialData.SO_Objectives.Count - 1)
    //        {
    //            _currentDataIndex++;
    //            UpdateText(manager);
    //        }
    //        else
    //        {
    //            ExitState(manager);
    //        }
    //    }
    //}

    private void HandleGripStateChanged(bool isHolding) // The Objective
    {
        ObjectiveCondition condition = SO_tutorialData.SO_Objectives[_currentDataIndex];

        if (isHolding && !condition._isCompleted)
        {
            // Execute the condition logic
            SO_tutorialData.ExecuteCondition(TutorialManager.S_Instance, _currentDataIndex);
            UpdateText(TutorialManager.S_Instance);
        }
    }

    public override void ExitState(TutorialManager manager) // Finish objective
    {
        Debug.Log("Completed: Holding Object");
        PlayerVR.OnGripStateChanged -= HandleGripStateChanged;
        manager.ShowProgression();
        manager.NextObjective(manager._objectiveTwo); // Start next objective
    }
}
