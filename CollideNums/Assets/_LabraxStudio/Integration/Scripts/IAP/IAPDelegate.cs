using System;
using UnityEngine;

namespace LabraxStudio.AnalyticsIntegration.IAP
{
    public class IapDelegate
    {
        // FIELDS: -------------------------------------------------------------------

        private Action _onInitializeSuccess;
        private Action<ProductName> _onPurchaseComplete;
        private Action _onPurchaseFailure;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(Action onInitializeSuccess, Action<ProductName> onPurchaseComplete, Action onPurchaseFailure)
        {
            _onInitializeSuccess = onInitializeSuccess;
            _onPurchaseComplete = onPurchaseComplete;
            _onPurchaseFailure = onPurchaseFailure;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public void OnInitializeSuccess()
        {
            // fetch/display the price of your IAPs if needed
            if (_onInitializeSuccess != null)
                _onInitializeSuccess.Invoke();
        }

        public void OnInitializeFailure(string reason = "")
        {
            // disable in-app purchases feature as they won't be available
            Debug.Log("Initialize fail:" + reason.ToString());
        }

        public void OnPurchaseComplete(ProductName productId, string purchaseValidation = "")
        {
            if (_onPurchaseComplete != null)
                _onPurchaseComplete.Invoke(productId);
        }

        public void OnPurchaseFailure(string reason = "")
        {
            Debug.Log("Buy product failed: " + reason.ToString());

            if (_onPurchaseFailure != null)
                _onPurchaseFailure.Invoke();
        }
    }
}