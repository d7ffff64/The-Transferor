using Assets.Scripts.PlayerScripts;
using Assets.Scripts.ResourcesScripts;
using Assets.Scripts.UserInterfaceScripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.FactoryScripts
{
    public class ResourceDepot : MonoBehaviour
    {
        [Header("Resource Depot Settings")]
        [SerializeField] private float _currentTime;
        [SerializeField] private float _timeToTheNextResource;
        [SerializeField] private int _amountOfResources;
        [SerializeField] private int _maximumAmountOfResources;

        [Header("Resource Depot Information")]
        [SerializeField] private bool _isFull;
        [SerializeField] private bool _isEmpty;
        [SerializeField] private bool _canTake;

        [Header("Resource Depot Inventory Settings")]
        [SerializeField] private List<Transform> _slots = new List<Transform>();

        [Header("Resource Depot References")]
        [SerializeField] private InsideGameNotifications _insideGameNotifications;
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private GameObject _resourcePrefab;

        [Header("Stock Notifications Settings")]
        [SerializeField] private float _currentTimeNotification;
        [SerializeField] private float _timeToTheNextNotification;

        public bool IsFull => _isFull;
        public bool IsEmpty => _isEmpty;
        public int AmountOfResources => _amountOfResources;
        public int MaximumAmountOfResources => _maximumAmountOfResources;

        public GameObject ResourcePrefab => _resourcePrefab;

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
                if (!_isEmpty && !_playerInventory.IsFull)
                {
                    _canTake = true;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            _canTake = false;
            _currentTime = _timeToTheNextResource;
        }
        #endregion

        private void Update()
        {
            if (_amountOfResources != _maximumAmountOfResources)
            {
                _isFull = false;
            }
            else
            {
                _isEmpty = false;
                _isFull = true;
            }

            if (_canTake)
            {
                _currentTime -= 1.0f / _timeToTheNextResource * Time.deltaTime;
                if (_currentTime <= 0f)
                {
                    if (_amountOfResources != 0)
                    {
                        _isEmpty = false;
                        DecreateAResource();
                    }
                    else
                    {
                        _isEmpty = true;
                    }
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
            _isFull = false;
            _amountOfResources += Mathf.Clamp(1, 0, _maximumAmountOfResources);
            NewResourceInTheInventory(_resourcePrefab);
        }
        public void NewResourceInTheInventory(GameObject resourcePrefab)
        {
            _isFull = false;
            Transform freeSlot = FreeSlots(_slots);
            freeSlot.gameObject.SetActive(true);

            Instantiate(resourcePrefab, new Vector3(freeSlot.transform.position.x, freeSlot.transform.position.y, freeSlot.transform.position.z), Quaternion.identity, freeSlot);
        }
        private Transform FreeSlots(List<Transform> slots)
        {
            foreach (var item in slots)
            {
                if (!item.gameObject.activeInHierarchy)
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
        private void DecreateAResource()
        {
            if (_amountOfResources != 0 && !_playerInventory.IsFull)
            {
                _amountOfResources -= Mathf.Clamp(1, 0, _maximumAmountOfResources);

                Transform busySlot = BusySlots(_slots);
                GameObject resource = busySlot.transform.GetChild(0).gameObject;

                _playerInventory.NewResourceInTheInventory(resource);

                Destroy(busySlot.transform.GetChild(0).gameObject);
                busySlot.gameObject.SetActive(false);
            }
            else
            {
                _isEmpty = true;
            }
        }
    }
}
