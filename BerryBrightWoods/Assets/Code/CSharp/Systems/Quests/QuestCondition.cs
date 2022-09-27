using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class QuestCondition : ScriptableObject
{
    [SerializeField] [HideInInspector] public UnityEvent OnConditionMet = new UnityEvent();
    [SerializeField] [HideInInspector] public UnityEvent OnReset = new UnityEvent();
    [SerializeField] public string description = "";

    protected bool _isComplete = false;
    public virtual bool IsComplete()
    {
        return _isComplete;
    }

    public void SetCompleted(bool value)
    {
        _isComplete = value;
    }

    public virtual void Reset()
    {
        OnReset.Invoke();
    }

    public virtual string ToViewString()
    {
        return description;
    }
}

