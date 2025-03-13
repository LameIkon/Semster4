using UnityEngine;

[CreateAssetMenu(fileName = "Condition", menuName = "Dialogue/Condition")]
public abstract class SOCondition : ScriptableObject
{
    public abstract bool IsMet();       // I was wondering if I should scrape this 
}