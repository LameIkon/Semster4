using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Objective/One")]
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
        HandleTask(isHolding, 0);
    }

    private void SetInspectState(bool isInspecting)
    {
        HandleTask(isInspecting, 1);
    }

    public override void ExitState(TutorialManager manager) // Finish objective
    {
        PlayerVR.OnGripStateChanged -= SetGripState;
        PlayerVR.OnSelectStateChanged -= SetInspectState;
        base.ExitState(manager);
        
    }
}
