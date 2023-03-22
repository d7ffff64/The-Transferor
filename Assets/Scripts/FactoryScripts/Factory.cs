using UnityEngine;

namespace Assets.Scripts.FactoryScripts
{
    public class Factory : MonoBehaviour
    {
        [Header("Factory Settings")]
        [SerializeField] private float currentTime;
        [SerializeField] float timeToTheNextResource;

        [Header("Factory Information")]
        [SerializeField] private bool allowProduce;

        [Header("Factory References")]
        [SerializeField] private ResourceDepot resourceDepot;
        [SerializeField] private Stock[] stocks;
        [SerializeField] private int[] amountOfResourcesRequired;

        #region Mono
        private void Awake()
        {
            allowProduce = true;
            currentTime = timeToTheNextResource;
        }
        #endregion

        private void Update()
        {
            if (resourceDepot.IsFull)
            {
                allowProduce = false;
            }

            if (allowProduce)
            {
                currentTime -= 1 / timeToTheNextResource * Time.deltaTime;
                if (currentTime <= 0f)
                {
                    CheckBeforeProduction();
                    currentTime = timeToTheNextResource;
                }
            }
        }
        public void CheckBeforeProduction()
        {
            if (allowProduce && !resourceDepot.IsFull && GoThroughTheStocks(stocks, amountOfResourcesRequired))
            {
                Produce();
            }
        }
        private void Produce()
        {
            DecreaseAResourceInStocks(stocks);
            resourceDepot.InstantiateANewResource();
        }
        private void DecreaseAResourceInStocks(Stock[] stocks)
        {
            for (int i = 0; i < stocks.Length; i++)
            {
                if (stocks[i].AmountOfResources != 0)
                {
                    stocks[i].DecreateAResource();
                }
            }
        }
        private bool GoThroughTheStocks(Stock[] stocks, int[] amountOfResourcesRequired)
        {
            if (stocks.Length != 0 && amountOfResourcesRequired.Length != 0)
            {
                for (int i = 0; i < stocks.Length; i++)
                {
                    if (stocks[i].AmountOfResources == amountOfResourcesRequired[i])
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
