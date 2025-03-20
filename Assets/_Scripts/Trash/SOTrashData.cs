using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Trash/Data")]
public class SOTrashData : ScriptableObject
{
	[Header("Description")]
	public string SO_Name;
	[TextArea(3, 15)] public string SO_Description;

	[Header("Sorting Category"), Space(2)]
	public SortingCategory SO_PreferdType;		// Preferd trash type, this is implemented as this to make it more dynamic and scalable
	public float SO_PreferdTypePoints;

	[Space()]
	public SortingCategory SO_AcceptableType;
	public float SO_AcceptableTypePoints;

	[Space()]
	public float SO_WrongTypePoints;
	[TextArea(3,15)] public string InfoIfSortedCorrectly;
    [TextArea(3, 15)] public string InfoIfSortedWrongly;

	[Space()]
	public AudioClip SO_TrashAudioClip;
	public AudioClip SO_PickUpAudioClip;
}









// The code below should be implemented instead of the above,
// for now it is commented to avoid merging issues.
//
// See also: [1]

/*
public class SOTrashData : ScriptableObject
{
	[Header("Description")]
	[SerializeField] private string _name;
	[SerializeField, TextArea(3, 10)] public string _Description;

	[Header("Sorting Category"), Space(2)]
	public SortingCategory _PreferredCategory;		// Preferred trash category, this is implemented as this to make it more dynamic and scalable
	public float _PreferredCategoryPoints;

	[Space()]
	public SortingCategory _AcceptableCategory/Categories;     // [1] Could also be an Array or a List
	public float _AcceptableCategoryPoints;

	[Space()]
	public float _WrongCategoryPoints;
}
*/











