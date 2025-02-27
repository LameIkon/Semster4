using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrashData", menuName = "Trash Item")]
public class TrashData : ScriptableObject
{
    [SerializeField] private string          _trashName;
    [SerializeField] private ushort          _pointValue;
    [SerializeField] private SortingCategory _sortingCategory;

    private enum SortingCategory : Byte
    {
        FoodWaste,                     // Madaffald 
        ResidualWaste,                 // Restaffald
        PaperAndCardboard,             // Papir & småt pap
        GlassAndMetal,                 // Glas & metal 
        PlasticAndFoodBeverageCartons, // Plast og mad- & drikkekartoner
        HazardousAndSmallElectronics,  // Farligt affald og småt elektronik
        Textiles,                      // Tekstilaffald
    }

    public override String ToString()
    {
        return $"Trash Name = {_trashName}, Point Value = {_pointValue}, Sorting Category = {_sortingCategory}";
    }
}