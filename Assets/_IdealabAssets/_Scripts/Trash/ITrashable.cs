using UnityEngine;

public interface ITrashable
{
    //bool IsTrashed(SortingCategory type);

    float Trashing(SortingCategory type);

    SortingCategory GetTrashType();
    AudioClip TrashingSound();
    void PickUpSound();
    void DropSound();
    SOTrashData TrashData();
}