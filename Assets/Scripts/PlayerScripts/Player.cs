using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [Header("Player Movement Settings")]
        [SerializeField] private float movementSpeed;

        public float MovementSpeed => movementSpeed;
    }
}

