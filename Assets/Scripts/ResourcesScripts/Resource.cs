using UnityEngine;

namespace Assets.Scripts.ResourcesScripts
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private int _id;
        public int Id => _id;
    }
}
