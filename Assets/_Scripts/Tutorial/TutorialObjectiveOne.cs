using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/ObjectiveOne")]
public class TutorialObjectiveOne : SOTutorialObjectiveBase
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

        SOObjectivePage currentPage = _runtimeTutorialData.SO_Pages[_currentPage];

        if (currentPage.SO_Tasks.Count == 0) return;

        SOObjectiveCondition condition = currentPage.SO_Tasks[0];

        if (!condition._isCompleted)
        {
            condition.Execute();
            UpdateText(TutorialManager.S_Instance);
        }
        CheckCompletion();
    }

    private void HandleInspect(bool isInspecting) // Called by event
    {
        if (!isInspecting) return;

        SOObjectivePage currentPage = _runtimeTutorialData.SO_Pages[_currentPage];

        if (currentPage.SO_Tasks.Count == 0) return;

        SOObjectiveCondition condition = currentPage.SO_Tasks[1]; // Get the objectives to the current task

        if (!condition._isCompleted)
        {
            condition.Execute();
            UpdateText(TutorialManager.S_Instance);
        }
        CheckCompletion();
    }

    public override void CompleteState(TutorialManager manager)
    {
        Debug.Log("Completed Objective");
        //_completedPages.Add(_runtimeTutorialData.SO_Pages[_currentPage]);
        NextPage();

        if (_currentPage >= _runtimeTutorialData.SO_Pages.Count)
        {
            Debug.Log("No more pages, exiting state...");
            ExitState(manager); // Exit if no more content
        }
    }

    public override void ExitState(TutorialManager manager) // Finish objective
    {
        Debug.Log("Exit: Holding Object");
        PlayerVR.OnGripStateChanged -= HandleGripStateChanged;
        PlayerVR.OnSelectStateChanged -= HandleInspect;
        manager.NextObjective(); // Start next objective
    }
}
