using System.Collections.Generic;
using UnityEngine;

public abstract class SOTutorialObjectiveBase : ScriptableObject
{
    [SerializeField] private List<SOObjectivePage> SO_Pages; // Original dataset
    protected List<SOObjectivePage> _runtimeTutorialData; // Cloned tutorial data
    protected int _currentPage;

    [Tooltip("Optional: Only needed if you want to spawn objects. You can leave this empty.")]
    [SerializeField] private List<GameObject> SO_trashToInstantiate; // Original list if need instantiate objects to the scene
    protected List<GameObject> SO_SpawnedTrashObjects = new(); // Clone of original. To track if any objects are missing. To prvent soft lock



    public virtual void EnterState(TutorialManager manager)
    {
        Debug.Log("Tutorial: Hold an Object");
        CloneData(); // Copy the scriptable objects that needs to be used
        SO_SpawnedTrashObjects.Clear(); // Clear list of tracked objects
        CreateObjects();
        UpdateText(manager); // Display/update UI
    }
    //public virtual void ExecuteState(TutorialManager manager) { }
    public virtual void CompleteState(TutorialManager manager)
    {
        Debug.Log("Completed Objective");
        NextPage();

        if (_currentPage >= SO_Pages.Count)
        {
            Debug.Log("No more pages, exiting state...");
            ExitState(manager); // Exit if no more content
        }
    }
    public virtual void ExitState(TutorialManager manager)
    {
        Debug.Log("Exit: Holding Object");
        manager.NextObjective(); // Start next objective
    }

    public void NextPage()
    {
        _currentPage++;
        if (_currentPage < _runtimeTutorialData.Count) // Check if there are more pages left
        {
            Debug.Log("Next page");
            UpdateText(TutorialManager.S_Instance);
            TutorialManager.S_Instance.ShowProgression();
        }
        else // If not then exit state
        {
            Debug.Log("No page left");
            ExitState(TutorialManager.S_Instance);
        }  
    }

    protected void UpdateText(TutorialManager manager)
    {
        SOObjectivePage currentPageData = _runtimeTutorialData[_currentPage];

        manager._Descriptiontext.text = currentPageData.SO_Description;
        if (!currentPageData.SO_KeepPreviousTaskDescription) // Keep or remove it. Done in inspector
        {
            manager._Objective.text = string.Empty;
        }
        
        if (!currentPageData.SO_KeepPreviousImage)
        {
            foreach (Transform child in manager._ImageHolder.transform) // Check for child of imageholder
            {
                Destroy(child.gameObject); // Destory it since we dont need it. (For future maybe look for a way to activate and deactivate instead of instantiate and destroy)
            }
        }

        if (currentPageData.SO_ImagePrefab != null) // If have Image create it and make it child of imageholder
        {
            Debug.Log("created image");
            GameObject image = Instantiate(currentPageData.SO_ImagePrefab); // Instantiate
            image.transform.SetParent(manager._ImageHolder.transform, false); // Set child to parent 
        }

        foreach (SOObjectiveCondition currentCondition in currentPageData.SO_Tasks)
        {
            string textColor = (currentCondition._currentAmount >= currentCondition._requiredAmount) ? "green" : "yellow"; // Decide the color. It knows what color just by using the tags

            manager._Objective.text += string.Format("<color={0}>{1}/{2}</color> {3}\n", // It takes the 3 below values and insert it in corresponding formation
                textColor, // Color of values (0)
                currentCondition._currentAmount, // Current amount (1)
                currentCondition._requiredAmount, // Max amount (2)
            currentCondition.SO_conditionDescription); // Task Description (3)
        }

        
        manager._ContinueButton.SetActive(!currentPageData.HasTasks()); // Show only if there are no tasks
    }

    public void CloneData()
    {
        _runtimeTutorialData = new List<SOObjectivePage>(); // Create new list of pages

        foreach (SOObjectivePage page in SO_Pages)
        {
            SOObjectivePage clonedPage = Instantiate(page); // Clone
            List<SOObjectiveCondition> clonedTasks = new List<SOObjectiveCondition>(); // Create new list of tasks

            foreach (SOObjectiveCondition task in page.SO_Tasks)
            {
                clonedTasks.Add(Instantiate(task));
            }

            clonedPage.SO_Tasks = clonedTasks;
            _runtimeTutorialData.Add(clonedPage);
        }
        _currentPage = 0; // Reset Page number
    }

    #region Tasks

    public void HandleTask(bool state, int taskIndex)
    {
        if (!state) return;

        SOObjectivePage currentPage = _runtimeTutorialData[_currentPage];

        if (currentPage.SO_Tasks.Count <= taskIndex) return;

        SOObjectiveCondition condition = currentPage.SO_Tasks[taskIndex]; // Get the objectives to the current task

        if (!condition._isCompleted)
        {
            condition.Execute();
            UpdateText(TutorialManager.S_Instance);
        }
        CheckCompletion();
    }

    public void ExecuteTask(int pageIndex, int conditionIndex)
    {
        if (_runtimeTutorialData[pageIndex].SO_Tasks[conditionIndex] != null)
        {
            _runtimeTutorialData[pageIndex].SO_Tasks[conditionIndex].Execute();
        }
    }

    protected void CheckCompletion() // Check if all conditions are complete
    {
        foreach (SOObjectiveCondition condition in _runtimeTutorialData[_currentPage].SO_Tasks)
        {
            if (!condition._isCompleted) // if any task is not complete
            {
                return; // Stop here and dont continue
            }
        }
        CompleteState(TutorialManager.S_Instance); // Complete objective
    }

    #endregion

    #region CreateObjects
    private void CreateObjects()
    {
        if (SO_trashToInstantiate == null) return; // Return if we dont have ny objects to check after

        Vector3 spawnPosition = TutorialManager.S_Instance.GetSpawnPosition();
        foreach (GameObject prefabObject in SO_trashToInstantiate)
        {
            GameObject spawned = Instantiate(prefabObject, spawnPosition, Quaternion.identity);
            SO_SpawnedTrashObjects.Add(spawned);
        }
    }

    public void CheckAndRestoreTrash(bool notUsedBool) // Should be called by event
    {
        if (SO_SpawnedTrashObjects == null) return; // Return if we dont have ny objects to check after

        bool allGone = true;
        Debug.Log(SO_SpawnedTrashObjects.Count);
        foreach (GameObject prefabObject in SO_SpawnedTrashObjects)
        {
            if (prefabObject != null) // Ignore null references - objecs become null when destroyed
            {
                allGone = false;
                break; // Stop if we find a value besides null
            }
        }

        if (allGone) // If we have no objects in tracked list
        {
            Debug.Log("spawn");
            SO_SpawnedTrashObjects.Clear(); // Clear old references
            CreateObjects();
        }
    }
    #endregion
}
