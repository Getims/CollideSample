using UnityEngine;
using LabraxStudio.Data;

namespace LabraxStudio.AnalyticsIntegration
{
    public static class PremiumUser
    {
        public static void SetEnabledPremium(bool enabled = true)
        {
            if (enabled == IsIAPPremium())
                return;
            
            PlayerPrefs.SetInt(PlayerPrefsConstants.PREMIUM, enabled ? 1 : 0);
            SetEnabledNotRewardedAds(!enabled);
        }

        public static void SetPremiumPeriod(bool isPremiumPeriodActive)
        {
            if (isPremiumPeriodActive)
            {
                PlayerPrefs.SetInt(PlayerPrefsConstants.PREMIUM_PERIOD, 1);
                SetEnabledNotRewardedAds(false);
            }
            else
            {
                if (HasPremiumPeriod()) // if previously the free period was active
                {
                    PlayerPrefs.SetInt(PlayerPrefsConstants.PREMIUM_PERIOD, 0);
                    if (!IsPremium()) // and not a premium user (NoAds IAP)
                    {
                        SetEnabledNotRewardedAds(true);
                    }
                }
            }
        }

        private static void SetEnabledNotRewardedAds(bool enabled)
        {
            if (!enabled)
            {
                /*
                AdsManager.Banner.Hide();
                AdsManager.Banner.Disable();
                AdsManager.Interstitial.Disable();
                AdsManager.RewardedInterstitialVideo.Disable();
                AdsManager.Mrec.Disable();
                AdsManager.NativeAds.Disable();
                VoodooCrossPromo.Hide();
                */
            }
            else
            {
                /*
                AdsManager.Banner.Enable();
                AdsManager.Interstitial.Enable();
                AdsManager.RewardedInterstitialVideo.Enable();
                AdsManager.Mrec.Enable();
                AdsManager.NativeAds.Enable();
                */
            }
        }

        public static void SetFreePeriod(bool isFreePeriodActive)
        {
            if (isFreePeriodActive)
            {
                PlayerPrefs.SetInt(PlayerPrefsConstants.PREMIUM_PERIOD, 1);
                SetEnabledNotRewardedAds(false);
            }
            else
            {
                if (PlayerPrefs.GetInt(PlayerPrefsConstants.PREMIUM_PERIOD, 0) == 1) // if previously the free period was active
                {
                    PlayerPrefs.SetInt(PlayerPrefsConstants.PREMIUM_PERIOD, 0);
                    if (!IsPremium()) // and not a premium user (NoAds IAP)
                    {
                        SetEnabledNotRewardedAds(false);
                    }
                }
            }
        }

        public static bool IsPremium() => IsIAPPremium() || HasPremiumPeriod();

        public static bool IsIAPPremium() => PlayerPrefs.GetInt(PlayerPrefsConstants.PREMIUM, 0) == 1;
        public static bool HasPremiumPeriod() => PlayerPrefs.GetInt(PlayerPrefsConstants.PREMIUM_PERIOD, 0) == 1;
    }
}