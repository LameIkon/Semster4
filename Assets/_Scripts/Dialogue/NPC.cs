using UnityEngine;
using UnityEngine.Serialization;

public class NPC : MonoBehaviour
{
    [FormerlySerializedAs("_StartingSoDialogue")] 
    public SODialogueNode _StartingDialogue;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Interact();
        }
    }

    public void Interact()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StartDialogue(_StartingDialogue);
    }
    
}