using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/ObjectivePage")]
public class SOObjectivePage : ScriptableObject
{
    [TextArea(3, 15)] public string SO_Description; // The text for this page
    public List<SOObjectiveCondition> SO_Tasks; // Tasks tied to this page
    public GameObject SO_ImagePrefab; // To show Images
    public bool SO_KeepPreviousTaskDescription; // If want to keep previous tasks
    public bool SO_KeepPreviousImage; // If want to keep previous image
    public bool SO_KeepObjects; // If want to keep created objects from task.
    public bool SO_CheckCompletionOnEnterState;

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
