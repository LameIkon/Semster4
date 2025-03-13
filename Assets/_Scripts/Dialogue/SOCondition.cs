using UnityEngine;

[CreateAssetMenu(fileName = "Condition", menuName = "Dialogue/Condition")]
public abstract class SOCondition : ScriptableObject
{
    public abstract bool IsMet();
}