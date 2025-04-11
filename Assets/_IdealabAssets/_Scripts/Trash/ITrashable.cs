using UnityEngine;

public interface ITrashable
{
    //bool IsTrashed(SortingCategory type);

    float Trashing(SortingCategory type);

    SortingCategory GetTrashType();
    AudioClip TrashingSound();

    void DestroyTrashOnTrashing();
    void PickUpSound();
    void DropSound();
    SOTrashData TrashData();
}