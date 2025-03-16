using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class DialogueUI : MonoBehaviour
{
    public TextMeshProUGUI _DialogueText;      // NPC dialogue text
    public GameObject      _ButtonPrefab;      // Prefab for player response buttons
    public Transform       _ResponseContainer; // Parent container for buttons
    private const int      _MaxResponses = 4;  // Limit the number of response buttons (optional safeguard)

    public void UpdateDialogueUI(SODialogueNode currentNode)
    {
        // Update the dialogue text
        _DialogueText.text = currentNode._DialogueText;

        // Clear previous response buttons
        foreach (Transform child in _ResponseContainer)
        {
            Destroy(child.gameObject);
        }

        // Dynamically create buttons based on the node's responses
        if (currentNode._PlayerResponses != null)
        {
            for (int i = 0; i < currentNode._PlayerResponses.Length; i++)
            {
                if (i >= _MaxResponses)
                {
                    Debug.LogWarning("Maximum number of responses exceeded. Only showing the first " + _MaxResponses);
                    break;
                }

                // Create a new button
                GameObject button = Instantiate(_ButtonPrefab, _ResponseContainer);
                button.GetComponentInChildren<TextMeshProUGUI>().text = currentNode._PlayerResponses[i]._ResponseText;

                // Add listener to handle button click
                SODialogueNode
                        nextNode = currentNode._PlayerResponses[i]._NextNode; // Cache the next node for the listener
                button.GetComponent<Button>().onClick.AddListener(() => { OnResponseSelected(nextNode); });
            }
        }
        else
        {
            // If no responses are defined, show a single "Continue" button
            GameObject button = Instantiate(_ButtonPrefab, _ResponseContainer);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnResponseSelected(null); // Passing null or ending dialogue logic
            });
        }
    }

    private void OnResponseSelected(SODialogueNode nextNode)
    {
        if (nextNode != null)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(nextNode);
        }
        else
        {
            EndDialogue(); // Implement your end logic here
        }
    }

    private void EndDialogue()
    {
        Debug.Log("Dialogue ended.");
        // Logic for hiding dialogue UI or going back to gameplay
    }
}