using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestReward
{
    [SerializeField] public ResourceType resourceType = ResourceType.Lumber;
    [SerializeField] public int quantity = 0;
}


[CreateAssetMenu(fileName = "NewQuest", menuName = "Custom/Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] protected string questName;
    [SerializeField] protected string description;
    [SerializeField] public List<QuestReward> rewards;
    [SerializeField] [HideInInspector] public List<QuestCondition> conditions = new List<QuestCondition>();

    Quest(string Name, string Description)
    {
        name = Name;
        description = Description;
    }

    public bool IsComplete()
    {
        foreach (var condition in conditions)
            if (!condition.IsComplete())
                return false;

        return true;
    }

    public void ResetQuest()
    {
        conditions.ForEach(x => x.Reset());
    }

    public string GetName()
    {
        return questName;
    }

    public string GetDescription()
    {
        return description;
    }
}
