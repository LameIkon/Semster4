using UnityEngine;
public interface ITrashable
{
	float Trashing(SortingCategory type);

	AudioClip TrashingSound();
	void PickUpSound();
}