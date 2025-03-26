using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Data")]
public class SOTutorialData : ScriptableObject
{
    [Header("Description")]
    public string SO_Name;
    [TextArea(3, 15)] public string SO_Description;
}
