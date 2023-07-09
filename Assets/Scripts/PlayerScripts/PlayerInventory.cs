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
        [SerializeField] private bool _isFull;
        [SerializeField] private int _amountOfResources;

        public bool IsFull => _isFull;
        public int AmountOfResources => _amountOfResources;

        [SerializeField] private List<Transform> _slots = new List<Transform>();

        public GameObject GetResource(int id)
        {
            foreach (var slot in _slots)
            {
                if (slot.childCount != 0)
                {
                    GameObject resource = slot.GetChild(0).gameObject;
                    if (resource.GetComponent<Resource>().Id == id)
                    {
                        if (_amountOfResources <= _slots.Count)
                        {
                            _isFull = false;
                        }
                        _amountOfResources -= Mathf.Clamp(1, 1, _slots.Count);
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
            foreach (var slot in _slots)
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
            if (_amountOfResources >= _slots.Count)
            {
                _isFull = true;
            }
            else
            {
                _amountOfResources = 1;
                foreach (var slot in _slots)
                {
                    if (slot.gameObject.activeInHierarchy)
                    {
                        _amountOfResources += Mathf.Clamp(1, 1, _slots.Count);
                    }
                }

                Transform freeSlot = FreeSlots(_slots);
                freeSlot.gameObject.SetActive(true);

                Instantiate(resourcePrefab, new Vector3(freeSlot.transform.position.x, freeSlot.transform.position.y, freeSlot.transform.position.z), freeSlot.rotation, freeSlot);
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
