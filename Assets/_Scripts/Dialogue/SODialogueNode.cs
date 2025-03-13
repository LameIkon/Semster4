using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/Node")]
public class SODialogueNode : ScriptableObject
{
    [TextArea]
    public String _DialogueText;

    public SODialogueNode[] _NextNodes;
    public SOCondition[]    _Conditions;

    [Serializable]
    public class PlayerResponse
    {
        public String         _ResponseText;
        public SODialogueNode _NextNode;
    }

    public PlayerResponse[] _PlayerResponses;
}