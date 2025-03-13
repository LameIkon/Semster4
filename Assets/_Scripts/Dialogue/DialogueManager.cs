using UnityEngine;
using UnityEngine.Serialization;

public class DialogueManager : MonoBehaviour
{
    // Should perhaps be turned into a Singleton?
    
    public SODialogueNode _CurrentDisplayedNode;

    public void StartDialogue(SODialogueNode startingNode)
    {
        _CurrentDisplayedNode = startingNode;
        DisplayNode(_CurrentDisplayedNode);
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