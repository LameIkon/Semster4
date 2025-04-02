using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/ObjectivePage")]
public class SOObjectivePage : ScriptableObject
{
    [TextArea(3, 15)] public string SO_Description; // The text for this page
    public List<ObjectiveCondition> SO_Tasks; // Tasks tied to this page

    public bool HasTasks()
    {
        if (SO_Tasks != null && SO_Tasks.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
