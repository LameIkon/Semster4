using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Conditions/InspectObjectCondition")]
public class InspectObjectCondition : ObjectiveCondition
{
   public override void Execute()
   {
        if (PlayerVR.S_Instance.IsInspectingObject())
        {
            base.Execute();
        }
   }
}
