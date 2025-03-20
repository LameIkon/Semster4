using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public TextMeshProUGUI _DialogueText;
    public GameObject      _ButtonPrefab;
    public Transform       _ResponseContainer;
    const  int             _MaxResponses = 4;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentNode"></param>
    public void UpdateDialogueUI(SODialogueNode currentNode)
    {
        _DialogueText.text = currentNode._DialogueText;

        // Used to clear (destory) previous response buttons
        // Sort of like "refreshing"
        foreach (Transform child in _ResponseContainer)
        {
            Destroy(child.gameObject);
        }
        
        // Used to dynamically create buttons based on the node's responses
        if (currentNode._PlayerResponses != null)
        {
            for (int i = 0; i < currentNode._PlayerResponses.Length; i++)
            {
                if (i >= _MaxResponses)
                {
                    Debug.LogWarning($"Maximum number of responses exceeded. Only showing the first {_MaxResponses} responses");
                    break;
                }

                // Creating buttons
                GameObject button = Instantiate(_ButtonPrefab, _ResponseContainer);
                button.GetComponentInChildren<TextMeshProUGUI>().text = currentNode._PlayerResponses[i]._ResponseText;
                
                // Add listener to handle button click
                SODialogueNode nextNode = currentNode._PlayerResponses[i]._NextNode;  
                button.GetComponent<Button>().onClick.AddListener( () =>              
                {
                    OnResponseSelected(nextNode);
                });
            }
        }
        else
        {
            // If no responses are defined, show a single "Continue" button
            GameObject button = Instantiate(_ButtonPrefab, _ResponseContainer);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Fortsæt";
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnResponseSelected(null); 
            });
        }
    }

    private void OnResponseSelected([CanBeNull] SODialogueNode nextNode)
    {
        if (nextNode is null)
            EndDialogue();
        
        // Move to the next dialogue node
        else FindObjectOfType<DialogueManager>().ProgressToNextDialogue(nextNode); 
    }

    private void EndDialogue()
    {
        Debug.Log("Dialogue ended.");
    }
}