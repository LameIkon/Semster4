using System;
using UnityEngine;

public abstract class ObjectiveCondition : ScriptableObject
{
    public string _conditionDescription;
    public int _currentAmount;
    public int _requiredAmount;
    public bool _isCompleted;

    public virtual void Execute()
    {
        _currentAmount++;
        if (_currentAmount >= _requiredAmount)
        {
            _isCompleted = true;
        }
    }
}

