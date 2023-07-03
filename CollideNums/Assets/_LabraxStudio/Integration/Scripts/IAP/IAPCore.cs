using System;
using System.Collections.Generic;

//using SupersonicWisdomSDK;
namespace LabraxStudio.AnalyticsIntegration.IAP
{
    public class IAPCore
    {
        // FIELDS: -------------------------------------------------------------------

        private const string TAG = "IAPCore";
        private List<IapDelegate> purchaseDelegates = new List<IapDelegate>();

        private bool _isInitialized = false;

        // private GleyEasyIAP.IAP.IAPManager _gleyManager;
        private Action<bool> _onRestoreComplete;
        private IapSettings _iapSettings;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(IapSettings iapSettings)
        {
            _iapSettings = iapSettings;

            /*
            PurchaseRestoreListener.Initialize();
            if (!SupersonicWisdom.Api.IsReady())
            {
                InitializeResult(false);
                return;
            }
            
            if(SupersonicWisdom.Api.IsStoreAvailable())
            {
                InitializeResult(true);
            }
            else
            {
                SupersonicWisdom.Api.AddOnIapRestorePurchaseInitializationListener(InitializeResult);
            }
            */
        }

        public void AddIapPurchaseDelegate(IapDelegate purchaseDelegate)
        {
            if (_isInitialized)
                purchaseDelegate.OnInitializeSuccess();

            purchaseDelegates.Add(purchaseDelegate);
        }

        public void RemoveIapPurchaseDelegate(IapDelegate purchaseDelegate)
        {
            purchaseDelegates.Remove(purchaseDelegate);
        }

        public virtual string GetLocalizedProductPrice(string productId)
        {
            return string.Empty;
        }

        public virtual string GetLocalizedProductPrice(ProductName productName)
        {
            return "-";
            /*
            if (!SupersonicWisdom.Api.IsReady())
                return "-";

            IapId iap = IAPManager.Instance.GetIapSettings(productName);
            if (iap == null)
                return "-";

#if SUPERSONIC_WISDOM_IAP
            var productCollection = SupersonicWisdom.Api.GetProductCollection();
            var productInfo = productCollection.WithID(iap.Id);
            if (productInfo == null)
                return "-";

            return productInfo.metadata.localizedPriceString;
#else
            return "-";
#endif
            */
        }

        public void Purchase(ProductName productName)
        {
            /*
            if (!SupersonicWisdom.Api.IsReady())
                return;

            IapId iap = IAPManager.Instance.GetIapSettings(productName);
            if (iap == null)
                return;

            SupersonicWisdom.Api.BuyProduct(iap.Id, ProductBought);
            */
        }

        public void RestorePurchases(Action<bool> onRestoreComplete)
        {
            /*
            if (!SupersonicWisdom.Api.IsReady())
            {
                onRestoreComplete.Invoke(false);
                return;
            }

            _onRestoreComplete += onRestoreComplete;
            SupersonicWisdom.Api.RestorePurchases();
            */
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        /*
        private void InitializeResult(IAPOperationStatus status, string message, List<StoreProduct> shopProducts)
        {
            if (status == IAPOperationStatus.Success)
            {
                if (!PlayerManager.IsDataRestored)
                {
                    //IAP was successfully initialized
                    //loop through all products and check which one are bought to update our variables
                    for (int i = 0; i < shopProducts.Count; i++)
                    {
                        var productName = shopProducts[i].productName;
                        switch (productName)
                        {
                            case ProductName.NoAds:
                            case ProductName.NoAdsBundle1:
                            case ProductName.NoAdsBundle2:
                                if (shopProducts[i].active)
                                    PurchaseRestoreListener.AddRestoredProduct(productName);
                                break;
                        }
                    }

                    PlayerManager.SetDataState(true);
                }

                OnInitializeSuccess();
            }
            else
            {
                OnInitializeFailed();
            }
        }

        private void InitializeResult(bool success)
        {
            if (success)
            {
                if (!PlayerManager.IsDataRestored)
                {
                    var noAds = IAPManager.GetIapSettings(_iapSettings, ProductName.NoAds);

                    if (noAds != null && SupersonicWisdom.Api.IsProductOwned(noAds.Id))
                        PurchaseRestoreListener.AddRestoredProduct(noAds.Product);

                    var noAdsBundle1 = IAPManager.GetIapSettings(_iapSettings, ProductName.NoAdsBundle1);

                    if (noAdsBundle1 != null && SupersonicWisdom.Api.IsProductOwned(noAdsBundle1.Id))
                        PurchaseRestoreListener.AddRestoredProduct(noAdsBundle1.Product);

                    var noAdsBundle2 = IAPManager.GetIapSettings(_iapSettings, ProductName.NoAdsBundle2);

                    if (noAdsBundle2 != null && SupersonicWisdom.Api.IsProductOwned(noAdsBundle2.Id))
                        PurchaseRestoreListener.AddRestoredProduct(noAdsBundle2.Product);
                }

                PlayerManager.SetDataState(true);
                OnInitializeSuccess();
            }
            else
            {
                OnInitializeFailed();
            }
        }
        
        private void OnInitializeFailed()
        {
            _isInitialized = false;
            foreach (var purchaseDelegate in purchaseDelegates)
                purchaseDelegate.OnInitializeFailure();
        }

        // Called by StoreListener 
        private void OnInitializeSuccess()
        {
            _isInitialized = true;
            foreach (var purchaseDelegate in purchaseDelegates)
                purchaseDelegate.OnInitializeSuccess();
        }

        private void ProductBought(IAPOperationStatus status, string message, StoreProduct product)
        {
            if (status == IAPOperationStatus.Success)
            {
                OnPurchaseComplete(product);
            }
            else
            {
                OnPurchaseFailure(product, message);
            }

            if (_onRestoreComplete != null)
            {
                _onRestoreComplete.Invoke(status == IAPOperationStatus.Success);
                _onRestoreComplete = null;
            }
        }

        private void ProductBought(string productId, bool isPurchased, SwPurchaseFailureReason reason)
        {
            IapId iap = IAPManager.Instance.GetIapSettings(productId);

            if (isPurchased)
                OnPurchaseComplete(iap.Product);
            else
                OnPurchaseFailure(reason.ToString());

            if (_onRestoreComplete != null)
            {
                _onRestoreComplete.Invoke(isPurchased);
                _onRestoreComplete = null;
            }
        }
        
        private void OnPurchaseComplete(StoreProduct product)
        {
            var delegates = new List<IapDelegate>(purchaseDelegates);

            foreach (var purchaseDelegate in delegates)
            {
                if (purchaseDelegate != null)
                    purchaseDelegate.OnPurchaseComplete(product.productName);
            }
        }

        private void OnPurchaseFailure(StoreProduct product, string reason)
        {
            var delegates = new List<IapDelegate>(purchaseDelegates);

            foreach (var purchaseDelegate in delegates)
            {
                if (purchaseDelegate != null)
                    purchaseDelegate.OnPurchaseFailure(reason);
            }
        }

        private void OnPurchaseComplete(ProductName product)
        {
            var delegates = new List<IapDelegate>(purchaseDelegates);

            foreach (var purchaseDelegate in delegates)
            {
                if (purchaseDelegate != null)
                    purchaseDelegate.OnPurchaseComplete(product);
            }
        }

        private void OnPurchaseFailure(string reason)
        {
            var delegates = new List<IapDelegate>(purchaseDelegates);

            foreach (var purchaseDelegate in delegates)
            {
                if (purchaseDelegate != null)
                    purchaseDelegate.OnPurchaseFailure(reason);
            }
        }
        */
    }
}