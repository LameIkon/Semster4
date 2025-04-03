using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/Node")]
public class SODialogueNode : ScriptableObject
{
    [TextArea]
    public String _DialogueText;

    public SODialogueNode[] _NextNodes;
    public SOQuestStageCondition[] _Conditions;

    [Serializable]
    public class PlayerResponse
    {
        public String _ResponseText; // Could potentially be an Array or a TextArea, I'll think about it
        public SODialogueNode _NextNode;
    }

    public PlayerResponse[] _PlayerResponses;
}