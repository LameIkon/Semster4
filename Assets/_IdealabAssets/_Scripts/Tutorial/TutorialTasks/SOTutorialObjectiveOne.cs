using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Objective/One")]
public class SOTutorialObjectiveOne : SOTutorialObjectiveBase
{
    [SerializeField] private List<GameObject> SO_trashToInstantiate; // Original list
    private List<GameObject> SO_SpawnedTrashObjects = new(); // Clone of original. To track if any objects are missing. To prvent soft lock
    public override void EnterState(TutorialManager manager) // Start objective
    {
        PlayerVR.S_OnGripStateChanged += SetGripState;
        PlayerVR.S_TestTrashing += CheckAndRestoreTrash;
        SO_SpawnedTrashObjects.Clear();
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
            GameObject spawned = Instantiate(prefabObject, spawnPosition, Quaternion.identity);
            SO_SpawnedTrashObjects.Add(spawned);
        }
    }

    private void CheckAndRestoreTrash(bool notUsedBool)
    {
        bool allGone = true;
        Debug.Log(SO_SpawnedTrashObjects.Count);
        foreach (GameObject prefabObject in SO_SpawnedTrashObjects)
        {
            if (prefabObject != null)
            {
                allGone = false;
                break;
            }
        }

        if (allGone)
        {
            Debug.Log("spawn");
            SO_SpawnedTrashObjects.Clear(); // Clear old references
            CreateObjects();
        }
    }

    public override void ExitState(TutorialManager manager) // Finish objective
    {
        PlayerVR.S_OnGripStateChanged -= SetGripState;
        PlayerVR.S_TestTrashing -= CheckAndRestoreTrash;
        base.ExitState(manager);
        
    }
}
