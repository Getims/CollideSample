using System;
using LabraxStudio.AnalyticsIntegration.Modules;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.UI;
using UnityEngine.Events;

namespace LabraxStudio.AnalyticsIntegration.Ads
{
    public class AdsCore
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool CanShowInterstitial => _isSetuped &&
                                           SuperSonicManager.IsInterstitialAvailable() &&
                                           !AnalyticsService.IsPremium();

        public bool CanShowReward => _isSetuped &&
                                     SuperSonicManager.IsRewardedAvailable();

        public bool CanShowCrossPromo => SuperSonicManager.IsSetuped && !AnalyticsService.IsPremium();

        private bool ForceReward => false;
        private SuperSonicManager SuperSonicManager => ServicesProvider.AnalyticsService.SuperSonicManager;
        private AnalyticsService AnalyticsService => ServicesProvider.AnalyticsService;

        // FIELDS: -------------------------------------------------------------------

        private UnityAction _onRewardedReceived;
        private UnityAction _onAdClosed;
        private bool _isSetuped = false;
        private AdReward _rewardType;
        private int _minGamesBetweenInterstitialAds = 0;
        private LabraxAnalyticsSettings _analyticsSettings;
        private BannerTracker _bannerTracker = new BannerTracker();
        private bool _wasRewarded = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(LabraxAnalyticsSettings analyticsSettings)
        {
            _analyticsSettings = analyticsSettings;
            _isSetuped = false;
        }

        public void ShowBanner()
        {
            if (!_isSetuped)
                return;

            if (SuperSonicManager.IsBannerEnabled())
                _bannerTracker.ShowBanner();
        }

        public void HideBanner()
        {
            if (!_isSetuped)
                return;

            _bannerTracker.HideBanner();
        }

        public void ShowRewarded(AdReward adReward, UnityAction onRewarded, UnityAction onAdClosed)
        {
            if (!_isSetuped)
            {
                onAdClosed?.Invoke();
                return;
            }

            _onRewardedReceived = null;
            _onAdClosed = null;
            _rewardType = adReward;

            if (onRewarded != null)
                _onRewardedReceived += onRewarded;
            if (onAdClosed != null)
                _onAdClosed += onAdClosed;

            if (ForceReward)
                OnRewardedCallback(true);
            else
            {
                string placement = AdsEnumsConverter.RewardToGADesignName(_rewardType);
                SuperSonicManager.ShowRewarded(OnRewardedCallback, onAdClosed, placement);
            }
        }

        public void ShowInterstitial(Action onInterstitialClose = null, string interstitialTag = "")
        {
            if (!_isSetuped)
            {
                if (onInterstitialClose != null)
                    onInterstitialClose.Invoke();
                return;
            }

            SuperSonicManager.ShowInterstitial(onInterstitialClose);
        }

        public void ShowGDPRSettings()
        {
            if (!_isSetuped)
                return;

            SuperSonicManager.ShowPrivacyWindow();
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnRewardedCallback(bool rewardReceived)
        {
            if (rewardReceived && _onRewardedReceived != null)
            {
                _onRewardedReceived.Invoke();
                int cashPoints = _analyticsSettings.ChashPointsForRv;
                AnalyticsService.EventsCore.TrackRewardedAdEvent(_rewardType, cashPoints);
            }

            if (_onAdClosed != null)
                _onAdClosed.Invoke();
        }

        private void OnBannerLoaded(float height) =>
            _bannerTracker.ShowBanner(height);
    }
}