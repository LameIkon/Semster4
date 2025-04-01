using System;

[Serializable]
public class ObjectiveCondition
{
    public string _conditionDescription;
    public int _currentAmount;
    public int _requiredAmount;
    public bool _isCompleted;

    public virtual void Execute(TutorialManager manager){}
}