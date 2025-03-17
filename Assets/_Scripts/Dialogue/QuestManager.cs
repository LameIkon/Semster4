using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager s_Instance;
    private Dictionary<string, int> questStages = new();
    
    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else Destroy(gameObject);
    }
    
    public int GetQuestStage(string questName)
    {
        if (questStages.TryGetValue(questName, out int stage))
        {
            return stage;
        }

        return 0; // Default game stage
    }

    /// <summary>
    /// This method should be called in scripts responsible for player actions, game events, or dialogue progression that directly affect quest progress.
    /// Examples of this could be key items that advances the quest by or to a certain stage.
    /// </summary>
    /// <param name="questName">The name of the quest that should be advanced</param>
    /// <param name="stage">The stage the quest should be advanced to</param>
    public void SetQuestStage(string questName, int stage)
    {
        questStages[questName] = stage;
        Debug.Log($"{questName} is now set to {stage}");
    }
}