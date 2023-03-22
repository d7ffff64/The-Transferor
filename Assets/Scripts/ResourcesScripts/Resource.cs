using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.ResourcesScripts
{
    public class Resource : MonoBehaviour
    {
        private PlayerInventory playerInventory;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (!playerInventory.IsFull)
                {
                    playerInventory.NewResourceInTheInventory(gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
