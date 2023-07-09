using Assets.Scripts.PlayerScripts;
using Assets.Scripts.UserInterfaceScripts;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    [Header("Stock Settings")]
    [SerializeField] private int _requiredResourceId;
    [SerializeField] private float _currentTime;
    [SerializeField] private float _timeToTheNextResource;
    [SerializeField] private int _amountOfResources;
    [SerializeField] private int _maximumAmountOfResources;

    [Header("Stock Information")]
    [SerializeField] private bool _isFull;
    [SerializeField] private bool _isEmpty;
    [SerializeField] private bool _canPutDown;

    [Header("Stock Inventory Settings")]
    [SerializeField] private List<Transform> _slots = new List<Transform>();

    [Header("Stock References")]
    [SerializeField] private InsideGameNotifications _insideGameNotifications;
    [SerializeField] private PlayerInventory _playerInventory;

    [Header("Stock Notifications Settings")]
    [SerializeField] private float _currentTimeNotification;
    [SerializeField] private float _timeToTheNextNotification;
    public bool IsFull => _isFull;
    public bool IsEmpty => _isEmpty;
    public int AmountOfResources => _amountOfResources;
    public int MaximumAmountOfResources => _maximumAmountOfResources;

    #region Mono
    private void Awake()
    {
        _currentTime = _timeToTheNextResource;
        _currentTimeNotification = _timeToTheNextNotification;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_isFull)
            {
                _canPutDown = true;
            }
            else
            {
                _canPutDown = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _canPutDown = false;
        _currentTime = _timeToTheNextResource;
    }
    #endregion

    private void Update()
    {
        if (_canPutDown && _playerInventory.AmountOfResources >= 1)
        {
            _currentTime -= 1.0f / _timeToTheNextResource * Time.deltaTime;
            if (_currentTime <= 0f)
            {
                InstantiateANewResource();
                _currentTime = _timeToTheNextResource;
            }
        }

        if (_isFull)
        {
            _currentTimeNotification -= 1 / _timeToTheNextNotification * Time.deltaTime;

            if (_currentTimeNotification <= 0f)
            {
                _insideGameNotifications.NewNotification($"{gameObject.transform.parent.name} | {gameObject.name} | The stock is full!");
                _currentTimeNotification = _timeToTheNextNotification;
            }
        }
    }

    public void InstantiateANewResource()
    {
        if (_amountOfResources != _maximumAmountOfResources)
        {
            _isFull = false;

            if (_playerInventory.AnySuchResources(_requiredResourceId))
            {
                _amountOfResources += Mathf.Clamp(1, 0, _maximumAmountOfResources);

                Transform freeSlot = FreeSlots(_slots);
                freeSlot.gameObject.SetActive(true);

                if (freeSlot != null)
                {
                    GameObject resourcePrefab = _playerInventory.GetResource(_requiredResourceId);
                    if (resourcePrefab != null)
                    {
                        Instantiate(resourcePrefab, new Vector3(freeSlot.position.x, freeSlot.position.y, freeSlot.position.z), Quaternion.identity, freeSlot);
                    }
                }
            }
        }
        else
        {
            _isFull = true;
        }
    }
    public void DecreateAResource()
    {
        if (_amountOfResources >= 4)
        {
            for (int i = 0; i < 4; i++)
            {
                _amountOfResources -= Mathf.Clamp(1, 0, _maximumAmountOfResources);

                Transform busySlot = BusySlots(_slots);

                busySlot.gameObject.SetActive(false);
                Destroy(busySlot.transform.GetChild(0).gameObject);
            }
        }
        else
        {
            _isFull = false;
            _isEmpty = true;
        }
    }
    private Transform FreeSlots(List<Transform> slots)
    {
        foreach (var item in slots)
        {
            if (!item.gameObject.activeInHierarchy && item.childCount == 0)
            {
                return item;
            }
        }
        return null;
    }
    private Transform BusySlots(List<Transform> slots)
    {
        foreach (var item in slots)
        {
            if (item.gameObject.activeInHierarchy)
            {
                return item;
            }
        }
        return null;
    }
}
