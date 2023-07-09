using Assets.Scripts.UserInterfaceScripts;
using UnityEngine;

namespace Assets.Scripts.FactoryScripts
{
    public class Factory : MonoBehaviour
    {
        [Header("Factory Settings")]
        [SerializeField] private float _currentTime;
        [SerializeField] private float _timeToTheNextResource;
        [SerializeField] private Stock[] _stocks;
        [SerializeField] private int[] _amountOfResourcesRequired;

        [Header("Factory Information")]
        [SerializeField] private bool _allowProduce;

        [Header("Factory References")]
        [SerializeField] private InsideGameNotifications _insideGameNotifications;
        [SerializeField] private ResourceDepot _resourceDepot;

        [Header("Factory Notifications Settings")]
        [SerializeField] private bool _allowNotification;
        [SerializeField] private float _currentTimeNotification;
        [SerializeField] private float _timeToTheNextNotification;

        public int[] AmountOfresourceRequired => _amountOfResourcesRequired;

        #region Mono
        private void Awake()
        {
            _allowProduce = true;
            _currentTime = _timeToTheNextResource;
            _currentTimeNotification = _timeToTheNextNotification;
        }
        #endregion

        private void Update()
        {
            if (GoThroughTheStocks(_stocks, _amountOfResourcesRequired) && !_resourceDepot.IsFull)
            {
                _allowProduce = true;
            }
            else
            {
                _allowProduce = false;
            }

            if (_allowProduce && !_resourceDepot.IsFull)
            {
                _currentTime -= 1 / _timeToTheNextResource * Time.deltaTime;
                if (_currentTime <= 0f)
                {
                    Produce();
                    _currentTime = _timeToTheNextResource;
                }
            }

            if (_allowNotification)
            {
                _currentTimeNotification -= 1 / _timeToTheNextNotification * Time.deltaTime;

                if (_currentTimeNotification <= 0f)
                {
                    _insideGameNotifications.NewNotification($"{gameObject.name} | Factory has suspended its work due to a lack of resources.");
                    _currentTimeNotification = _timeToTheNextNotification;
                }
            }
        }
        private void Produce()
        {
            DecreaseAResourceInStocks(_stocks);
            _resourceDepot.InstantiateANewResource();
        }
        private void DecreaseAResourceInStocks(Stock[] stocks)
        {
            for (int i = 0; i < stocks.Length; i++)
            {
                if (stocks[i].AmountOfResources == _amountOfResourcesRequired[i])
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
                        _allowNotification = true;
                    }
                }
                return allStocksHaveTheRightAmountOfResources;
            }
            else
            {
                _allowNotification = false;
                return true;
            }
        }
    }
}
