using Assets.Scripts.FactoryScripts;
using Assets.Scripts.PlayerScripts;
using Assets.Scripts.ResourcesScripts;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    [Header("Stock Settings")]
    [SerializeField] private int requiredResourceId;
    [SerializeField] private float currentTime;
    [SerializeField] private float timeToTheNextResource;
    [SerializeField] private int amountOfResources;
    [SerializeField] private int maximumAmountOfResources;

    [Header("Stock Information")]
    [SerializeField] private bool isFull;
    [SerializeField] private bool isEmpty;
    [SerializeField] private bool canPutDown;

    [Header("Stock Inventory Settings")]
    [SerializeField] private List<Transform> slots = new List<Transform>();

    [Header("Stock References")]
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private ResourceDepot resourceDepot;

    public bool IsFull => isFull;
    public bool IsEmpty => isEmpty;
    public int AmountOfResources => amountOfResources;
    public int MaximumAmountOfResources => maximumAmountOfResources;

    #region Mono
    private void Awake()
    {
        currentTime = timeToTheNextResource;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isFull)
            {
                canPutDown = true;
            }
            else
            {
                canPutDown = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canPutDown = false;
        currentTime = timeToTheNextResource;
    }
    #endregion

    private void Update()
    {
        if (canPutDown)
        {
            currentTime -= 1.0f / timeToTheNextResource * Time.deltaTime;
            if (currentTime <= 0f)
            {
                InstantiateANewResource();
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

            Transform freeSlot = FreeSlots(slots);
            freeSlot.gameObject.SetActive(true);

            if (freeSlot != null)
            {
                GameObject resourcePrefab = playerInventory.GetResource(requiredResourceId);

                Instantiate(resourcePrefab, new Vector3(freeSlot.position.x, freeSlot.position.y, freeSlot.position.z), Quaternion.identity, freeSlot);
            }
        }
        else
        {
            isFull = true;
        }
    }
    public void DecreateAResource()
    {
        if (amountOfResources != 0)
        {
            amountOfResources -= Mathf.Clamp(1, 0, maximumAmountOfResources);
            Transform busySlot = BusySlots(slots);
            busySlot.gameObject.SetActive(false);
            Destroy(busySlot.transform.GetChild(0).gameObject);
        }
        else
        {
            isFull = false;
            isEmpty = true;
        }
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
        Debug.Log("Free slots were not found");
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
        Debug.Log("Busy slots were not found");
        return null;
    }
}
