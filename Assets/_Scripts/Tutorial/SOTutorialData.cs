using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Data")]
public class SOTutorialData : ScriptableObject
{
    [Header("Description")]
    [TextArea(3, 15)] public string[] SO_Description;

    [Space(10)]
    public bool SO_ShowContinueButton;
    public List<ObjectiveCondition> SO_Tasks; // The tasks connected to the objective
    public int SO_TaskIndex; // Assign in inspector what index the tasks should show up.

    public void ExecuteCondition(int conditionIndex)
    {
        if (SO_Tasks[conditionIndex] != null)
        {
            SO_Tasks[conditionIndex].Execute();
        }
    }
}

