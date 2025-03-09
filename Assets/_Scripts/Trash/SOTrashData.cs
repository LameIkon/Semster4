using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Trash/Data")]
public class SOTrashData : ScriptableObject
{
	[Header("Description")]
	public string _Name;
	[TextArea(3, 15)] public string _Description;

	[Header("Sorting Category"), Space(2)]
	public SortingCategory _PreferdType;		// Preferd trash type, this is implemented as this to make it more dynamic and scalable
	public float _PreferdTypePoints;

	[Space()]
	public SortingCategory _AcceptableType;
	public float _AcceptableTypePoints;

	[Space()]
	public float _WrongTypePoints;

	[Space()]
	public AudioClip _TrashAudioClip;

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
	[SerializeField, TextArea(3, 10)] private string _description;

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











