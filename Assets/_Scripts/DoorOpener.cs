using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    private int _angle = 90;
    public float _DurationToOpen;
    private bool _isOpening = false;

    [SerializeField] private GameObject _door; //put in right door


    public void OpenDoor()
    {
        if (!_isOpening)
        {
            StartCoroutine(RotateDoor(_door.transform.rotation, Quaternion.Euler(0, _angle, 0), _DurationToOpen));
        }
    }

    IEnumerator RotateDoor(Quaternion startRotation, Quaternion endRotation, float duration)
    {
        _isOpening = true;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
           _door.transform.rotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;  
        }

        _door.transform.rotation = endRotation;
        _isOpening = false;
    }

    private void CompletionEvent()
    {
        Debug.Log("All trash objects have been sorted");
        OpenDoor(); 
    }

    public void OnEnable()
    {
        CompletionTracker.s_OnCompletion += CompletionEvent;
    }

    public void OnDisable()
    {
        CompletionTracker.s_OnCompletion -= CompletionEvent;
    }
}
