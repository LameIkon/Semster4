using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Conditions/HoldObjectCondition")]
public class HoldObjectCondition : ObjectiveCondition
{
    public override void Execute()
    {
        Debug.Log("test");
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