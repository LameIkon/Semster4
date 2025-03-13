using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "QuestStageCondition", menuName = "Dialogue/Conditions/Quest Stage")]
public class SOQuestStageSoCondition : SOCondition
{
    public string _QuestName;
    public int    _RequiredStage;

    public override bool IsMet()
    {
        return QuestManager.s_Instance.GetQuestStage(_QuestName) >= _RequiredStage;
    }
}