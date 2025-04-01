using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Data")]
public class SOTutorialData : ScriptableObject
{
    [Header("Description")]
    [TextArea(3, 15)] public string SO_Description;

    [Header("Objective Description")]
    [TextArea(3, 15)] public string SO_Objective;


    [Space(10)]public bool SO_ShowContinueButton;
}
