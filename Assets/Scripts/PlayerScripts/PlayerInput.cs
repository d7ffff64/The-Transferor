using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector2 _vector2;
        public Vector2 Vector2 => _vector2;
        public void OnMove(InputAction.CallbackContext context)
        {
            _vector2 = context.ReadValue<Vector2>();
        }
    }
}