using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Objective/One")]
public class SOTutorialObjectiveOne : SOTutorialObjectiveBase
{
    [SerializeField] private List<GameObject> SO_trashToInstantiate;
    public override void EnterState(TutorialManager manager) // Start objective
    {     
        PlayerVR.S_OnGripStateChanged += SetGripState;
        CreateObjects();
        base.EnterState(manager);
    }

    private void SetGripState(bool isHolding)
    {
        HandleTask(isHolding, 0);
    }

    private void CreateObjects()
    {
        if (SO_trashToInstantiate == null) return;
        
        Vector3 spawnPosition = TutorialManager.S_Instance.GetSpawnPosition();
        foreach (GameObject prefabObject in SO_trashToInstantiate)
        {
            Instantiate(prefabObject, spawnPosition, Quaternion.identity);
        }       
    }

    public override void ExitState(TutorialManager manager) // Finish objective
    {
        PlayerVR.S_OnGripStateChanged -= SetGripState;
        base.ExitState(manager);
        
    }
}
