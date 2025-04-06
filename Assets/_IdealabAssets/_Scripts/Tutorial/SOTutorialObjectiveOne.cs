using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Objective/One")]
public class SOTutorialObjectiveOne : SOTutorialObjectiveBase
{

    public override void EnterState(TutorialManager manager) // Start objective
    {     
        PlayerVR.OnGripStateChanged += SetGripState;
        base.EnterState(manager);
    }

    private void SetGripState(bool isHolding)
    {
        HandleTask(isHolding, 0);
    }

    public override void ExitState(TutorialManager manager) // Finish objective
    {
        PlayerVR.OnGripStateChanged -= SetGripState;
        base.ExitState(manager);
        
    }
}
