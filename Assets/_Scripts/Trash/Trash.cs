using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody), typeof(AudioSource)), 
	RequireComponent(typeof(XRGrabInteractable), typeof(XRGeneralGrabTransformer), typeof(TrashDescriptionUIEnable))]
public class Trash : MonoBehaviour, ITrashable
{

	[SerializeField] private SOTrashData _data; // All the data on the object is handle with a Flyweight pattern
	[SerializeField] private AudioSource _audioSource;

	public float Trashing(SortingCategory type)
	{
		if (_data == null) throw new NullReferenceException();
		Destroy(gameObject);
		
		if (type == _data.SO_PreferredCategory)	// Will check the type it gets from the TrashBin to the Preferd and Okay types
		{
			return _data.SO_PreferredCategoryPoints;
		}
		else if (type == _data.SO_AcceptableCategory)
		{
			return _data.SO_AcceptableCategoryPoints;
		}

		return _data.SO_WrongCategoryPoints;

	}

	public AudioClip TrashingSound() 
	{
		if ( _data == null) throw new NullReferenceException();

		return _data.SO_DropInBinAudio;
	}

	public void PickUpSound()
	{
		if (_data == null) throw new NullReferenceException();

		if (_data.SO_PickUpAudio != null) 
		{
			_audioSource.clip = _data.SO_PickUpAudio;
			_audioSource.Play();
		}
	}

	public void DropSound()
	{
        if (_data == null) throw new NullReferenceException();

        if (_data.SO_PickUpAudio != null)
        {
            _audioSource.clip = _data.SO_DropOnFloorAudio;
            _audioSource.Play();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.CompareTag("Floor"))
		{
			DropSound();
		}
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
		if (_data.SO_PickUpAudio != null)
		{
			_audioSource.clip = _data.SO_PickUpAudio;
		}
	}

    #endregion



}
