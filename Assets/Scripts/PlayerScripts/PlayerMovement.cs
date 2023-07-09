using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private Player _player;
        private PlayerInput _playerInput;

        #region Mono
        private void Awake()
        {
            _player = GetComponent<Player>();
            _playerInput = GetComponent<PlayerInput>();
        }

        #endregion

        private void Update()
        {
            Move();
        }
        private void Move()
        {
            Vector3 vector3 = new Vector3(_playerInput.Vector2.x, 0f, _playerInput.Vector2.y);

            transform.Translate(vector3 * _player.MovementSpeed * Time.deltaTime, Space.World);

            if (vector3 != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vector3), 0.16f);
            }
        }
    }
}
