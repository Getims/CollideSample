using LabraxStudio.AnalyticsIntegration.Ads;
using LabraxStudio.AnalyticsIntegration.AnalyticsEvents;
using LabraxStudio.AnalyticsIntegration.IAP;
using LabraxStudio.AnalyticsIntegration.Modules;
using LabraxStudio.App.Services;
using LabraxStudio.Data;
using LabraxStudio.Events;
using UnityEngine;

namespace LabraxStudio.AnalyticsIntegration
{
    public class AnalyticsService
    {
        public IAPService IAPService { get; private set; } = new IAPService();
        public bool IsIAPEnabled { get; private set; }
        public IAPCore IAPCore { get; private set; } = new IAPCore();
        public EventsCore EventsCore { get; private set; } = new EventsCore();
        public AdsCore AdsCore { get; private set; } = new AdsCore();
        public SuperSonicManager SuperSonicManager { get; private set; } = new SuperSonicManager();

        public bool IsPremium() => PremiumUser.IsPremium();
        public bool IsIAPPremium() => PremiumUser.IsIAPPremium();
        public bool HasPremiumPeriod() => PremiumUser.HasPremiumPeriod();

        public bool IsFullInitialized => _isInitialized;
        public bool WasFirstPurchase => _analyticsData.WasFirstPurchase;
        public AnalyticsData AnalyticsData => _analyticsData;
        public int LastSessionTime => _lastSessionTime;

        // FIELDS: -------------------------------------------------------------------

        private bool _isInitialized = false;
        private LabraxAnalyticsSettings _labraxAnalyticsSettings;
        private AnalyticsData _analyticsData;
        private int _startSessionTime = 0;
        private int _lastSessionTime = 0;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void EnablePremium()
        {
            SuperSonicManager.SetPremiumUser(true);
            PremiumUser.SetEnabledPremium();
        }

        public void DisablePremium()
        {
            SuperSonicManager.SetPremiumUser(false);
            PremiumUser.SetEnabledPremium(false);
        }

        public void Initialize()
        {
            _labraxAnalyticsSettings = ServicesProvider.GameSettingsService.GetGlobalSettings().LabraxAnalyticsSettings;
            IsIAPEnabled = _labraxAnalyticsSettings.IsIAPEnabled;

            _analyticsData = ServicesProvider.GameDataService.GetGameData().AnalyticsData;
            if (_isInitialized)
                return;

            _startSessionTime = UnixTime.Now;
            _lastSessionTime = _analyticsData.SessionTime;
            RegisterEvents();
            SetSetuped(true);
        }

        public void SaveSessionTime()
        {
            int time = UnixTime.Now - _startSessionTime;
            _analyticsData.SetSessionTime(time);
            SaveAnalyticsData();
        }

        public void SaveAnalyticsData() => ServicesProvider.GameDataService.SaveGameData();

        public void DebugEvent(string eventName)
        {
            string log = string.Format("Analytics Manager: {0}", eventName);
            Debug.Log(log);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void RegisterEvents()
        {
            if (_isInitialized)
                return;
            CommonEvents.AllCurrencyChanged?.RemoveListener(OnAllCurrencyChanged);
            CommonEvents.AllCurrencyChanged.AddListener(OnAllCurrencyChanged);
        }

        private void OnAllCurrencyChanged()
        {
            int coins = ServicesProvider.PlayerDataService.Money;
            EventsCore.RegisterMixpanelSuperProperty(LabraxAnalyticsConstants.COINS_COUNT_PROPERTY, coins);
        }

        private void SetSetuped(bool analyticsConsentGranted)
        {
            SuperSonicManager.Setup();
            IAPCore.Initialize(_labraxAnalyticsSettings.IapSettings);
            EventsCore.Initialize(_labraxAnalyticsSettings);

            IAPService.Setup(_labraxAnalyticsSettings.IapSettings);
            AdsCore.Initialize(_labraxAnalyticsSettings);

            _analyticsData.SetSessionNumber(_analyticsData.SessionNumber + 1);
            SaveAnalyticsData();
            _isInitialized = true;
        }
    }
}