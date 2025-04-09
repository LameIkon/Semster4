using UnityEngine;

public interface ITrashable
{
    float Trashing(SortingCategory type);
    bool Vomit(SortingCategory type);
    SOTrashData TrashData();
    AudioClip TrashingSound();
    void PickUpSound();
}