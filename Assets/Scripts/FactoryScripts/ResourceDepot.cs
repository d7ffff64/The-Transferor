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
        [SerializeField] private int cellsX;
        [SerializeField] private int cellsY;
        [SerializeField] private int cellsSize;
        [SerializeField] private int maximumCellsSize;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Vector2 startPosition;

        private List<GameObject> cells = new List<GameObject>();
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
        private void Start()
        {
            startPosition = new Vector2(0f, 0f);
            Vector2 cellPotision = new Vector2(startPosition.x + cellsY * cellsSize, startPosition.y + cellsX * cellsSize);

            GameObject cell = Instantiate(cellPrefab, cellPotision, Quaternion.identity, transform);

            cells.Add(cell);
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
                isEmpty = false;
                amountOfResources += Mathf.Clamp(1, 0, maximumAmountOfResources);
                NewResourceInTheInventory()
            }
            else
            {
                isFull = true;
            }
        }
        public void NewResourceInTheInventory(GameObject item)
        {
            if (cellsSize == maximumCellsSize)
            {
                isFull = true;
            }
            else
            {
                isFull = false;
            }

            resources.Add(item);

            GameObject freeCell = Free(cells);

            if (freeCell != null)
            {
                item.transform.position = freeCell.transform.position;
            }
        }
        private GameObject Free(List<GameObject> objects)
        {
            foreach (var item in objects)
            {
                if (!item.activeInHierarchy)
                {
                    return item;
                }
            }
            return null;
        }
        private void DecreateAResource()
        {
            if (amountOfResources != 0)
            {
                amountOfResources -= Mathf.Clamp(1, 0, maximumAmountOfResources);
                // Destory 1 object resource in resource depot
                // Instantiate a object in inventory

            }
            else
            {
                isEmpty = true;
            }
        }
    }
}
