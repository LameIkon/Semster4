using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/ObjectiveOne")]
public class TutorialObjectiveOne : TutorialObjectiveBase
{

    public override void EnterState(TutorialManager manager) // Start objective
    {
        Debug.Log("Tutorial: Hold an Object");

        CloneData(); // Copy the scriptable objects that needs to be used
        UpdateText(manager); // Display/update UI

        PlayerVR.OnGripStateChanged += HandleGripStateChanged;
        PlayerVR.OnSelectStateChanged += HandleInspect;
    }

    private void HandleGripStateChanged(bool isHolding) // Called by event
    {
        ObjectiveCondition condition = _runtimeTutorialData.SO_Tasks[_currentTask]; // Get the objectives to the current task

        if (isHolding && !condition._isCompleted)
        {
            _runtimeTutorialData.ExecuteCondition(_currentTask); // Check the objective
            UpdateText(TutorialManager.S_Instance);
        }
        CheckCompletion();
    }

    private void HandleInspect(bool isInspecting) // Called by event
    {
        ObjectiveCondition condition = SO_tutorialData.SO_Tasks[1]; // Get the objectives to the current task

        if (isInspecting && !condition._isCompleted)
        {
            _runtimeTutorialData.ExecuteCondition(1); // Check the objective
            UpdateText(TutorialManager.S_Instance);
        }
        CheckCompletion();
    }

    private void CheckCompletion()
    {
        foreach (ObjectiveCondition condition in _runtimeTutorialData.SO_Tasks)
        {
            if (!condition._isCompleted)
            {
                return; // Stop here and dont continue
            }
        }
        ExitState(TutorialManager.S_Instance);
    }

    public override void ExitState(TutorialManager manager) // Finish objective
    {
        Debug.Log("Completed: Holding Object");
        PlayerVR.OnGripStateChanged -= HandleGripStateChanged;
        PlayerVR.OnSelectStateChanged -= HandleInspect;
        manager.ShowProgression();
        manager.NextObjective(); // Start next objective
    }
}
