using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private string _npcName;
    public SODialogueNode _StartingDialogue;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Interact();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CloseDialogue();
        }
    }
    
    private void Interact()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StartDialogue(_StartingDialogue, _npcName);
    }

    private void CloseDialogue()
    {
        // TODO: Add some logic here to end the dialogue, maybe close the UI after a few seconds?
    }
}