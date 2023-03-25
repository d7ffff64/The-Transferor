using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [Header("Player Movement Settings")]
        [SerializeField] private float _movementSpeed;
        public float MovementSpeed => _movementSpeed;
    }
}

