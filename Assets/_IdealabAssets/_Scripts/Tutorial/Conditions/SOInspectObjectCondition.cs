using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Conditions/InspectObjectCondition")]
public class SOInspectObjectCondition : SOObjectiveCondition
{
    private HashSet<GameObject> _inspectedObjects = new HashSet<GameObject>();

    public override void Execute()
   {
        if (PlayerVR.S_Instance.IsInspectingObject())
        {
            //_inspectedObjects.Add(PlayerVR.S_Instance.IsInspectingObject());
            base.Execute();
        }
   }
}
