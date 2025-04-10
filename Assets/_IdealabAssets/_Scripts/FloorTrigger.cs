using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FloorTrigger : MonoBehaviour
{
   void OnTriggerEnter(Collider target)
   {
      Debug.Log(target.name + "eqwe");
      target.TryGetComponent(out ITrashable trash);
      trash.DropSound();
      Debug.Log(target.name + "2222eqwe");

   }

    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
}
