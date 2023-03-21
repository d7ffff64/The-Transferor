using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [Header("Player Backpack Settings")]
        [SerializeField] private int backpackSize;
        [SerializeField] private int maximumBackpackSize;

        [Header("Player Movement Settings")]
        [SerializeField] private float movementSpeed;

        public int BackpackSize => backpackSize;
        public int MaximumBackpackSize => maximumBackpackSize;
        public float MovementSpeed => movementSpeed;

        public void AddAResource()
        {
            if (backpackSize != maximumBackpackSize)
            {
                backpackSize += 1;
            }
        }
    }
}

