using System.Collections.Generic;
using _Scripts.Dialogue;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Private Fields
    private DialogueUI _dialogueUI;
    private SODialogueVideoNode _rootNode;
    private Stack<SODialogueVideoNode> _nodeHistory = new();
    
    // Public Fields
    public SODialogueNode _CurrentDisplayedNode;
    public SODialogueVideoNode _CurrentDisplayedVideoNode;
    public string _CurrentNPCName;

    
    // DialogueUI (_dialogueUI) is found and initialized so that it can be used in the rest of the script.
    // We then print out DialogueUI as a way to verify if we have found it in the console.
    private void Start()
    {
        _dialogueUI = FindObjectOfType<DialogueUI>();
        Debug.Log($"DialogueUI found: {_dialogueUI}");
    }
    
    public void StartDialogue(SODialogueVideoNode startingNode, string npcName)
    {
        _nodeHistory.Clear(); 
        _rootNode = startingNode;
        _CurrentNPCName = npcName;
        _CurrentDisplayedVideoNode = startingNode;
        DisplayNode(_CurrentDisplayedVideoNode);
        _dialogueUI.UpdateDialogueUI(_CurrentDisplayedVideoNode, _CurrentNPCName);
    }
    
    public void ProgressToNextDialogue(SODialogueVideoNode nextNode)
    {
        if (_CurrentDisplayedVideoNode != null)
        {
            _nodeHistory.Push(_CurrentDisplayedVideoNode);
        }

        _CurrentDisplayedVideoNode = nextNode;
        _dialogueUI.UpdateDialogueUI(_CurrentDisplayedVideoNode, _CurrentNPCName);
    }

    public void ProgressToPreviousDialogue()
    {
        if (_nodeHistory.Count > 0)
        {
            _CurrentDisplayedVideoNode = _nodeHistory.Pop();
            _dialogueUI.UpdateDialogueUI(_CurrentDisplayedVideoNode, _CurrentNPCName);
        }
    }

    public void ReturnToRootDialogue()
    {
        // Clears the history stack, so that we don't have any previous nodes in the stack before going to the root node
        if (_rootNode != null)
        {
            _nodeHistory.Clear();
            _CurrentDisplayedVideoNode = _rootNode;
            _dialogueUI.UpdateDialogueUI(_CurrentDisplayedVideoNode, _CurrentNPCName);
        }
    }
    
    private void DisplayNode(SODialogueVideoNode node)
    {
        // TODO: Add logic to the rest of the function or remove it if not needed
        
        Debug.Log(node._DialogueVideo);

        foreach (SODialogueVideoNode nextNode in node._NextVideoNodes)
        {
            if (AreConditionsMet(nextNode))
            {
                Debug.Log($"Next: {nextNode._DialogueVideo}");
            }
        }

        // Display player responses if available
        // if (node._PlayerResponses != null)
        // {
        //     for (int i = 0; i < node._PlayerResponses.Length; i++)
        //     {
        //         Debug.Log($"{i + 1}: {node._PlayerResponses[i]._ResponseText}");
        //     }
        // }
    }

    public void ChooseResponse(int responseIndex)
    {
        if (_CurrentDisplayedNode._PlayerResponses != null &&
            responseIndex < _CurrentDisplayedNode._PlayerResponses.Length)
        {
            StartDialogue(_CurrentDisplayedVideoNode._PlayerResponses[responseIndex]._NextVideoNode, _CurrentNPCName);
        }
    }

    /// <summary>
    /// This method is responsible for checking if the stage conditions are being met.
    /// It firstly checks if there are any conditions, if there aren't any, it returns true.
    ///
    /// If there are any conditions, it will iterate through them all and call IsMet() from SOQuestStageCondition.cs
    /// on every single condition. If IsMet() returns false on any condition, then this method will return false.
    /// </summary>
    /// <param name="node">The dialogue node that will have its conditions checked.</param>
    /// <returns>Returns true if the conditions have been met, false if they haven't.</returns>
    private bool AreConditionsMet(SODialogueVideoNode node)
    {
        if (node._Conditions == null || node._Conditions.Length == 0)
            return true;

        foreach (ICondition condition in node._Conditions)
        {
            if (!condition.IsMet())
            {
                return false;
            }
        }

        return true;
    }
}