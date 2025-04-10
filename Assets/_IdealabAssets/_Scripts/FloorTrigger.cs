using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
   void OnTriggerEnter(Collider target)
   {
      Debug.Log(target.name + "eqwe");
      target.TryGetComponent(out ITrashable trash);
      trash.DropSound();
      Debug.Log(target.name + "2222eqwe");

   }
}
