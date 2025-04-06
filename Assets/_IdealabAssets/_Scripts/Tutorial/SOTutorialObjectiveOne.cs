using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/ObjectiveOne")]
public class SOTutorialObjectiveOne : SOTutorialObjectiveBase
{

    public override void EnterState(TutorialManager manager) // Start objective
    {     
        PlayerVR.OnGripStateChanged += SetGripState;
        PlayerVR.OnSelectStateChanged += SetInspectState;
        base.EnterState(manager);
    }

    private void SetGripState(bool isHolding)
    {
        HandleGripState(isHolding, 0);
    }

    private void SetInspectState(bool isInspecting)
    {
        HandleInspect(isInspecting, 1);
    }

    //public override void CompleteState(TutorialManager manager)
    //{
    //    Debug.Log("Completed Objective");
    //    //_completedPages.Add(_runtimeTutorialData.SO_Pages[_currentPage]);
    //    NextPage();

    //    if (_currentPage >= _runtimeTutorialData.SO_Pages.Count)
    //    {
    //        Debug.Log("No more pages, exiting state...");
    //        ExitState(manager); // Exit if no more content
    //    }
    //}

    public override void ExitState(TutorialManager manager) // Finish objective
    {
        PlayerVR.OnGripStateChanged -= SetGripState;
        PlayerVR.OnSelectStateChanged -= SetInspectState;
        base.ExitState(manager);
        
    }
}
