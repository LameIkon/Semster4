using System;
using _Scripts.Dialogue;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.Video;

#nullable enable

public class DialogueUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _npcName, _dialogueText;

    [SerializeField]
    private VideoClip _dialogueVideo;
    
    [SerializeField]
    private Transform _responseContainer;

    [SerializeField]
    private GameObject _buttonPrefab;

    private const int MaxResponses = 4;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentNode"></param>
    /// <param name="npcName"></param>
    public void UpdateDialogueUI(SODialogueVideoNode currentNode, string npcName)
    {
        _npcName.text = npcName;
        
        // Should update the video clip in the UI instead of the dialogue text
        _dialogueText.text = currentNode._DialogueVideo.ToString();

        ClearExistingButtons();

        // Creates a continue button if there are no defined player responses
        if (currentNode._PlayerResponses is null)
        {
            CreateButtons(null, _buttonPrefab, _responseContainer, "Fortsæt");
            return;
        }

        // Otherwise, create buttons based on the number of player responses and fill them with player response text
        for (int i = 0; i < currentNode._PlayerResponses.Length; i++)
        {
            if (i >= MaxResponses)
            {
                Debug.LogWarning(
                        $"Maximum number of responses exceeded. Only showing the first {MaxResponses} responses");
                break;
            }

            SODialogueVideoNode nextNode = currentNode._PlayerResponses[i]._NextVideoNode;
            CreateButtons(nextNode, _buttonPrefab, _responseContainer, currentNode._PlayerResponses[i]._ResponseText);
        }
    }

    /// <summary>
    /// Ensures that the UI is cleared from any leftover buttons so the new ones can be loaded safely.
    /// </summary>
    private void ClearExistingButtons()
    {
        for (int i = 0; i < _responseContainer.childCount; i++)
        {
            Destroy(_responseContainer.GetChild(i).gameObject);
        }
    }
    
    private static void CreateButtons(SODialogueVideoNode? nextNode, GameObject buttonPrefab, Transform responseContainer, string buttonText)
    {
        GameObject button = Instantiate(buttonPrefab, responseContainer);
        button.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;

        if (nextNode is not null)
        {
            button.GetComponent<Button>().onClick.AddListener(() => OnResponseSelected(nextNode));
            return;
        }

        button.GetComponent<Button>().onClick.AddListener(() => OnResponseSelected(null));
    }
    
    private static void OnResponseSelected(SODialogueVideoNode? nextNode)
    {
        if (nextNode is not null)
        {
            FindObjectOfType<DialogueManager>().ProgressToNextDialogue(nextNode);
            return;
        }

        EndDialogue();
    }

    private static void EndDialogue()
    {
        Debug.Log("Dialogue ended.");
        // TODO: Add more logic here to end the dialogue, maybe close the UI after a few seconds?
    }
}