using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Conditions/HoldObjectCondition")]
public class HoldObjectCondition : ObjectiveCondition
{
    public override void Execute()
    {
        if (PlayerVR.S_Instance.IsHoldingObject())
        {
            base.Execute();
        }
    }
}