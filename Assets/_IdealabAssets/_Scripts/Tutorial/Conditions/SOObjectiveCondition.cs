using UnityEngine;

public abstract class SOObjectiveCondition : ScriptableObject
{
    public string SO_conditionDescription;
    public int _currentAmount;
    public int _requiredAmount;
    public bool _isCompleted; //{ get; private set; } // Used to check if task gets completed.

    public virtual void Execute()
    {
        _currentAmount++;
        if (_currentAmount >= _requiredAmount)
        {
            _isCompleted = true;
        }
    }
}

