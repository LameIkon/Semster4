using System;
using UnityEngine;

public abstract class ObjectiveCondition : ScriptableObject
{
    public string _conditionDescription;
    public int _currentAmount;
    public int _requiredAmount;
    public bool _isCompleted;

    public abstract void Execute();
}