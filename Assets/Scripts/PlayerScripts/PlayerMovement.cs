using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private Player player;
        private PlayerInput playerInput;

        #region Mono
        private void Awake()
        {
            player = GetComponent<Player>();
            playerInput = GetComponent<PlayerInput>();
        }

        #endregion

        private void Move()
        {
            Vector3 vector3 = new Vector3(playerInput.Vector2.x, 0f, playerInput.Vector2.y);

            transform.Translate(vector3 * player.MovementSpeed * Time.deltaTime, Space.World);

            if (vector3 != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vector3), 0.16f);
            }
        }
        private void Update()
        {
            Move();
        }
    }
}
