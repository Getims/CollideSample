using UnityEngine;

namespace LabraxStudio.AnalyticsIntegration.AnalyticsEvents
{
    public class FacebookManager
    {
        private bool _isSetuped = false;

        public void Setup()
        {
            _isSetuped = true;
           
            /*
            if (!FB.IsInitialized)
            {
                // Initialize the Facebook SDK
                FB.Init(InitCallback, OnHideUnity);
            }
            else
            {
                // Already initialized, signal an app activation App Event
                FB.ActivateApp();
            }
            */
        } 

        public void UpdateAttStatus(bool enabled)
        {
            /*
#if UNITY_EDITOR || UNITY_IPHONE || UNITY_IOS
            if (!_isSetuped)
                return;

            try
            {
                AudienceNetwork.AdSettings.SetAdvertiserTrackingEnabled(enabled);
            }
            catch (System.Exception)
            {
                Debug.LogWarning("AudienceNetwork failed to set");
            }
#endif
*/
        }

        private void InitCallback()
        {
            /*
            if (FB.IsInitialized)
            {
                // Signal an app activation App Event
                FB.ActivateApp();
                // Continue with Facebook SDK
                // ...
#if UNITY_EDITOR || UNITY_IPHONE || UNITY_IOS
               // UpdateAttStatus(MaxSdk.GetSdkConfiguration().AppTrackingStatus == MaxSdkBase.AppTrackingStatus.Authorized);
#endif
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
            */
        }

        private void OnHideUnity(bool isGameShown)
        {
            if (!_isSetuped)
                return;

            if (!isGameShown)
            {
                // Pause the game - we will need to hide
                Time.timeScale = 0;
            }
            else
            {
                // Resume the game - we're getting focus again
                Time.timeScale = 1;
            }
        }
    }
}
