using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CocPlane : MonoBehaviour
{
    private BoxCollider _cocPlane;
    [SerializeField] private Transform _spawnTransform;

#region Unity Methods
    private void OnTriggerEnter(Collider target)
    {
        if (_spawnTransform == null)
        {
            Debug.LogError("_spawnTransform not set!");
            return;
        }

        if (target.GetComponent<ITrashable>() == null)
        {
            Debug.LogError($"Not a TrashObject that fell though the floor {target.name}");
            return;
        }

        Transform trashTransform = target.gameObject.GetComponent<Transform>();
        ResetPosition(trashTransform);
    }


    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
#endregion

    private void ResetPosition(Transform trashObject)
    {
        trashObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        trashObject.position = _spawnTransform.position;
        trashObject.rotation = _spawnTransform.rotation;
    }
}