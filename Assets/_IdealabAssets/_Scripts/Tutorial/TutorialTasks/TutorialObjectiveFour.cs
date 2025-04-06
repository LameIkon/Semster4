using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Objective/Four")]
public class TutorialObjectiveFour : SOTutorialObjectiveBase
{
    public override void EnterState(TutorialManager manager)
    {
        PlayerVR.S_TestDoor += SetDoorOpenedState;
        base.EnterState(manager);
    }

    private void SetDoorOpenedState(bool isDoorEnter)
    {
        HandleTask(isDoorEnter, 0);
    }

    public override void ExitState(TutorialManager manager)
    {
        PlayerVR.S_TestDoor -= SetDoorOpenedState;
        base.ExitState(manager);
    }
}
