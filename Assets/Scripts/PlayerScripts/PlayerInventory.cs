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
        [Header("Player Inventory Information")]
        [SerializeField] private bool isFull;
        [SerializeField] private int amountOfResources;

        public bool IsFull => isFull;

        [SerializeField] private List<Transform> slots = new List<Transform>();

        #region Mono
        private void Awake()
        {
            
        }
        #endregion

        public GameObject GetResource(int id)
        {
            foreach (var slot in slots)
            {
                if (slot.transform.childCount != 0)
                {
                    GameObject resource = slot.transform.GetChild(0).gameObject;
                    if (resource.GetComponent<Resource>().Id == id)
                    {
                        return resource;
                    }
                    else
                    {
                        resource = null;
                    }
                }
            }
            return null;
        }
        public void NewResourceInTheInventory(GameObject resourcePrefab)
        {
            amountOfResources = 0;
            foreach (var slot in slots)
            {
                if (slot.gameObject.activeInHierarchy)
                {
                    amountOfResources++;
                }
            }

            if (amountOfResources >= slots.Count)
            {
                isFull = true;
            }
            else
            {
                Transform freeSlot = FreeSlots(slots);
                freeSlot.gameObject.SetActive(true);

                GameObject resource = Instantiate(resourcePrefab, new Vector3(freeSlot.transform.position.x, freeSlot.transform.position.y, freeSlot.transform.position.z), Quaternion.identity);
                resource.transform.parent = freeSlot;
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
            return null;
        }
    }
}
