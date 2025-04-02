using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Trash/Data")]
public class SOTrashData : ScriptableObject
{
	[Space(10), Header("Description")]
	public string SO_Name;
	[TextArea(3, 15)] public string SO_Description;

	[Space(10), Header("Sorting Category")]
	[FormerlySerializedAs("SO_PreferdType")] 
	public SortingCategory SO_PreferredCategory;		// Preferred trash category, this is implemented as this to make it more dynamic and scalable

	[FormerlySerializedAs("SO_PreferdTypePoints")] 
	public float SO_PreferredCategoryPoints;

	[Space(10)]
	[FormerlySerializedAs("SO_AcceptableType")] 
	public SortingCategory SO_AcceptableCategory;
	
	[FormerlySerializedAs("SO_AcceptableTypePoints")] 
	public float SO_AcceptableCategoryPoints;

	[Space(10)]
	[FormerlySerializedAs("SO_WrongTypePoints")] 
	public float SO_WrongCategoryPoints;

    [Space(10), Header("Sorting Information")]
    [TextArea(3, 15)] public string InfoIfSortedCorrectly;
    [TextArea(3, 15)] public string InfoIfSortedWrongly;
    
    [FormerlySerializedAs("InfoIfSortedAcceptable")] 
    [TextArea(3, 15)] public string InfoIfSortedAcceptably;

    [Space(10), Header("Audio")]
	public AudioClip SO_TrashAudioClip;
	public AudioClip SO_PickUpAudioClip;
	public AudioClip SO_DropOnFloorAudioClip;
}




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
	public SortingCategory _AcceptableCategory/Categories;     
	public float _AcceptableCategoryPoints;

	[Space()]
	public float _WrongCategoryPoints;
}
*/











