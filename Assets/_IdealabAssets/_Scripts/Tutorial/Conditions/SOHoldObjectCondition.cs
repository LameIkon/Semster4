using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Conditions/HoldObjectCondition")]
public class SOHoldObjectCondition : SOObjectiveCondition
{
    public override void Execute()
    {
        if (PlayerVR.S_Instance.IsHoldingObject())
        {
            base.Execute();
        }
    }
}