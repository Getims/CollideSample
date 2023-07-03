using LabraxStudio.AnalyticsIntegration.Ads;
using LabraxStudio.AnalyticsIntegration.IAP;
using LabraxStudio.App.Services;
using LabraxStudio.Data;
using mixpanel;

namespace LabraxStudio.AnalyticsIntegration.AnalyticsEvents
{
    public class EventsCore
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        private AnalyticsService AnalyticsService => ServicesProvider.AnalyticsService;

        // FIELDS: -------------------------------------------------------------------

        private bool _isInitialized = false;
        private MixpanelManager _mixpanelManager = new MixpanelManager();
        private LabraxAnalyticsSettings _labraxAnalyticsSettings;
        private AnalyticsData _analyticsData;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(LabraxAnalyticsSettings analyticsSettings)
        {
            _labraxAnalyticsSettings = analyticsSettings;
            _analyticsData = AnalyticsService.AnalyticsData;
            _isInitialized = true;

            if (_labraxAnalyticsSettings.EnableMixpanel)
            {
#if UNITY_EDITOR
                if (_labraxAnalyticsSettings.UseMixpanelInEditor)
                    _mixpanelManager.Setup(_analyticsData.IsFirstGameStart, _analyticsData.WasFirstPurchase);
#else
                _mixpanelManager.Setup(_analyticsData.IsFirstGameStart, _analyticsData.WasFirstPurchase);
#endif
                
                if (_analyticsData.SessionNumber > 0)
                {
                    RegisterMixpanelSuperProperty(LabraxAnalyticsConstants.SESSION_NUMBER_PROPERTY,
                        _analyticsData.SessionNumber);
                    TrackGameFinishEvent(_analyticsData.SessionNumber, AnalyticsService.LastSessionTime);
                }
                
                RegisterMixpanelSuperProperty(LabraxAnalyticsConstants.SESSION_NUMBER_PROPERTY,
                    _analyticsData.SessionNumber+1);
                TrackGameStartEvent(_analyticsData.SessionNumber+1);
            }

            if (!_analyticsData.WasFirstPurchase)
            {
                if (AnalyticsService.IsPremium())
                    RegisterFirstPurchase();
            }

            if (_analyticsData.IsFirstGameStart)
            {
                _analyticsData.SetFirstGameStart(false);
                // TrackLevelComplete(0, 0);
            }
        }

        public void TrackLevelStart(int level)
        {
            if (!_isInitialized)
                return;
            
            _mixpanelManager.SendLevelStartEvent(level);
            
            AnalyticsService.DebugEvent(string.Format("Level {0} start", level));
            AnalyticsService.SaveSessionTime();
        }

        public void TrackLevelRestart(int level)
        {
            if (!_isInitialized)
                return;
            
            _mixpanelManager.SendLevelRestartEvent(level);
            
            AnalyticsService.DebugEvent(string.Format("Level {0} restart", level));
            AnalyticsService.SaveSessionTime();
        }

        public void TrackLevelFail(int level)
        {
            if (!_isInitialized)
                return;

            AnalyticsService.DebugEvent(string.Format("Level {0} fail", level));
            AnalyticsService.SaveSessionTime();
        }

        public void TrackLevelComplete(int level, float time)
        {
            if (!_isInitialized)
                return;
            
            _mixpanelManager.SendLevelCompleteEvent(level, time);

            AnalyticsService.DebugEvent(string.Format(
                "Level {0} Comlete. Level time {1}", level, time.ToString()));
            AnalyticsService.SaveSessionTime();
        }

        public void TrackBoosterUse(int level, string boosterName)
        {
            if (!_isInitialized)
                return;

            _mixpanelManager.SendBoosterUseEvent(level, boosterName);
            
            AnalyticsService.DebugEvent(string.Format("Level {0}. Use booster {1}", level, boosterName));
            AnalyticsService.SaveSessionTime();
        }

        public void TrackBoosterBuy(int level, string boosterName)
        {
            if (!_isInitialized)
                return;

            _mixpanelManager.SendBoosterBuyEvent(level, boosterName);
            
            AnalyticsService.DebugEvent(string.Format("Level {0}. Buy {1} booster", level, boosterName));
            AnalyticsService.SaveSessionTime();
        }

        public void TrackRewardedAdEvent(AdReward adReward, int cashPoints)
        {
            if (!_isInitialized)
                return;

            AnalyticsService.DebugEvent(string.Format("Rewarded video: {0} ", adReward.ToString()));
            AnalyticsService.SaveSessionTime();
        }

        public void TrackIapPurchaseComplete(ProductName productName, int cashPoints)
        {
            if (!_isInitialized)
                return;

            AnalyticsService.DebugEvent(string.Format("Purchase iap: {0} ",
                AdsEnumsConverter.IapProductNameToMixpanelName(productName)));
            AnalyticsService.SaveSessionTime();
        }

        public void TrackInterstitialAdEvent(InterstitialPlacement placement)
        {
            if (!_isInitialized)
                return;

            int cashPoints = _labraxAnalyticsSettings.ChashPointsForInterstitial;
            AnalyticsService.DebugEvent(string.Format("Interstitional show: {0} - {1}", placement, cashPoints));
            AnalyticsService.SaveSessionTime();
        }

        public void RegisterMixpanelSuperProperty(string key, Value value) =>
            _mixpanelManager.RegisterSuperProperty(key, value);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void RegisterFirstPurchase()
        {
            if (_analyticsData.WasFirstPurchase)
                return;

            _analyticsData.SetFirstPurchaseState(true);
            AnalyticsService.SaveAnalyticsData();

            _mixpanelManager.CreateProfile();
        }

        private void TrackGameFinishEvent(int sessionNumber, int sessionTime)
        {
            if (!_isInitialized)
                return;

            _mixpanelManager.SendGameFinishEvent(sessionTime);
            
            AnalyticsService.DebugEvent(
                string.Format("Game finish: session {0}, time {1} ", sessionNumber, sessionTime));
            AnalyticsService.SaveSessionTime();
        }

        private void TrackGameStartEvent(int sessionNumber)
        {
            if (!_isInitialized)
                return;

            _mixpanelManager.SendGameStartEvent();
            
            AnalyticsService.DebugEvent(
                string.Format("Game start: session {0} ", sessionNumber));
            AnalyticsService.SaveSessionTime();
        }
    }
}