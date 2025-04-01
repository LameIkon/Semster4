using UnityEngine;

public class HoldObjectCondition : ObjectiveCondition
{
    public override void Execute(TutorialManager manager)
    {
        if (PlayerVR.S_Instance.IsHoldingObject())
        {
            _currentAmount++;
            if (_currentAmount >= _requiredAmount)
            {
                _isCompleted = true;
            }
        }
    }
}