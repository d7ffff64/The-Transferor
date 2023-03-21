using Assets.Scripts.ResourcesScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.FactoryScripts
{
    public abstract class FactoryBase : MonoBehaviour
    {
        [Header("Factory Settings")]
        [SerializeField] private float productionSpeed;
        [SerializeField] private float maximumProductionSpeed;

        [SerializeField] private int[] theRequiredAmountForProduction;

        [Header("Factory Information")]

        [SerializeField] private bool allowProduce;

        [Header("Factory References")]
        [SerializeField] private ResourceDepot resourceDepot;

        [SerializeField] private Stock[] stocks;


        #region Mono
        private void Awake()
        {
            productionSpeed = maximumProductionSpeed;
        }
        #endregion
        private void Update()
        {
            if (allowProduce && !resourceDepot.IsFull)
            {
                productionSpeed -= 1f * Time.deltaTime;
                if (productionSpeed <= 0f)
                {
                    // Instantiate resource
                    productionSpeed = maximumProductionSpeed;
                }
            }
        }
    }
}
