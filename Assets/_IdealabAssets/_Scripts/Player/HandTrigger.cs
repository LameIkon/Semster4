using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrigger : MonoBehaviour
{
    [SerializeField] private bool _isRightTrigger;
    [SerializeField] private bool _isLeftTrigger;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.GetComponent<ITrashable>() != null)
        {
            if (_isRightTrigger)
            {
                PlayerVR.S_Instance.OnObjectRightTriggerEnter(other);
            }
            else if (_isLeftTrigger)
            {
                PlayerVR.S_Instance.OnObjectLeftTriggerEnter(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ITrashable>() != null)
        {
            if (_isRightTrigger) 
            {
                PlayerVR.S_Instance.OnObjectRightTriggerExit();
            }
            else if (_isLeftTrigger)
            {
                PlayerVR.S_Instance.OnObjectLeftTriggerExit();
            }
        }
    }
}