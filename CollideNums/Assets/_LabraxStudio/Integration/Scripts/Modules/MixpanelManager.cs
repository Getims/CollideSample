using LabraxStudio.AnalyticsIntegration.Ads;
using LabraxStudio.AnalyticsIntegration.IAP;
using LabraxStudio.AnalyticsIntegration.RemoteControl;
using UnityEngine.Rendering;

//using mixpanel;

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
            /*
            Mixpanel.Init();
            _isSetuped = true;

            if (resetData)
                Mixpanel.Reset();
            if(wasFirstPurchase)
                CreateProfile();

            RegisterSuperProperties();
            */
        }

        public void SendGameStartEvent(int sessionNumber)
        {
            /*
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.SessionNumberProperty] = sessionNumber;

            Mixpanel.Track(LabraxAnalyticsConstants.StartGameEventName, props);
            */
        }

        public void SendGameFinishEvent(int sessionNumber, int sessionTime)
        {
            /*
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.SessionNumberProperty] = sessionNumber;
            props[LabraxAnalyticsConstants.SessionTimeProperty] = sessionTime;

            Mixpanel.Track(LabraxAnalyticsConstants.FinishGameEventName, props);
            */
        }

        public void SendLevelStartEvent(int level, int sessionNumber)
        {
            /*
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.LevelNumberProperty] = level;
            props[LabraxAnalyticsConstants.SessionNumberProperty] = sessionNumber;
            props[LabraxAnalyticsConstants.AttemptNumberProperty] = attemptNum;

            Mixpanel.Track(LabraxAnalyticsConstants.LevelStartEventName, props);
            */
        }

        public void SendLevelRestartEvent(int level, int sessionNumber)
        {
            /*
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.LevelNumberProperty] = level;
            props[LabraxAnalyticsConstants.AttemptNumberProperty] = attemptNum;
            props[LabraxAnalyticsConstants.SessionNumberProperty] = sessionNumber;

            Mixpanel.Track(LabraxAnalyticsConstants.LevelRestarteEventName, props);
            */
        }

        public void SendLevelCompleteEvent(int level, int sessionNumber, float time)
        {
            /*
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.LevelNumberProperty] = level;
            props[LabraxAnalyticsConstants.AttemptNumberProperty] = attemptNum;
            props[LabraxAnalyticsConstants.SessionNumberProperty] = sessionNumber;
            props[LabraxAnalyticsConstants.StarsCountProperty] = stars;
            props[LabraxAnalyticsConstants.LevelPlayTimeProperty] = time;

            Mixpanel.Track(LabraxAnalyticsConstants.LevelCompleteEventName, props);
            */
        }
        
        public void SendBoosterUseEvent(int level, string boosterName)
        {
            /*
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.LevelNumberProperty] = level;
            props[LabraxAnalyticsConstants.BoosterNameProperty] = boosterName;

            Mixpanel.Track(LabraxAnalyticsConstants.OneBoosterUseEventName, props);
            */
        }

        public void SendBoosterBuyEvent(int level, string boosterName)
        {
            /*
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.LevelNumberProperty] = level;
            props[LabraxAnalyticsConstants.BoosterNameProperty] = boosterName;

            Mixpanel.Track(LabraxAnalyticsConstants.BuyBoosterEventName, props);
            */
        }

        public void SendRewardedAdEvent(AdReward adReward, int cashPoints)
        {
            /*
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.RewardTypeProperty] = AdsEnumsConverter.RewardToGADesignName(adReward);
            props[LabraxAnalyticsConstants.CashPointsProperty] = cashPoints;

            Mixpanel.Track(LabraxAnalyticsConstants.RewardedVideoEventName, props);
            */
        }
        
        public void SendInterstitialAdEvent(InterstitialPlacement placement, int cashPoints)
        {
            /*
            if (!_isSetuped)
                return;

            var props = new Value();
            props[LabraxAnalyticsConstants.InterstitialPlacementProperty] = placement.ToString(); 
            props[LabraxAnalyticsConstants.CashPointsProperty] = cashPoints;

            Mixpanel.Track(LabraxAnalyticsConstants.InterstitialEventName, props);
            */
        }
    
        public void RegisterSuperProperty(string key, int value)
        { 
            /*
            if (!_isSetuped)
                return;
            
            Mixpanel.Register(key, value);
            */
        }

        public void CreateProfile()
        {
            /*
            if (!_isSetuped)
                return;
            
            Mixpanel.Identify(Mixpanel.DistinctId);
            */
        }

        public void SetCashPointsInProfile(int cashPoints)
        {
            /*
            if (!_isSetuped)
                return;
            
            Mixpanel.People.Set(LabraxAnalyticsConstants.CashPointsProperty, cashPoints);
            */
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void RegisterSuperProperties()
        {
            /*
            string abTest = RemoteDataManager.AbTestName;
            string abConfig = RemoteDataManager.AbTestConfigName;
            string сonfigSource = RemoteDataManager.GameConfigSource;

            RegisterSuperProperty(LabraxAnalyticsConstants.AbConfigPropertyName, abConfig);
            RegisterSuperProperty(LabraxAnalyticsConstants.AbTestPropertyName, abTest);
            RegisterSuperProperty(LabraxAnalyticsConstants.ConfigSourcePropertyName, сonfigSource);
            RegisterSuperProperty(LabraxAnalyticsConstants.СoinsCountProperty, PlayerManager.Money);
            RegisterSuperProperty(LabraxAnalyticsConstants.GemsCountProperty, PlayerManager.Gems);
            RegisterSuperProperty(LabraxAnalyticsConstants.EnergyCountProperty, PlayerManager.Energy);
            RegisterSuperProperty(LabraxAnalyticsConstants.СashPointsTotalProperty, AnalyticsManager.Instance.CashPoints);
        */
        }

    }
}