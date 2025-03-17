using UnityEngine;

[CreateAssetMenu(fileName = "QuestStageCondition", menuName = "Dialogue/Conditions/Quest Stage")]
public class SOQuestStageCondition : ScriptableObject, ICondition
{
    public string _QuestName;
    public int    _RequiredStage;

    public bool IsMet()
    {
        return QuestManager.s_Instance.GetQuestStage(_QuestName) >= _RequiredStage;
    }
}