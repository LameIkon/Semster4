using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider), typeof(Animator), typeof(AudioSource))]
public class TrashBin : MonoBehaviour
{
	/* The implementation of the TrashBin handles the sending of data to the TrashManager,
	 * It is also responsible for calling the Trashing method on the ITrashables
	 */

	[SerializeField]
	private SOTrashBinData _binData; // The Trash type comes from the SO 

	public GameObject _FloatingPoint;

	private BoxCollider _boxCollider;

	public float _Points; // Purely for debugging

	public static event Action<GameObject, float> OnTrashedEvent; // The event that connects to the TrashManager

	private Animator _animator;
	private AudioSource _audioSource;

	private void Start()
	{
		if (gameObject.TryGetComponent(out Animator animator)) // if component exist get
		{
			_animator = animator;
		}
		_audioSource = GetComponent<AudioSource>();
		_audioSource.playOnAwake = false;
		_audioSource.spatialBlend = 1f;
	}

	private void OnTriggerEnter(Collider target)
	{
		if (_binData != null)
		{
			ITrashable trash = target.GetComponent<ITrashable>();

			float? points = trash.Trashing(_binData._AllowedType); // Check if the GameObject entering the Trigger has an ITrashable
																   // and calling the Trashing method, the method returns a float
			if (points != null)
			{
				_Points += (float)points; // Purely for debugging
				OnTrashedEvent.Invoke(gameObject, (float)points); // Casts the points as a float and invokes the OnTrashedEvent
				Debug.Log(gameObject);

				HighscoreTable.UpdateHighScorePoints(points);

				SOTrashData trashData = target.GetComponent<Trash>()?._data;
				if (trashData != null)
				{
					Infoboard.DisplayInfoMessage(points, trashData);
				}

				EnablePolish(points);
			}

			AudioClip clip = trash.TrashingSound();
			if (clip != null)
			{
				PlayTrashSound(clip);
			}

		}
	}

	private void PlayTrashSound(AudioClip clip) 
	{
		_audioSource.clip = clip;
		_audioSource.Play();
	}

	private void EnablePolish(float? points) // Checks if the value is positive or negative
	{
		if (_animator != null)
		{
			_animator.Play(points >= 0 ? "ExpandCorrect" : "ExpandIncorrect"); // Play corresponding animation
		}
	}


    private void HandleTrashEvent(float points)
    {
        if (_FloatingPoint != null)
        {
            GetComponent<TrashBin>();

            GameObject go = Instantiate(_FloatingPoint, transform.position + Vector3.up*3, Quaternion.identity, transform);      // Instantiates the FloatingPoint. Becomes a child of parent object.
            go.GetComponent<TextMeshPro>().text = points.ToString();                                              // <- Variable for points goes here.
        }

    }

	#region UnityMethods
	// Makes sure that the BoxCollider is a set to a Trigger
	public void Reset()
	{
		GetComponent<BoxCollider>().isTrigger = true;
		AudioSource source = GetComponent<AudioSource>();
	}

    #endregion
}
