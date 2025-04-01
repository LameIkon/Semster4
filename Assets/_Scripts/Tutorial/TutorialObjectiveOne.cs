using UnityEngine;

public class TutorialObjectiveOne : TutorialObjectiveBase
{
    private int _currentHoldingObjects = 0;
    private const int _totalInspectedAmount = 3;

    public override void EnterState(TutorialManager manager) // Start objective
    {
        Debug.Log("Tutorial: Hold an Object");
        UpdateText(manager, _currentHoldingObjects, _totalInspectedAmount);

        PlayerVR.OnGripStateChanged += HandleGripStateChanged;
    }

    public override void ExecuteState(TutorialManager manager) // On progression on objective
    {
        //_currentHoldingObjects++;

        //if (_currentHoldingObjects <= _totalInspectedAmount)
        //{
        //    UpdateText(manager, _currentHoldingObjects, _totalInspectedAmount);
        //}
        //else
        //{
        //    if (_currentDataIndex <= SO_tutorialData.Length - 1)
        //    {
        //        _currentDataIndex++;
        //        UpdateText(manager, _currentHoldingObjects, _totalInspectedAmount);
        //        manager.ShowProgression();
        //    }
        //    else
        //    {
        //        ExitState(manager);
        //    }
        //}
    }
    private void HandleGripStateChanged(bool isHolding)
    {
        if (isHolding)
        {
            _currentHoldingObjects++;

            if (_currentHoldingObjects <= _totalInspectedAmount)
            {
                UpdateText(TutorialManager.S_Instance, _currentHoldingObjects, _totalInspectedAmount);
            }
            else
            {
                if (_currentDataIndex < SO_tutorialData.Length - 1)
                {
                    _currentDataIndex++;
                    UpdateText(TutorialManager.S_Instance, _currentHoldingObjects, _totalInspectedAmount);
                    TutorialManager.S_Instance.ShowProgression();
                }
                else
                {
                    ExitState(TutorialManager.S_Instance);
                }
            }
        }
    }

    public override void ExitState(TutorialManager manager) // Finish objective
    {
        Debug.Log("Completed: Holding Object");
        manager.ShowProgression();
        manager.NextObjective(manager._objectiveTwo); // Start next objective
    }
}
