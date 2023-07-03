using UnityEngine;

namespace LabraxStudio.AnalyticsIntegration.IAP
{
    public static class IAPAlertWindow
    {
        // FIELDS: -------------------------------------------------------------------

        private const string RESTORE_SUCCESSFUL = "Your purchases has been restored!";
        private const string RESTORE_FAILED = "Failed to restore your purchases.";
        private const string PURCHASE_FAILED = "Purchase failed.";
        private const string PURCHASE_SUCCESSFUL = "Purchase success.";

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public static void ShowRestoreSuccessful(string info = null)
        {
            string message = string.Format("{0} {1}", RESTORE_SUCCESSFUL, info);
            /*
            new Alert(null, message)
                .SetPositiveButton("OK", () => { Debug.Log("Ok handler"); })
                .Show();
                */
        }

        public static void ShowRestoreFailed(string info = null)
        {
            string message = string.Format("{0} {1}", RESTORE_FAILED, info);
            /*
            new Alert(null, message)
                .SetPositiveButton("OK", () => { Debug.Log("Ok handler"); })
                .Show();
                */
        }

        public static void ShowPurchaseFailed(string info = null)
        {
            string message = string.Format("{0} {1}", PURCHASE_FAILED, info);
            /*
            new Alert(null, message)
                .SetPositiveButton("OK", () => { Debug.Log("Ok handler"); })
                .Show();
                */
        }

        public static void ShowPurchaseSuccess(string info = null)
        {
            string message = string.Format("{0} {1}", PURCHASE_SUCCESSFUL, info);
            /*
            new Alert(null, message)
                .SetPositiveButton("OK", () => { Debug.Log("Ok handler"); })
                .Show();
                */
        }
    }
}