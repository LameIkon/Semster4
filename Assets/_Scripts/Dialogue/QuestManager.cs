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

    public void SetQuestStage(string questName, int stage)
    {
        questStages[questName] = stage;
    }
}