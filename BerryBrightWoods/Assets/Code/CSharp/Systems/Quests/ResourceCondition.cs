using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCondition : QuestCondition
{
    [SerializeField] public ResourceType type;
    [SerializeField] public int requiredAmount = 1;

    //bind amount to resource manager
    int _currentAmount = 0;
    //replace enum with type? idk yet
    public override bool IsComplete()
    {
        _currentAmount = ResourceManager.instance.ResourceAmount(type);
        return _isComplete = _currentAmount >= requiredAmount;
    }

    public override void Reset()
    {
        base.Reset();
        _currentAmount = 0;
    }

    public override string ToViewString()
    {
        return $"{type.ToString()}: {_currentAmount} / {requiredAmount}";
    }
}