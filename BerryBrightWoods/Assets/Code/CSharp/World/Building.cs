using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Building : MonoBehaviour
{

    [SerializeField] ResourceType outputResource = ResourceType.Lumber;
    [Tooltip("workOutput = workspeed*workerCount")]
    [SerializeField] float workSpeed = 1.0f;
    [SerializeField] float workTime = 5.0f;
    [SerializeField] int maxStockAmount = 5;


    [Header("Events")]
    [SerializeField] UnityEvent OnAddWorker;
    public UnityEvent OnRemoveWorker;
    public UnityEvent OnCompleteWorkCycle;
    public UnityEvent OnStockFull;
    public UnityEvent OnMouseEnter;
    public UnityEvent OnMouseExit;

    [Header("References")]
    [SerializeField] Slider progressBar = null;
    [SerializeField] TextCounter workerCounter = null;
    [SerializeField] TextCounter resourceCounter = null;
    [SerializeField] GameObject worldspaceCanvas = null;
    [SerializeField] GameObject removeWorkerButton = null;

    int _workerCount = 0;
    int _numStoredResource = 0;
    float _progress = 0;

    bool StockFull => _numStoredResource == maxStockAmount;

    bool _pinned = false;

    private void Awake()
    {
        worldspaceCanvas.SetActive(false);
        OnAddWorker.AddListener(() =>
        {
            _workerCount++;
            workerCounter.count = _workerCount;
            progressBar.gameObject.SetActive(true);
            removeWorkerButton.SetActive(true);
        });
        OnRemoveWorker.AddListener(() =>
        {
            _workerCount--;
            workerCounter.count = _workerCount;
            if (_workerCount == 0)
            {
                progressBar.gameObject.SetActive(false);
                removeWorkerButton.SetActive(false);
            }

        });
        OnCompleteWorkCycle.AddListener(() =>
        {
            ResourceManager.instance.AddResource(outputResource, 1);
            _numStoredResource++;
            resourceCounter.count++;
            if (StockFull)
                OnStockFull.Invoke();
            else
            {
                progressBar.value = 0.0f;
                _progress = 0.0f;
            }
        });
        OnStockFull.AddListener(() =>
        {
            Debug.Log("Resource stock is full! Cannot work more.");
        });
        OnMouseEnter.AddListener(() =>
        {
            if (_pinned)
                return;
            worldspaceCanvas.SetActive(true);
        });
        OnMouseExit.AddListener(() =>
        {
            if (_pinned)
                return;
            worldspaceCanvas.SetActive(false);
        });
    }

    private void Update()
    {
        if (_pinned)
            worldspaceCanvas.SetActive(true);
        if (StockFull)
            return;

        _progress += workSpeed * _workerCount * Time.deltaTime;
        var prog = _progress / workTime;
        progressBar.value = prog;

        if (prog >= 1.0f)
            OnCompleteWorkCycle.Invoke();
    }

    public void AddWorker()
    {
        OnAddWorker.Invoke();
    }

    public void RemoveWorker()
    {
        OnRemoveWorker.Invoke();
    }

    public void TogglePinned()
    {
        _pinned = !_pinned;
    }
}
