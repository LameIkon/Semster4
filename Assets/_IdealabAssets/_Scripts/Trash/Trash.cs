using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody), typeof(AudioSource)),
 RequireComponent(typeof(XRGrabInteractable), typeof(XRGeneralGrabTransformer), typeof(TrashDescriptionUIEnable))]
public class Trash : MonoBehaviour, ITrashable
{
    [SerializeField] private SOTrashData _data; // All the data on the object is handle with a Flyweight pattern
    [SerializeField] private AudioSource _audioSource;

    public float Trashing(SortingCategory type)
    {
        if (_data == null) throw new NullReferenceException();

        if (type == _data.SO_PreferredCategory) // Will check the type it gets from the TrashBin to the Preferd and Okay types
        {
            return _data.SO_PreferredCategoryPoints;
        }

        if (type == _data.SO_AcceptableCategory)
        {
            return _data.SO_AcceptableCategoryPoints;
        }

        return _data.SO_WrongCategoryPoints;
    }

    public bool Vomit(SortingCategory type)
    {
        if (type == _data.SO_PreferredCategory || type == _data.SO_AcceptableCategory)
        {
            Destroy(gameObject);
            return false;
        }

        return true;
    }

    public AudioClip TrashingSound()
    {
        if (_data == null) 
        {
            Debug.LogError($"No data assigned {gameObject.name}");
        }

        if (_data.SO_DropInBin.Length == 0) 
        {
            return null; 
        }

        return _data.SO_DropInBin[Random.Range(0, _data.SO_DropInBin.Length)];
    }

    public void PickUpSound()
    {
        if (_data == null) throw new NullReferenceException();

        AudioClip pickUpClip = _data.SO_PickUp[UnityEngine.Random.Range(0, _data.SO_PickUp.Length)];
        _audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        _audioSource.clip = pickUpClip;
        _audioSource.Play();
    }

    public void DropSound()
    {
        if (_data == null) throw new NullReferenceException();

        AudioClip dropOnFloorClip = _data.SO_DropOnFloor[UnityEngine.Random.Range(0, _data.SO_DropOnFloor.Length)];
        _audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        _audioSource.clip = dropOnFloorClip;
        _audioSource.Play();
    }

    public SOTrashData TrashData()
    {
        if (_data == null)
        {
            Debug.LogError($"Data has not been assigned for this object: {gameObject}");
        }

        return _data;
    }


#region UnityMethods
    private void Awake()
    {
        gameObject.tag = "Trash";
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

#endregion
}