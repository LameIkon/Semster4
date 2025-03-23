using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionTracker : MonoBehaviour
{
    public static event Action s_OnCompletion; // Look DoorOpener

    public static int s_TrashObjectsCount = 0;
    public static int s_targetCount;


    private void HandleRegisterTrash(GameObject go, float points)
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
        yield return null;
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
