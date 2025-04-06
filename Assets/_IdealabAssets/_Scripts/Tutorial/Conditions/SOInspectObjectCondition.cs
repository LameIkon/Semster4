using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Conditions/InspectObjectCondition")]
public class SOInspectObjectCondition : SOObjectiveCondition
{
   public override void Execute()
   {
        if (PlayerVR.S_Instance.IsInspectingObject())
        {
            base.Execute();
        }
   }
}
