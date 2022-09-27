using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

class ResourceTracker
{
    public int currentAmount;
    public UnityEvent OnAdd = new UnityEvent();
    public UnityEvent OnRemove = new UnityEvent();
}

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance = null;

    Dictionary<ResourceType, ResourceTracker> resources = new Dictionary<ResourceType, ResourceTracker>();
    UnityEvent OnAdd = new UnityEvent();
    UnityEvent OnRemove = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.LogError("multiple resource managers!!");
            Destroy(gameObject);
            return;
        }
        instance = this;

        var numTypes = System.Enum.GetNames(typeof(ResourceType));
        foreach (var type in numTypes)
            resources.Add((ResourceType)System.Enum.Parse(typeof(ResourceType), type), new ResourceTracker());

    }

    public void AddResource(ResourceType key, int amount)
    {
        resources[key].currentAmount += amount;
        Debug.Log($"{key} amount: {resources[key].currentAmount}");

        OnAdd.Invoke();
        resources[key].OnAdd.Invoke();
    }

    public void RemoveResource(ResourceType key, int amount)
    {
        resources[key].currentAmount -= amount;
        Debug.Log($"{key} amount: {resources[key].currentAmount}");
        
        OnRemove.Invoke();
        resources[key].OnRemove.Invoke();
    }

    public int ResourceAmount(ResourceType key)
    {
        return resources[key].currentAmount;
    }

    #region Binding Functions

    public void BindOnAddFor(ResourceType key, UnityAction func)
    {
        resources[key].OnAdd.AddListener(func);
    }
    public void UnbindOnAddFor(ResourceType key, UnityAction func)
    {
        resources[key].OnAdd.RemoveListener(func);
    }
    public void BindOnRemoveFor(ResourceType key, UnityAction func)
    {
        resources[key].OnRemove.AddListener(func);
    }
    public void UnbindOnRemoveFor(ResourceType key, UnityAction func)
    {
        resources[key].OnRemove.RemoveListener(func);
    }

    public void BindOnAdd(UnityAction func)
    {
        OnAdd.AddListener(func);
    }
    public void UnbindOnAdd(UnityAction func)
    {
        OnAdd.RemoveListener(func);
    }
    public void BindOnRemove(UnityAction func)
    {
        OnRemove.AddListener(func);
    }
    public void UnbindOnRemove(UnityAction func)
    {
        OnRemove.RemoveListener(func);
    }
    #endregion

}
