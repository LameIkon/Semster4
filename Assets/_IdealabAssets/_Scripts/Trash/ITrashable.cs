using UnityEngine;

public interface ITrashable
{
    float Trashing(SortingCategory type);
    bool Vomit(SortingCategory type);
    AudioClip TrashingSound();
    void PickUpSound();
    void DropSound();
    SOTrashData TrashData();
}