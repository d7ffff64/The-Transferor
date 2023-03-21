using Assets.Scripts.PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ResourcesScripts
{
    public class Resource : MonoBehaviour
    {
        private Player player;
        private void OnCollisionEnter(Collision collision)
        {
            if (player.BackpackSize <= player.MaximumBackpackSize)
            {
                player.AddAResource();
                // Instantiate a resource in backpack
                Destroy(gameObject);
            }
        }
    }
}
