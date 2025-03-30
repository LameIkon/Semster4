using System;
using UnityEngine;
using UnityEngine.Video;

namespace _Scripts.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueVideoNode", menuName = "Dialogue/VideoNode")]
    public class SODialogueVideoNode : ScriptableObject
    {
        public VideoClip _DialogueVideo;

        public SODialogueVideoNode[] _NextVideoNodes;
        public SOQuestStageCondition[] _Conditions;

        [Serializable]
        public class PlayerResponse
        {
            public String _ResponseText; // Could potentially be an Array or a TextArea, I'll think about it
            public SODialogueVideoNode _NextVideoNode;
        }

        public PlayerResponse[] _PlayerResponses;
    }
}