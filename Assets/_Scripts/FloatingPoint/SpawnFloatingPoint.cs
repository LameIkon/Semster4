using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnFloatingPoint : MonoBehaviour
{
    public GameObject _FloatingPoint; // Declaring our GameObject. Attach this script to the object which should be able to instantiate FloatingPoints.
    [SerializeField] private float _pointPosition;

    private void OnEnable()
    {
        TrashBin.OnTrashedEvent += HandleTrashEvent;   
    }

    private void OnDisable()
    {
        TrashBin.OnTrashedEvent -= HandleTrashEvent;
    }

    private void HandleTrashEvent(GameObject sender, float points)
    {
        if ( _FloatingPoint != null )
        {       
            GameObject go = Instantiate(_FloatingPoint, sender.transform.position+Vector3.up*_pointPosition, Quaternion.Euler(0, 90, 0), sender.transform);      // Instantiates the FloatingPoint. Becomes a child of parent object.
            go.GetComponent<TextMeshPro>().text = points.ToString();                                              // <- Variable for points goes here.
        }
       
    }
}
