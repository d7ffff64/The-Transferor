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
        public int AmountOfResources => amountOfResources;

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
                if (slot.childCount != 0)
                {
                    GameObject resource = slot.GetChild(0).gameObject;
                    if (resource.GetComponent<Resource>().Id == id)
                    {
                        if (amountOfResources <= slots.Count)
                        {
                            isFull = false;
                        }
                        //amountOfResources -= 1;
                        amountOfResources -= Mathf.Clamp(1, 1, slots.Count);
                        slot.gameObject.SetActive(false);
                        Destroy(resource, 0.1f);
                        return resource;
                    }
                }
            }
            return null;
        }
        public bool AnySuchResources(int id)
        {
            foreach (var slot in slots)
            {
                if (slot.childCount != 0)
                {
                    if (slot.GetChild(0).gameObject.GetComponent<Resource>().Id == id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void NewResourceInTheInventory(GameObject resourcePrefab)
        {
            if (amountOfResources >= slots.Count)
            {
                isFull = true;
            }
            else
            {
                amountOfResources = 1;
                foreach (var slot in slots)
                {
                    if (slot.gameObject.activeInHierarchy)
                    {
                        amountOfResources += Mathf.Clamp(1, 1, slots.Count);
                    }
                }

                Transform freeSlot = FreeSlots(slots);
                freeSlot.gameObject.SetActive(true);

                Instantiate(resourcePrefab, new Vector3(freeSlot.transform.position.x, freeSlot.transform.position.y, freeSlot.transform.position.z), Quaternion.identity, freeSlot);
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
