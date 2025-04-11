using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FloorTrigger : MonoBehaviour
{
   void OnTriggerEnter(Collider target)
   {
        if (target.TryGetComponent(out ITrashable trash) && trash != null)
        {
            Debug.Log(target.name + "eqwe");
            trash.DropSound();
            Debug.Log(target.name + "2222eqwe");
        }

   }

    private void Start() 
    {
        Reset();
    }

    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
}
