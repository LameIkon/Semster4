using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueManager : MonoBehaviour
{
    private DialogueUI     _dialogueUI;
    public  SODialogueNode _CurrentDisplayedNode;

    private void Start()
    {
        _dialogueUI = FindObjectOfType<DialogueUI>(); 
        Debug.Log(_dialogueUI);
    }

    public void StartDialogue(SODialogueNode startingNode)
    {
        _CurrentDisplayedNode = startingNode;
        DisplayNode(_CurrentDisplayedNode);
        _dialogueUI.UpdateDialogueUI(_CurrentDisplayedNode);
    }
    
    public void ProgressToNextDialogue(SODialogueNode nextNode)
    {
        _CurrentDisplayedNode = nextNode;                    // Move to the next node
        _dialogueUI.UpdateDialogueUI(_CurrentDisplayedNode); // Update the UI with new node
    }
    
    public void DisplayNode(SODialogueNode node)
    {
        Debug.Log(node._DialogueText);

        // Check conditions for next nodes
        foreach (SODialogueNode nextNode in node._NextNodes)
        {
            if (!AreConditionsMet(nextNode)) continue;
            ShowNextValidDialogueNodes();

            void ShowNextValidDialogueNodes()
            {
                print($"Next: {nextNode._DialogueText}");
            }
        }

        // Display player responses if available
        if (node._PlayerResponses != null)
        {
            for (int i = 0; i < node._PlayerResponses.Length; i++)
            {
                print($"{i + 1}: {node._PlayerResponses[i]._ResponseText}");
            }
        }
    }

    public void ChooseResponse(int responseIndex)
    {
        if (_CurrentDisplayedNode._PlayerResponses != null &&
            responseIndex < _CurrentDisplayedNode._PlayerResponses.Length)
        {
            StartDialogue(_CurrentDisplayedNode._PlayerResponses[responseIndex]._NextNode);
        }
    }
    
    private bool AreConditionsMet(SODialogueNode node)
    {
        if (node._Conditions == null || node._Conditions.Length == 0)
            return true;

        foreach (SOCondition condition in node._Conditions)
        {
            if (!condition.IsMet())
                return false;
        }

        return true;
    }
}