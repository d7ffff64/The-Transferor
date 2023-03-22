using Assets.Scripts.ResourcesScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerInventory : MonoBehaviour
    {
        [Header("Player Inventory Settings")]
        [SerializeField] private int cellsX;
        [SerializeField] private int cellsY;
        [SerializeField] private int cellsSize;
        [SerializeField] private int maximumCellsSize;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Vector2 startPosition;

        [Header("Player Inventory Information")]
        [SerializeField] private bool isFull;

        public bool IsFull => isFull;

        private List<GameObject> cells = new List<GameObject>();
        private List<GameObject> resources = new List<GameObject>();

        #region Mono
        private void Awake()
        {
            
        }
        private void Start()
        {
            startPosition = new Vector2(0f, 0f);
            Vector2 cellPotision = new Vector2(startPosition.x + cellsY * cellsSize, startPosition.y + cellsX * cellsSize);

            GameObject cell = Instantiate(cellPrefab, cellPotision, Quaternion.identity, transform);

            cells.Add(cell);
        }
        #endregion

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
    }
}
