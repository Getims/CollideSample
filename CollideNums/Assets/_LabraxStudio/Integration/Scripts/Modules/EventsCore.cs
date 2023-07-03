using LabraxStudio.AnalyticsIntegration.Ads;
using LabraxStudio.AnalyticsIntegration.IAP;
using LabraxStudio.Data;

namespace LabraxStudio.AnalyticsIntegration.AnalyticsEvents
{
    public class EventsCore
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsMixpanelInitialized => _mixpanelManager.IsSetuped;

        // FIELDS: -------------------------------------------------------------------

        private bool _isInitialized = false;
        private MixpanelManager _mixpanelManager = new MixpanelManager();
        private LabraxAnalyticsSettings _labraxAnalyticsSettings;
        private AnalyticsData _analyticsData;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(LabraxAnalyticsSettings analyticsSettings)
        {
            _labraxAnalyticsSettings = analyticsSettings;
            _analyticsData = AnalyticsManager.Instance.AnalyticsData;
            _isInitialized = true;

            if (_labraxAnalyticsSettings.EnableMixpanel)
            {
#if UNITY_EDITOR
                if (_labraxAnalyticsSettings.UseMixpanelInEditor)
                {
                    _mixpanelManager.Setup(_analyticsData.IsFirstGameStart, _analyticsData.WasFirstPurchase);
                    RegisterMixpanelSuperProperty(LabraxAnalyticsConstants.SessionNumberProperty,
                        _analyticsData.SessionNumber);
                    if (_analyticsData.SessionNumber > 1)
                        _mixpanelManager.SendGameFinishEvent(_analyticsData.SessionNumber - 1,
                            AnalyticsManager.Instance.LastSessionTime);
                    _mixpanelManager.SendGameStartEvent(_analyticsData.SessionNumber);
                }

#else
                _mixpanelManager.Setup(_analyticsData.IsFirstGameStart, _analyticsData.WasFirstPurchase);
                RegisterMixpanelSuperProperty(LabraxAnalyticsConstants.SessionNumberProperty,
                    _analyticsData.SessionNumber);
                if (_analyticsData.SessionNumber > 1)
                    _mixpanelManager.SendGameFinishEvent(_analyticsData.SessionNumber - 1,
                        AnalyticsManager.Instance.LastSessionTime);
                _mixpanelManager.SendGameStartEvent(_analyticsData.SessionNumber);
#endif
            }

            if (!_analyticsData.WasFirstPurchase)
            {
                if (AnalyticsManager.IsPremium())
                    RegisterFirstPurchase();
            }

            if (_analyticsData.IsFirstGameStart)
            {
                _analyticsData.SetFirstGameStart(false);
                TrackLevelComplete(0, 0);
            }
        }

        public void TrackLevelStart(int level)
        {
            if (!_isInitialized)
                return;

            AnalyticsManager.DebugEvent(string.Format("Level {0} start", level));

            _mixpanelManager.SendLevelStartEvent(level, _analyticsData.SessionNumber);
        }

        public void TrackLevelRestart(int level)
        {
            if (!_isInitialized)
                return;

            AnalyticsManager.DebugEvent(string.Format("Level {0} restart", level));

            _mixpanelManager.SendLevelRestartEvent(level, _analyticsData.SessionNumber);
        }

        public void TrackLevelFail(int level)
        {
            if (!_isInitialized)
                return;

            AnalyticsManager.DebugEvent(string.Format("Level {0} fail", level));
        }

        public void TrackLevelComplete(int level, float time)
        {
            if (!_isInitialized)
                return;

            AnalyticsManager.DebugEvent(string.Format(
                "Level {0} Comlete. Session {1}. Level time {2}", level, _analyticsData.SessionNumber,
                time.ToString()));

            _mixpanelManager.SendLevelCompleteEvent(level, _analyticsData.SessionNumber, time);
        }

        public void TrackBoosterUse(int level, string boosterName)
        {
            if (!_isInitialized)
                return;

            AnalyticsManager.DebugEvent(string.Format("Level {0}. Use booster", level));
            _mixpanelManager.SendBoosterUseEvent(level, boosterName);
        }

        public void TrackBoosterBuy(int level, string boosterName)
        {
            if (!_isInitialized)
                return;

            AnalyticsManager.DebugEvent(string.Format("Level {0}. Buy {1} booster", level, boosterName));
            _mixpanelManager.SendBoosterBuyEvent(level, boosterName);
        }

        public void TrackRewardedAdEvent(AdReward adReward, int cashPoints)
        {
            if (!_isInitialized)
                return;

            AnalyticsManager.DebugEvent(string.Format("Rewarded video: {0} ", adReward.ToString()));
        }

        public void TrackIapPurchaseComplete(ProductName productName, int cashPoints)
        {
            if (!_isInitialized)
                return;

            AnalyticsManager.DebugEvent(string.Format("Purchase iap: {0} ",
                AdsEnumsConverter.IapProductNameToMixpanelName(productName)));
        }

        public void TrackInterstitialAdEvent(InterstitialPlacement placement)
        {
            if (!_isInitialized)
                return;

            int cashPoints = _labraxAnalyticsSettings.ChashPointsForInterstitial;
            AnalyticsManager.DebugEvent(string.Format("Interstitional show: {0} - {1}", placement, cashPoints));
        }

        public void RegisterMixpanelSuperProperty(string key, int value) =>
            _mixpanelManager.RegisterSuperProperty(key, value);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void RegisterFirstPurchase()
        {
            if (_analyticsData.WasFirstPurchase)
                return;

            _analyticsData.SetFirstPurchaseState(true);
            AnalyticsManager.Instance.SaveAnalyticsData();

            _mixpanelManager.CreateProfile();
        }
    }
}