using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName =("Trash/CanContainer"))]
public class SOTrashBinContainer : ScriptableObject
{
	[Header("Allowed Trash")]
	public TrashType _AllowedType; // Used to determine which trash goes in to the bin.

}
