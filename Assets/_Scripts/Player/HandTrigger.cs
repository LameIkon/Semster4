using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.GetComponent<ITrashable>() != null)
        {
            PlayerVR.S_Instance.OnObjectTriggerEnter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ITrashable>() != null)
        {
            PlayerVR.S_Instance.OnObjectTriggerExit();
        }
    }
}
