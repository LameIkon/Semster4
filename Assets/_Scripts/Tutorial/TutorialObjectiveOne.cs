using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/ObjectiveOne")]
public class TutorialObjectiveOne : TutorialObjectiveBase
{

    public override void EnterState(TutorialManager manager) // Start objective
    {
        Debug.Log("Tutorial: Hold an Object");
        UpdateText(manager);

        PlayerVR.OnGripStateChanged += HandleGripStateChanged;
        PlayerVR.OnSelectStateChanged += HanldeInspect;
    }

    private void HandleGripStateChanged(bool isHolding) // The Objective
    {
        ObjectiveCondition condition = SO_tutorialData.SO_Objectives[_currentDataIndex]; // Get the objectives to the current task

        if (isHolding && !condition._isCompleted)
        {
            // Execute the condition logic
            Debug.Log(condition);
            SO_tutorialData.ExecuteCondition(TutorialManager.S_Instance, _currentDataIndex); // Check the objective
            UpdateText(TutorialManager.S_Instance);
        }
    }

    private void HanldeInspect(bool isInspecting)
    {
        ObjectiveCondition condition = SO_tutorialData.SO_Objectives[1]; // Get the objectives to the current task

        if (isInspecting && !condition._isCompleted)
        {
            // Execute the condition logic
            Debug.Log(condition);
            SO_tutorialData.ExecuteCondition(TutorialManager.S_Instance, 1); // Check the objective
            UpdateText(TutorialManager.S_Instance);
        }
    }

    public override void ExitState(TutorialManager manager) // Finish objective
    {
        Debug.Log("Completed: Holding Object");
        PlayerVR.OnGripStateChanged -= HandleGripStateChanged;
        manager.ShowProgression();
        manager.NextObjective(); // Start next objective
    }
}
