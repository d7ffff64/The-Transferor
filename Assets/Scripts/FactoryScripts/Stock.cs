using Assets.Scripts.ResourcesScripts;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    [Header("Stock Settings")]
    [SerializeField] private float currentTime;
    [SerializeField] private float timeToTheNextResource;
    [SerializeField] private int amountOfResources;
    [SerializeField] private int maximumAmountOfResources;

    [Header("Stock Information")]
    [SerializeField] private bool isFull;
    [SerializeField] private bool isEmpty;
    [SerializeField] private bool canPutDown;

    private List<GameObject> resources = new List<GameObject>();
     
    public bool IsFull => isFull;
    public bool IsEmpty => isEmpty;
    public int AmountOfResources => amountOfResources;
    public int MaximumAmountOfResources => maximumAmountOfResources;

    #region Mono
    private void Awake()
    {
        currentTime = timeToTheNextResource;
        if (amountOfResources == 0)
        {
            isEmpty = true;
        }
        else
        {
            if (amountOfResources == maximumAmountOfResources)
            {
                isFull = true;
            }
        }
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
    }
    #endregion

    private void Update()
    {
        if (isEmpty)
        {
            canPutDown = true;
        }

        if (canPutDown)
        {
            currentTime -= 1.0f / timeToTheNextResource * Time.deltaTime;
            if (currentTime <= 0f)
            {
                DecreateAResource();
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
            // Instantiate a object in stock
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
            // Destory 1 object resource in stock
            // Instantiate a object in inventory
        }
        else
        {
            isEmpty = true;
        }
    }
}
