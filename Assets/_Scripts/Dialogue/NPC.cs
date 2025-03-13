using UnityEngine;
using UnityEngine.Serialization;

public class NPC : MonoBehaviour
{
    public SODialogueNode _StartingSoDialogue;

    public void Interact()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StartDialogue(_StartingSoDialogue);
    }
}