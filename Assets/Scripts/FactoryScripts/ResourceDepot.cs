using Assets.Scripts.PlayerScripts;
using Assets.Scripts.ResourcesScripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.FactoryScripts
{
    public class ResourceDepot : MonoBehaviour
    {
        [Header("Resource Depot Settings")]
        [SerializeField] private float currentTime;
        [SerializeField] float timeToTheNextResource;
        [SerializeField] private int amountOfResources;
        [SerializeField] private int maximumAmountOfResources;

        [Header("Resource Depot Information")]
        [SerializeField] private bool isFull;
        [SerializeField] private bool isEmpty;
        [SerializeField] private bool canTake;

        [Header("Resource Depot Inventory Settings")]
        [SerializeField] private List<Transform> slots = new List<Transform>();

        [Header("Resource Depot References")]
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private GameObject resourcePrefab;

        public bool IsFull => isFull;
        public bool IsEmpty => isEmpty;
        public int AmountOfResources => amountOfResources;
        public int MaximumAmountOfResources => maximumAmountOfResources;

        public GameObject ResourcePrefab => resourcePrefab;

        #region Mono
        private void Awake()
        {
            currentTime = timeToTheNextResource;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!isEmpty)
                {
                    canTake = true;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            canTake = false;
            currentTime = timeToTheNextResource;
        }
        #endregion

        private void Update()
        {
            if (canTake)
            {
                currentTime -= 1.0f / timeToTheNextResource * Time.deltaTime;
                if (currentTime <= 0f)
                {
                    if (amountOfResources != 0)
                    {
                        isEmpty = false;
                        DecreateAResource();
                    }
                    else
                    {
                        isEmpty = true;
                    }
                    currentTime = timeToTheNextResource;
                }
            }
        }
        public void InstantiateANewResource()
        {
            if (amountOfResources != maximumAmountOfResources)
            {
                isFull = false;
                amountOfResources += Mathf.Clamp(1, 0, maximumAmountOfResources);
                NewResourceInTheInventory(resourcePrefab);
            }
            else
            {
                isEmpty = false;
                isFull = true;
            }
        }
        public void NewResourceInTheInventory(GameObject resourcePrefab)
        {
            Transform freeSlot = FreeSlots(slots);
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
            if (amountOfResources != 0 && !playerInventory.IsFull)
            {
                amountOfResources -= Mathf.Clamp(1, 0, maximumAmountOfResources);

                Transform busySlot = BusySlots(slots);
                GameObject resource = busySlot.transform.GetChild(0).gameObject;

                playerInventory.NewResourceInTheInventory(resource);

                Destroy(busySlot.transform.GetChild(0).gameObject);
                busySlot.gameObject.SetActive(false);
            }
            else
            {
                isEmpty = true;
            }
        }
    }
}
