using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Trash/Container")]
public class SOTrashContainer : ScriptableObject
{
	[Header("Description")]
	public string _Name;
	[TextArea(3, 10)]
	public string _Description;

	[Header("Trash Types"), Space(2)]
	public TrashType _PreferdType;		// Preferd trash type, this is implemented as this to make it more dynamic and scalable
	public float _PreferdTypePoints;

	[Space()]
	public TrashType _OkayType;
	public float _OkayTypePoints;

	[Space()]
	public float _WrongTypePoints;

}
