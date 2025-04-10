using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Objective/Two")]
public class SOTutorialObjectiveTwo : SOTutorialObjectiveBase
{
    public override void EnterState(TutorialManager manager)
    {
        TrashBin.s_OnTrashedEvent3 += SetTrashingState;
        //PlayerVR.S_TestTrashing += SetTrashingState; // Test. Should be deleted later
        base.EnterState(manager);
    }

    private void SetTrashingState()
    {
        HandleTask(true, 0);
    }

    public override void ExitState(TutorialManager manager)
    {
        TrashBin.s_OnTrashedEvent3 -= SetTrashingState;
        //PlayerVR.S_TestTrashing -= SetTrashingState; // Test. Should be deleted later
        base.ExitState(manager);
    }
}
