using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionTracker : MonoBehaviour
{
    public static event Action OnCompletion; // Look trashManager

    public static int _TrashObjectsCount = 0;
    [SerializeField] private List<GameObject> _trashObjectsToTrack; //All trash objects that needs to be sorted before next stage
    public static int _targetCount;

    private void Start()
    {
        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("Trash");
        _trashObjectsToTrack = new List<GameObject>(trashObjects);
        _targetCount = _trashObjectsToTrack.Count;
    }

    public void RegisterTrash(GameObject go, float points)
    {
        _TrashObjectsCount++;

        _trashObjectsToTrack.Remove(go);


        if(_TrashObjectsCount >= _targetCount)
        {
            OnCompletion?.Invoke();
        }
    }

    private void OnEnable()
    {
        TrashBin.OnTrashedEvent += RegisterTrash;
    }

    private void OnDisable() 
    {
        TrashBin.OnTrashedEvent -= RegisterTrash;
    }
}
