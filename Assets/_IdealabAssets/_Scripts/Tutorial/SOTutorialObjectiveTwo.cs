using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Objective/Two")]
public class SOTutorialObjectiveTwo : SOTutorialObjectiveBase
{
    public override void EnterState(TutorialManager manager)
    {
        TrashBin.s_OnTrashedEvent3 += SetTrashState;
        PlayerVR.S_TestTrashing += SetTrashState; // Test. Should be deleted later
        base.EnterState(manager);
    }

    private void SetTrashState(bool isTrashing)
    {
        HandleTask(isTrashing, 0);
    }

    public override void ExitState(TutorialManager manager)
    {
        TrashBin.s_OnTrashedEvent3 -= SetTrashState;
        PlayerVR.S_TestTrashing -= SetTrashState; // Test. Should be deleted later
        base.ExitState(manager);
    }
}
