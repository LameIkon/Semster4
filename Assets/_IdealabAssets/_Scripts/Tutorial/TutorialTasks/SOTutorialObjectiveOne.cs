using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Objective/One")]
public class SOTutorialObjectiveOne : SOTutorialObjectiveBase
{
    public override void EnterState(TutorialManager manager) // Start objective
    {
        PlayerVR.S_OnGripStateChanged += SetGripState;
        PlayerVR.S_TestTrashing += CheckAndRestoreTrash;       
        base.EnterState(manager);
    }

    private void SetGripState(bool isHolding)
    {
        HandleTask(isHolding, 0);
    }

    public override void ExitState(TutorialManager manager) // Finish objective
    {
        PlayerVR.S_OnGripStateChanged -= SetGripState;
        PlayerVR.S_TestTrashing -= CheckAndRestoreTrash;
        base.ExitState(manager);
        
    }
}
