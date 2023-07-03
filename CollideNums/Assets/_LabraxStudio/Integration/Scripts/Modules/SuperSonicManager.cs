using System;
using LabraxStudio.AnalyticsIntegration.Ads;
using LabraxStudio.Sound;
using UnityEngine.Events;

namespace LabraxStudio.AnalyticsIntegration.Modules
{
    public class SuperSonicManager
    {
        // PROPERTIES: ----------------------------------------------------------------------------
        public bool IsSetuped => _isSetuped; 
        
        // FIELDS: -------------------------------------------------------------------

        private bool _isSetuped = false;
        private bool _rewardedDynamicState = true;
        private UnityAction _onRewardedAdClosed = null;
        private Action<bool> _onRewardedWatched = null;
        private Action _onInterstitialClose = null;
        InterstitialPlacement _interstitialPlacement = InterstitialPlacement.Undefined;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup()
        {
            _isSetuped = false;
            /*
            _isSetuped = SupersonicWisdom.Api.IsReady();
            SupersonicWisdom.Api.AddOnRewardedVideoAdRewardedListener(OnRewarded);
            SupersonicWisdom.Api.AddOnAdClosedListener(OnAdClosed);
            SupersonicWisdom.Api.AddOnAdOpenedListener(OnAdOpened);
            SupersonicWisdom.Api.AddOnRewardedVideoAdAvailableEventListener(OnRewardedOn);
            SupersonicWisdom.Api.AddOnRewardedVideoAdUnavailableEventListener(OnRewardedOff);
            */
            if(AnalyticsManager.IsPremium())
                SetPremiumUser(true);
        }

        public bool IsBannerEnabled()
        {
            if (!_isSetuped)
                return false;

            return true;
            //return !SupersonicWisdom.Api.IsNoAds();
        }

        public bool IsRewardedAvailable()
        {
            if (!_isSetuped)
#if UNITY_EDITOR
                return true;
#else
            return false;
#endif

            return true;
            //return _rewardedDynamicState && SupersonicWisdom.Api.IsRewardedVideoAvailable();
        }

        public bool IsInterstitialAvailable()
        {
            if (!_isSetuped)
#if UNITY_EDITOR
                return true;
#else
            return false;
#endif

            return true;
            /*
            bool isNoAds = SupersonicWisdom.Api.IsNoAds();
            bool isInterstitialReady = IronSource.Agent.isInterstitialReady();
                       
            return !isNoAds && isInterstitialReady;
            */
        }

        public void SendLevelStartEvent(int level, Action action)
        {
            if (!_isSetuped)
            {
                if (action != null)
                    action.Invoke();
                return;
            }

            SetInterstitialPlacement(InterstitialPlacement.UsualLevelStart);
            //SupersonicWisdom.Api.NotifyLevelStarted(level, action);
        }

        public void SendLevelCompleteEvent(int level, Action action)
        {
            if (!_isSetuped)
            {
                if (action != null)
                    action.Invoke();
                return;
            }

            SetInterstitialPlacement(InterstitialPlacement.UsualLevelComplete);
            //SupersonicWisdom.Api.NotifyLevelCompleted(level, action);
        }

        public void SendLevelFailEvent(int level, Action action)
        {
            if (!_isSetuped)
            {
                if (action != null)
                    action.Invoke();
                return;
            }

            SetInterstitialPlacement(InterstitialPlacement.UsualLevelComplete);
            //SupersonicWisdom.Api.NotifyLevelFailed(level, action);
        }

        public void ShowRewarded(Action<bool> onRewardedWatched, UnityAction onAdClosed, string placement = null)
        {
            if (!_isSetuped)
            {
                if (onRewardedWatched != null)
#if UNITY_EDITOR
                    onRewardedWatched.Invoke(true);
#else
                    onRewardedWatched.Invoke(false);
#endif
                return;
            }

            _onRewardedAdClosed = null;
            if (onAdClosed != null)
                _onRewardedAdClosed += onAdClosed;

            SoundManager.Instance.SetAllMusicOff();
            _onRewardedWatched = onRewardedWatched;
            //SupersonicWisdom.Api.ShowRewardedVideo(placement);
        }

        public void ShowInterstitial(Action action)
        {
            if (!_isSetuped)
            {
                if (action != null)
                    action.Invoke();
                return;
            }

            _onInterstitialClose = action;
           // SupersonicWisdom.Api.ShowInterstitial();
        }

        public void SendRewardedVideoMissedAfterLevelEnd(Action action)
        {
            if (!_isSetuped)
            {
                if (action != null)
                    action.Invoke();
                return;
            }

            SetInterstitialPlacement(InterstitialPlacement.RefusedWatchRV);
            //SupersonicWisdom.Api.NotifyRewardedVideoOpportunityMissedAfterLevelEnd(action);
        }

        public void ShowPrivacyWindow()
        {
            if (!_isSetuped)
                return;

            //SupersonicWisdom.Api.OpenPrivacy(null);
        }

        public void SetInterstitialPlacement(InterstitialPlacement interstitialPlacement)
        {
            _interstitialPlacement = interstitialPlacement;
        }

        public void SetPremiumUser(bool isPremium)
        {
            //SupersonicWisdom.Api.SetNoAdsUser(isPremium);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void TrackInterstitialOpened()
        {
            if (!_isSetuped)
                return;

            /*
            if (LevelManager.IsSuperLevel)
            {
                if (_interstitialPlacement != InterstitialPlacement.SuperLevelComplete)
                    SetInterstitialPlacement(InterstitialPlacement.SuperLevelIdle);
            }

            AnalyticsManager.EventsCore.TrackInterstitialAdEvent(_interstitialPlacement);

            if (_interstitialPlacement != InterstitialPlacement.MainMenu)
            {
                if (LevelManager.IsSuperLevel)
                    SetInterstitialPlacement(InterstitialPlacement.SuperLevelIdle);
                else
                    SetInterstitialPlacement(InterstitialPlacement.UsualLevelIdle);
            }
            */
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        /*
        private void OnAdOpened(SwAdUnit adunit)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                switch (adunit)
                {
                    case SwAdUnit.Interstitial:
                        TrackInterstitialOpened();
                        break;
                }
            });
        }

        private void OnAdClosed(SwAdUnit adunit)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                switch (adunit)
                {
                    case SwAdUnit.RewardedVideo:
                        if (_onRewardedAdClosed != null)
                            _onRewardedAdClosed.Invoke();
                        _onRewardedAdClosed = null;
                        SoundManager.Instance.ResetMusic();
                        break;

                    case SwAdUnit.Interstitial:
                        if (_onInterstitialClose != null)
                            _onInterstitialClose.Invoke();
                        _onInterstitialClose = null;
                        SoundManager.Instance.ResetMusic();
                        break;
                }
            });
        }

        private void OnRewarded(IronSourcePlacement placement)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                _onRewardedAdClosed = null;

                if (_onRewardedWatched != null)
                    _onRewardedWatched.Invoke(true);
                _onRewardedWatched = null;
                //SoundManager.Instance.ResetMusic();
            });
        }

        private void OnRewardedOn(IronSourceAdInfo adinfo)
        {
            Utils.ReworkPoint("Dynamic On");
            _rewardedDynamicState = true;
        }

        private void OnRewardedOff()
        {
            Utils.ReworkPoint("Dynamic Off");
            _rewardedDynamicState = false;
        }
        */
    }
}