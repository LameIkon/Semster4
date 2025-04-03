using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Trash/Data")]
public class SOTrashData : ScriptableObject
{
	[Header("Description")]
	public string SO_Name;
	[TextArea(3, 15)] public string SO_Description;

	[Space(20), Header("Sorting Category")]
	[FormerlySerializedAs("SO_PreferdType")] 
	public SortingCategory SO_PreferredCategory;		// Preferred trash category, this is implemented as this to make it more dynamic and scalable
	[FormerlySerializedAs("SO_PreferdTypePoints")] public float SO_PreferredCategoryPoints;

	[Space(10)]
	[FormerlySerializedAs("SO_AcceptableType")] public SortingCategory SO_AcceptableCategory;
	[FormerlySerializedAs("SO_AcceptableTypePoints")] public float SO_AcceptableCategoryPoints;

	[Space(10)]
	[FormerlySerializedAs("SO_WrongTypePoints")] public float SO_WrongCategoryPoints;

    [Space(20), Header("Sorting Information")]
    [TextArea(2, 5)] public string InfoIfSortedCorrectly;
    [TextArea(2, 5)] public string InfoIfSortedWrongly;
    [FormerlySerializedAs("InfoIfSortedAcceptable")] [TextArea(2, 15)] public string InfoIfSortedAcceptably;
    
    // TODO: Convert to arrays of AudioClips 
    [Space(20), Header("Audio")]
    [FormerlySerializedAs("SO_TrashAudioClip")] public AudioClip SO_DropInBinAudio;
    [FormerlySerializedAs("SO_DropOnFloorAudioClip")] public AudioClip SO_DropOnFloorAudio;
	[FormerlySerializedAs("SO_PickUpAudioClip")] public AudioClip SO_PickUpAudio;
}