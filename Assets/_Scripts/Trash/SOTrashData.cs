using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Trash/Data")]
public class SOTrashData : ScriptableObject
{
	[Header("Description")]
	[SerializeField] private string _Name;
	[SerializeField, TextArea(3, 10)] private string _Description;

	[Header("Sorting Category"), Space(2)]
	public SortingCategory _PreferdType;		// Preferd trash type, this is implemented as this to make it more dynamic and scalable
	public float _PreferdTypePoints;

	[Space()]
	public SortingCategory _AcceptableType;
	public float _AcceptableTypePoints;

	[Space()]
	public float _WrongTypePoints;

}
