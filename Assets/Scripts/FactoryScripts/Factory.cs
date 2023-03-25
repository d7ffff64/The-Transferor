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

        public int[] AmountOfresourceRequired => amountOfResourcesRequired;

        #region Mono
        private void Awake()
        {
            allowProduce = true;
            currentTime = timeToTheNextResource;
        }
        #endregion

        private void Update()
        {
            if (GoThroughTheStocks(stocks, amountOfResourcesRequired) && !resourceDepot.IsFull)
            {
                allowProduce = true;
            }
            else
            {
                allowProduce = false;
            }

            if (allowProduce && !resourceDepot.IsFull)
            {
                currentTime -= 1 / timeToTheNextResource * Time.deltaTime;
                if (currentTime <= 0f)
                {
                    Produce();
                    currentTime = timeToTheNextResource;
                }
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
                if (stocks[i].AmountOfResources == amountOfResourcesRequired[i])
                {
                    stocks[i].DecreateAResource();
                }
            }
        }
        private bool GoThroughTheStocks(Stock[] stocks, int[] amountOfResourcesRequired)
        {
            bool allStocksHaveTheRightAmountOfResources = true;
            if (stocks.Length != 0 && amountOfResourcesRequired.Length != 0)
            {
                for (int i = 0; i < stocks.Length; i++)
                {
                    if (stocks[i].AmountOfResources < amountOfResourcesRequired[i])
                    {
                        allStocksHaveTheRightAmountOfResources = false;
                    }
                }
                return allStocksHaveTheRightAmountOfResources;
            }
            else
            {
                return true;
            }
        }
    }
}
