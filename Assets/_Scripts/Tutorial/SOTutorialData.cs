using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Data")]
public class SOTutorialData : ScriptableObject
{
    [Header("Description")]
    [TextArea(3, 15)] public string SO_Description;

    [Space(10)]
    public bool SO_ShowContinueButton;
    public List<ObjectiveCondition> SO_Objectives;

    public void ExecuteCondition(TutorialManager manager, int conditionIndex)
    {
        Debug.Log("try execute");
        if (SO_Objectives[conditionIndex] != null)
        {
            Debug.Log($"execute {SO_Objectives[conditionIndex]._conditionDescription}");
            SO_Objectives[conditionIndex].Execute();
            Debug.Log("test after execute");
        }
    }
}

