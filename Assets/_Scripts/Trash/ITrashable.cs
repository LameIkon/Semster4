#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using UnityEngine;
public interface ITrashable
{
    float Trashing(SortingCategory type);
    SOTrashData TrashData();
	AudioClip TrashingSound();
	void PickUpSound();
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member