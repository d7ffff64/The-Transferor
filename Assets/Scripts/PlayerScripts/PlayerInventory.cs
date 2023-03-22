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

        [Header("Player Inventory Information")]
        [SerializeField] private bool isFull;

        public bool IsFull => isFull;

        private List<GameObject> resources = new List<GameObject>();

        #region Mono
        private void Awake()
        {
            
        }
        private void Start()
        {
        }
        #endregion

        public void NewResourceInTheInventory(GameObject resource)
        {
            resources.Add(item);
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
