using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

class ConditionView
{
    GameObject conditionViewGameObject;
    QuestCondition condition;
    TextMeshProUGUI viewText;

    public ConditionView(QuestCondition Condition, GameObject ConditionViewPrefab, GameObject parent)
    {
        condition = Condition;
        conditionViewGameObject = GameObject.Instantiate(ConditionViewPrefab, parent.transform);
        viewText = conditionViewGameObject.GetComponent<TextMeshProUGUI>();
        viewText.text = $"- {condition.ToViewString()}";

        condition.OnReset.AddListener(() =>
        {
            viewText.fontStyle = FontStyles.Normal;
            viewText.color = Color.white;
        });
    }

    public void Update()
    {
        viewText.text = $"- {condition.ToViewString()}";
        if (condition.IsComplete())
        {
            viewText.fontStyle = FontStyles.Strikethrough;
            viewText.color = Color.green;
        }
    }
}

public class QuestView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] GameObject conditionViewPrefab;

    Quest quest;
    List<ConditionView> conditionsView = new List<ConditionView>();
    // Start is called before the first frame update
    void Start()
    {
        quest = QuestManager.instance.quest;
        foreach (var condition in quest.conditions)
            conditionsView.Add(new ConditionView(condition, conditionViewPrefab, description.gameObject));
        title.text = quest.GetName();
        description.text = quest.GetDescription();
    }

    private void Update()
    {
        conditionsView.ForEach(x => x.Update());
    }
}
