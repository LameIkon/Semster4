using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Data")]
public class SOTutorialData : ScriptableObject
{

    //public bool SO_ShowContinueButton;
    //public List<ObjectiveCondition> SO_Tasks; // The tasks connected to the objective
    //public int SO_TaskIndex; // Assign in inspector what index the tasks should show up.
    public List<SOObjectivePage> SO_Pages;

    public void ExecuteCondition(int pageIndex, int conditionIndex)
    {
        if (SO_Pages[pageIndex].SO_Tasks[conditionIndex] != null)
        {
            SO_Pages[pageIndex].SO_Tasks[conditionIndex].Execute();
        }
    }
}

