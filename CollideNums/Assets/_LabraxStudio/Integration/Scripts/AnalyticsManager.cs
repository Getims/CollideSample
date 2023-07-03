using LabraxStudio.AnalyticsIntegration.Ads;
using LabraxStudio.AnalyticsIntegration.AnalyticsEvents;
using LabraxStudio.Managers;
using LabraxStudio.AnalyticsIntegration.IAP;
using LabraxStudio.AnalyticsIntegration.Modules;
using LabraxStudio.App.Services;
using LabraxStudio.Data;
using UnityEngine;
using LabraxStudio.Meta;

namespace LabraxStudio.AnalyticsIntegration
{
    public class AnalyticsManager : SharedManager<AnalyticsManager>
    {
       // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private AllGlobalSettings _allGlobalSettings;

        [SerializeField]
        private IAPManager _IAPManager;

        // PROPERTIES: ----------------------------------------------------------------------------

        public static bool IsIAPEnabled { get; private set; }
        public static IAPCore IAPCore { get; private set; } = new IAPCore();
        public static EventsCore EventsCore { get; private set; } = new EventsCore();
        public static AdsCore AdsCore { get; private set; } = new AdsCore();
        public static SuperSonicManager SuperSonicManager { get; private set; } = new SuperSonicManager();

        public static bool IsPremium() => PremiumUser.IsPremium();
        public static bool IsIAPPremium() => PremiumUser.IsIAPPremium();
        public static bool HasPremiumPeriod() => PremiumUser.HasPremiumPeriod();

        public bool IsFullInitialized => _isInitialized;
        public bool WasFirstPurchase => _analyticsData.WasFirstPurchase;
        public AnalyticsData AnalyticsData => _analyticsData;
        public int SessionTime => _sessionTime;
        public int LastSessionTime => _lastSessionTime;

        // FIELDS: -------------------------------------------------------------------

        private bool _isInitialized = false;
        private IGameDataService _gameDataService;
        private LabraxAnalyticsSettings _labraxAnalyticsSettings;
        private AnalyticsData _analyticsData;
        private int _sessionTime = 0;
        private int _lastSessionTime = 0;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public static void EnablePremium()
        {
            SuperSonicManager.SetPremiumUser(true);
            PremiumUser.SetEnabledPremium();
        }
        
        public static void DisablePremium()
        {
            SuperSonicManager.SetPremiumUser(false);
            PremiumUser.SetEnabledPremium(false);
        }

        public void Setup()
        {
            _labraxAnalyticsSettings = _allGlobalSettings.LabraxAnalyticsSettings;
            IsIAPEnabled = _labraxAnalyticsSettings.IsIAPEnabled;

            _analyticsData = _gameDataService.GetGameData().AnalyticsData;
            _analyticsData.SetSessionNumber(_analyticsData.SessionNumber + 1);

            _sessionTime = UnixTime.Now;
            _lastSessionTime = _analyticsData.SessionTime;

            SetSetuped(true);
        }

        public void SaveAnalyticsData()
        {
            _gameDataService.SaveGameData();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetSetuped(bool analyticsConsentGranted)
        {
            SuperSonicManager.Setup();
            IAPCore.Initialize(_labraxAnalyticsSettings.IapSettings);
            EventsCore.Initialize(_labraxAnalyticsSettings);

            _IAPManager.Setup(_labraxAnalyticsSettings.IapSettings);
            AdsCore.Initialize(_labraxAnalyticsSettings);

            SaveAnalyticsData();
            _isInitialized = true;
        }

        public static void DebugEvent(string eventName)
        {
            string log = string.Format("Analytics Manager", $"{eventName}");
            Debug.Log(log);
        }
    }
}