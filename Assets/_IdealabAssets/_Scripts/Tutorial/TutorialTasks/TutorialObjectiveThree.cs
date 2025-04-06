using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Objective/Three")]
public class TutorialObjectiveThree : SOTutorialObjectiveBase
{
    public override void EnterState(TutorialManager manager)
    {
        PlayerVR.S_OnSelectStateChanged += SetInspectState;
        TrashBin.s_OnTrashedEvent3 += SetTrashingState;
        PlayerVR.S_TestTrashing += SetTrashingState; // Test. Should be deleted later
        base.EnterState(manager);
    }

    private void SetInspectState(bool isInspecting)
    {
        HandleTask(isInspecting, 1);
    }

    private void SetTrashingState(bool isTrashing)
    {
        HandleTask(isTrashing, 0);
    }


    public override void ExitState(TutorialManager manager)
    {
        PlayerVR.S_OnSelectStateChanged -= SetInspectState;
        TrashBin.s_OnTrashedEvent3 -= SetTrashingState;
        PlayerVR.S_TestTrashing -= SetTrashingState; // Test. Should be deleted later
        base.ExitState(manager);
    }
}
