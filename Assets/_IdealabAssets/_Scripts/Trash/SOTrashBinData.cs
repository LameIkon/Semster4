using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName =("Trash/BinData"))]
public class SOTrashBinData : ScriptableObject
{
	[Header("Allowed Trash")]
	public SortingCategory _AllowedType; // Used to determine which trash goes in to the bin.
}

