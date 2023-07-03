using System;
using LabraxStudio.Managers;
using UnityEngine;

namespace LabraxStudio.AnalyticsIntegration.IAP
{
    public class IAPManager : SharedManager<IAPManager>
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        private bool IsBusy => !_isSetuped || _purchaseInProgress || _isRestore;

        // FIELDS: -------------------------------------------------------------------

        private bool _isSetuped = false;
        private bool _purchaseInProgress;
        private bool _isRestore = false;
        private bool _needBuyEvent = false;
        private Action<bool> _onPurchaseComplete;
        private Action _onRestoreDone;
        private IapDelegate _iapDelegate;
        private IapSettings _iapSettings;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(IapSettings iapSettings)
        {
            base.InitManager();

            _iapSettings = iapSettings;
            _iapDelegate = new IapDelegate();
            _iapDelegate.Setup(OnInitComplete, OnPurchaseComplete, OnPurchaseFailure);

            AnalyticsManager.IAPCore.AddIapPurchaseDelegate(_iapDelegate);
        }

        public void BuyProduct(ProductName productName, Action<bool> onComplete = null)
        {
            if (IsBusy)
                return;

            IapId iap = GetIapSettings(productName);

            if (iap == null)
                return;

            _purchaseInProgress = true;

            if (onComplete != null)
                _onPurchaseComplete += onComplete;

            _needBuyEvent = true;

            AnalyticsManager.IAPCore.Purchase(iap.Product);
        }

        public void DebugBuyProduct(ProductName productName, Action onComplete = null)
        {
            /*
            var iap = GetIapSettings(productName);
            if (iap == null)
                return;
            */
            OnPurchaseComplete(productName);
        }

        public void RestorePurchases(Action onRestoreDone = null)
        {
            if (IsBusy)
                return;

            _onRestoreDone += onRestoreDone;
            _isRestore = true;

            AnalyticsManager.IAPCore.RestorePurchases(OnRestorePurchasesComplete);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void ApplyPurchase(ProductName productName)
        {
            switch (productName)
            {
                case ProductName.NoAds:
                    AnalyticsManager.EnablePremium();
                    break;

                case ProductName.NoProduct:
                    break;

                default:
                    AnalyticsManager.EnablePremium();
                    break;
            }
        }

        private void CheckPurchases()
        {
            var restoredList = PurchaseRestoreListener.ProductsNames;
            _isRestore = true;

            foreach (var productName in restoredList)
                ApplyPurchase(productName);

            _isRestore = false;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnInitComplete()
        {
            _isSetuped = true;
            CheckPurchases();
        }

        private void OnRestorePurchasesComplete(bool result)
        {
            if (result == true)
            {
                OnRestoreDone();
                IAPAlertWindow.ShowRestoreSuccessful();
            }
            else
            {
                Debug.Log("Restore purchases: " + result);
                IAPAlertWindow.ShowRestoreFailed();
            }

            _isRestore = false;
        }

        private void OnRestoreDone()
        {
            if (_onRestoreDone == null)
                return;

            _onRestoreDone.Invoke();
            _onRestoreDone = null;
        }

        public void OnPurchaseComplete(ProductName productID)
        {
            var iap = GetIapSettings(productID);
            if (iap == null)
            {
                _purchaseInProgress = false;
                return;
            }

            ApplyPurchase(iap.Product);
            if (_needBuyEvent)
                AnalyticsManager.EventsCore.TrackIapPurchaseComplete(iap.Product, iap.CashPoints);

            if (_onPurchaseComplete != null)
            {
                _onPurchaseComplete?.Invoke(true);
                _onPurchaseComplete = null;
            }

            _purchaseInProgress = false;
            _needBuyEvent = false;
        }

        public void OnPurchaseFailure()
        {
            if (_onPurchaseComplete != null)
            {
                _onPurchaseComplete?.Invoke(false);
                _onPurchaseComplete = null;
            }

            _purchaseInProgress = false;
        }

        public IapId GetIapSettings(ProductName productName) =>
            _iapSettings?.Iaps.Find(iap => iap.Product == productName);

        public IapId GetIapSettings(string productID) =>
            _iapSettings?.Iaps.Find(iap => iap.Id == productID);

        public static IapId GetIapSettings(IapSettings iapSettings, ProductName productName)
        {
            return iapSettings?.Iaps.Find(iap => iap.Product == productName);
        }

        public static IapId GetIapSettings(IapSettings iapSettings, string productID)
        {
            return iapSettings?.Iaps.Find(iap => iap.Id == productID);
        }
    }
}