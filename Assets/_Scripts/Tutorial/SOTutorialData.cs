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
    public List<ObjectiveCondition> SO_Tasks; 

    public void ExecuteCondition(int conditionIndex)
    {
        if (SO_Tasks[conditionIndex] != null)
        {
            SO_Tasks[conditionIndex].Execute();
        }
    }
}

