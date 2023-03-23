using Assets.Scripts.PlayerScripts;
using UnityEngine;

namespace Assets.Scripts.ResourcesScripts
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private int id;
        public int Id => id;
    }
}
