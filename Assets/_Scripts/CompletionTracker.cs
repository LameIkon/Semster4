using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionTracker : MonoBehaviour
{
    public static event Action s_OnCompletion; // Look DoorOpener

    public static int s_TrashObjectsCount = 0;
    //private ISet<GameObject> _trashObjectsToTrack; //All trash objects that needs to be sorted before next stage, in Unity removing GameObjects from an IList or ISet is not possible with the .Remove(GameObject
    public static int s_targetCount;


    public void HandleRegisterTrash(GameObject go, float points)
    {
        s_TrashObjectsCount++;

        if(s_TrashObjectsCount >= s_targetCount)
        {
            s_OnCompletion?.Invoke();
        }
    }

    #region Unity Methods
    private IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        s_targetCount = GameObject.FindGameObjectsWithTag("Trash").Length;
    }

    private void OnEnable()
    {
        TrashBin.s_OnTrashedEvent += HandleRegisterTrash;
    }

    private void OnDisable() 
    {
        TrashBin.s_OnTrashedEvent -= HandleRegisterTrash;
    }
    #endregion
}
