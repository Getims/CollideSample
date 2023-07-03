using LabraxStudio.AnalyticsIntegration.Ads;
using LabraxStudio.App.Services;
using mixpanel;

namespace LabraxStudio.AnalyticsIntegration.AnalyticsEvents
{
    internal class MixpanelManager
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsSetuped => _isSetuped;

        // FIELDS: -------------------------------------------------------------------

        private bool _isSetuped = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(bool resetData, bool wasFirstPurchase)
        {
            Mixpanel.Init();
            _isSetuped = true;

            if (resetData)
                Mixpanel.Reset();
            if (wasFirstPurchase)
                CreateProfile();

            RegisterSuperProperties();
        }

        public void SendGameStartEvent()
        {
            if (!_isSetuped)
                return;

            Mixpanel.Track(LabraxAnalyticsConstants.START_GAME_EVENT_NAME);
        }

        public void SendGameFinishEvent(int sessionTime)
        {
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.SESSION_DURATION_PROPERTY] = sessionTime;

            Mixpanel.Track(LabraxAnalyticsConstants.FINISH_GAME_EVENT_NAME, props);
        }

        public void SendLevelStartEvent(int level)
        {
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.LEVEL_NUMBER_PROPERTY] = level;

            Mixpanel.Track(LabraxAnalyticsConstants.LEVEL_START_EVENT_NAME, props);
        }

        public void SendLevelRestartEvent(int level)
        {
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.LEVEL_NUMBER_PROPERTY] = level;

            Mixpanel.Track(LabraxAnalyticsConstants.LEVEL_RESTART_EVENT_NAME, props);
        }

        public void SendLevelFailEvent(int level)
        {
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.LEVEL_NUMBER_PROPERTY] = level;

            Mixpanel.Track(LabraxAnalyticsConstants.LEVEL_FAIL_EVENT_NAME, props);
        }
        
        public void SendLevelCompleteEvent(int level, float time)
        {
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.LEVEL_NUMBER_PROPERTY] = level;
            props[LabraxAnalyticsConstants.LEVEL_PLAY_TIME_PROPERTY] = time;

            Mixpanel.Track(LabraxAnalyticsConstants.LEVEL_COMPLETE_EVENT_NAME, props);
        }

        public void SendBoosterUseEvent(int level, string boosterName)
        {
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.LEVEL_NUMBER_PROPERTY] = level;
            props[LabraxAnalyticsConstants.BOOSTER_NAME_PROPERTY] = boosterName;

            Mixpanel.Track(LabraxAnalyticsConstants.BOOSTER_USE_EVENT_NAME, props);
        }

        public void SendBoosterBuyEvent(int level, string boosterName)
        {
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.LEVEL_NUMBER_PROPERTY] = level;
            props[LabraxAnalyticsConstants.BOOSTER_NAME_PROPERTY] = boosterName;

            Mixpanel.Track(LabraxAnalyticsConstants.BUY_BOOSTER_EVENT_NAME, props);
        }

        public void SendRewardedAdEvent(AdReward adReward, int cashPoints)
        {
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.REWARD_TYPE_PROPERTY] = AdsEnumsConverter.RewardToGADesignName(adReward);
            props[LabraxAnalyticsConstants.CASH_POINTS_PROPERTY] = cashPoints;

            Mixpanel.Track(LabraxAnalyticsConstants.REWARDED_VIDEO_EVENT_NAME, props);
        }

        public void SendInterstitialAdEvent(InterstitialPlacement placement, int cashPoints)
        {
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.INTERSTITIAL_PLACEMENT_PROPERTY] = placement.ToString();
            props[LabraxAnalyticsConstants.CASH_POINTS_PROPERTY] = cashPoints;

            Mixpanel.Track(LabraxAnalyticsConstants.INTERSTITIAL_EVENT_NAME, props);
        }

        public void RegisterSuperProperty(string key, Value value)
        {
            if (!_isSetuped)
                return;

            Mixpanel.Register(key, value);
        }

        public void CreateProfile()
        {
            if (!_isSetuped)
                return;

            Mixpanel.Identify(Mixpanel.DistinctId);
        }

        public void SetCashPointsInProfile(int cashPoints)
        {
            if (!_isSetuped)
                return;

            Mixpanel.People.Set(LabraxAnalyticsConstants.CASH_POINTS_PROPERTY, cashPoints);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void RegisterSuperProperties()
        {
            string abTest = LabraxAnalyticsConstants.AB_NONE_VALUE; // RemoteDataManager.AbTestName;
            string abConfig = LabraxAnalyticsConstants.AB_NONE_VALUE;
            string сonfigSource = ServicesProvider.RemoteDataService.GameConfigSource;

            RegisterSuperProperty(LabraxAnalyticsConstants.AB_TEST_PROPERTY, abTest);
            RegisterSuperProperty(LabraxAnalyticsConstants.AB_CONFIG_PROPERTY, abConfig);
            RegisterSuperProperty(LabraxAnalyticsConstants.CONFIG_SOURCE_PROPERTY, сonfigSource);
            RegisterSuperProperty(LabraxAnalyticsConstants.COINS_COUNT_PROPERTY,
                ServicesProvider.PlayerDataService.Money);
        }

    }
}