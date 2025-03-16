using UnityEngine;
using UnityEngine.Serialization;

public class NPC : MonoBehaviour
{
    [FormerlySerializedAs("_StartingSoDialogue")] 
    public SODialogueNode _StartingDialogue;
    
    // TriggerCollision Player
    // Call Interact

    public void Interact()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StartDialogue(_StartingDialogue);
    }
}