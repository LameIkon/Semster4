using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private DialogueUI _dialogueUI;
    public SODialogueNode _CurrentDisplayedNode;
    public string _CurrentNPCName;

    // DialogueUI (_dialogueUI) is found and initialized so that it can be used in the rest of the script.
    // We then print out DialogueUI as a way to verify if we have found it in the console.
    private void Start()
    {
        _dialogueUI = FindObjectOfType<DialogueUI>();
        Debug.Log($"DialogueUI found: {_dialogueUI}");
    }

    /// <summary>
    /// This method is responsible for starting the dialogue with an NPC. The way this works is to pass
    /// the starting dialogue of an NPC as an argument and then call this method in Interact() at NPC.cs.
    ///
    /// The current displayed node in the UI will then be initialized to the starting node of the NPC
    /// that we pass as an argument.
    ///
    /// Then...
    ///
    /// Lastly, the UI gets updated.
    /// </summary>
    /// <param name="startingNode">A starting dialogue that will be passed in at NPC.cs</param>
    public void StartDialogue(SODialogueNode startingNode, string npcName)
    {
        _CurrentNPCName       = npcName;
        _CurrentDisplayedNode = startingNode;
        DisplayNode(_CurrentDisplayedNode);
        _dialogueUI.UpdateDialogueUI(_CurrentDisplayedNode, _CurrentNPCName);
    }

    /// <summary>
    /// This method is responsible for continuing the dialogue with an NPC.
    ///
    ///
    /// Lastly, the UI gets updated.
    /// </summary>
    /// <param name="nextNode"></param>
    public void ProgressToNextDialogue(SODialogueNode nextNode)
    {
        _CurrentDisplayedNode = nextNode; // Move to the next node
        _dialogueUI.UpdateDialogueUI(_CurrentDisplayedNode, _CurrentNPCName);
    }

    /// <summary>
    /// This method is responsible for....
    /// </summary>
    /// <param name="node"></param>
    private void DisplayNode(SODialogueNode node)
    {
        Debug.Log(node._DialogueText);

        // Check conditions for next nodes
        foreach (SODialogueNode nextNode in node._NextNodes)
        {
            if (AreConditionsMet(nextNode))
            {
                Debug.Log($"Next: {nextNode._DialogueText}");
            }
        }

        // Display player responses if available
        if (node._PlayerResponses != null)
        {
            for (int i = 0; i < node._PlayerResponses.Length; i++)
            {
                Debug.Log($"{i + 1}: {node._PlayerResponses[i]._ResponseText}");
            }
        }
    }

    public void ChooseResponse(int responseIndex)
    {
        if (_CurrentDisplayedNode._PlayerResponses != null &&
            responseIndex < _CurrentDisplayedNode._PlayerResponses.Length)
        {
            StartDialogue(_CurrentDisplayedNode._PlayerResponses[responseIndex]._NextNode, _CurrentNPCName);
        }
    }

    /// <summary>
    /// This method is responsible for checking if the stage conditions are being met.
    ///
    /// It firstly checks if there are any conditions, if there aren't any, it returns true.
    ///
    /// If there are any conditions, it will iterate through them all and call IsMet() from SOQuestStageCondition.cs
    /// on every single condition. If IsMet() returns false on any condition, then this method will return false.
    /// </summary>
    /// <param name="node">The dialogue node that will have its conditions checked.</param>
    /// <returns>Returns true if the conditions have been met, false if they haven't.</returns>
    private bool AreConditionsMet(SODialogueNode node)
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