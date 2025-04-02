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
        if (!isHolding) return;

        ObjectiveCondition condition = _runtimeTutorialData.SO_Tasks[0]; // Get the objectives to the current task

        if (condition._isCompleted)
        {
            _runtimeTutorialData.ExecuteCondition(0); // Check the objective
            UpdateText(TutorialManager.S_Instance);
        }
        CheckCompletion();
    }

    private void HandleInspect(bool isInspecting) // Called by event
    {
        if (!isInspecting) return;

        ObjectiveCondition condition = _runtimeTutorialData.SO_Tasks[1]; // Get the objectives to the current task

        if (!condition._isCompleted)
        {
            _runtimeTutorialData.ExecuteCondition(1); // Check the objective
            UpdateText(TutorialManager.S_Instance);
        }
        CheckCompletion();
    }

    private void CheckCompletion() // Check if all conditions are complete
    {
        foreach (ObjectiveCondition condition in _runtimeTutorialData.SO_Tasks)
        {
            if (!condition._isCompleted) // if any task is not complete
            {
                return; // Stop here and dont continue
            }
        }
        ExitState(TutorialManager.S_Instance); // Else finish objective
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
