using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TrashBin : MonoBehaviour
{
	/* The implementation of the TrashBin handles the sending of data to the TrashManager,
	 * It is also responsible for calling the Trashing method on the ITrashables
	 */

	[SerializeField]
	private SOTrashBinData _binData; // The Trash type comes from the SO 

	private BoxCollider _boxCollider;

	public float _Points; // Purely for debugging

	public static event Action<float> OnTrashedEvent; // The event that connects to the TrashManager

	
	private void OnTriggerEnter(Collider target) 
	{
		if (_binData != null)
		{
			float? points = target.GetComponent<ITrashable>()?.Trashing(_binData._AllowedType); // Check if the GameObject entering the Trigger has an ITrashable
																								  // and calling the Trashing method, the method returns a float
			if (points != null) 
			{
				_Points += (float)points; // Purely for debugging
				OnTrashedEvent.Invoke((float)points); // Casts the points as a float and invokes the OnTrashedEvent
				HighscoreTable.UpdateHighScorePoints(points);
				HighscoreTable.DisplayErrorMessage(points, "apple", "fruit bin");
			}
		}
	}

	// Makes sure that the BoxCollider is a set to a Trigger
	public void Reset()
	{
		GetComponent<BoxCollider>().isTrigger = true;
	}

}
