using System.Collections.Generic;
using System;
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
        if (SO_Objectives[conditionIndex] != null)
        {
            SO_Objectives[conditionIndex].Execute(manager);
        }
    }

}

