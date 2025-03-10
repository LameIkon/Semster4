using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionTracker : MonoBehaviour
{
    public static event Action OnCompletion; // Look trashManager

    public static int _TrashObjectsCount = 0;
    [SerializeField] private GameObject[] _trashObjectsToTrack; //All trash objects that needs to be sorted before next stage
    public static int _targetCount;

    private void Awake()
    {
        _targetCount = _trashObjectsToTrack.Length;
    }

    public static void RegisterTrash()
    {
        _TrashObjectsCount++;

        if(_TrashObjectsCount >= _targetCount)
        {
            OnCompletion?.Invoke();
        }
    }
}
