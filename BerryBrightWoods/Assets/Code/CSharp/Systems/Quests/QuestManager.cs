using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance = null;
    public Quest quest = null;
    public UnityEvent OnCompleteQuest = new UnityEvent();

    bool questComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.LogError("multiple quest managers!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (quest.IsComplete() && !questComplete)
        {
            questComplete = true;
            OnCompleteQuest.Invoke();
            if (quest.rewards.Count > 0)
                quest.rewards.ForEach(x =>
                {
                    ResourceManager.instance.AddResource(x.resourceType, x.quantity);
                });
        }
    }
}
