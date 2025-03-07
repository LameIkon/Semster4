using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloatingPoint : MonoBehaviour
{
    public GameObject _FloatingPoint; // Declaring our GameObject. Attach this script to the object which should be able to instantiate FloatingPoints.
    

    public void ShowPoints()
    {
        if ( _FloatingPoint != null )
        {
            TrashBin trashBinScript = GetComponent<TrashBin>();

            var go = Instantiate(_FloatingPoint, transform.position, Quaternion.identity, transform);      // Instantiates the FloatingPoint. Becomes a child of parent object.
            // go.GetComponent<TextMesh>().text = ***                                              // <- Variable for points goes here.
        }
       
    }


}
