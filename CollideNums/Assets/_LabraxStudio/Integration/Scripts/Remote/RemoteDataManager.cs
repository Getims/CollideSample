using LabraxStudio.App.Services;
using LabraxStudio.Data;
using UnityEngine;

namespace LabraxStudio.AnalyticsIntegration.RemoteControl
{
    public class RemoteDataManager
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public static string AbTestConfigName => _abTestConfigName;
        public static string GameConfigSource => _remoteData.GameConfigSource;
        public static GameFlowConfiguration GameFlowConfiguration => _remoteData.GameFlowConfiguration;

        // FIELDS: -------------------------------------------------------------------

        private static bool _isSetuped;
        private static RemoteData _remoteData = new RemoteData();
        private static string _abTestConfigName = LabraxAnalyticsConstants.AbTestConfigDefaultName;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public static void Initialize()
        {
            if (_isSetuped)
                return;
            
            var gameData = ServicesProvider.GameDataService.GetGameData();
            _remoteData = gameData.RemoteData;

            bool isFirstStart = gameData.IsFirstStart && !_remoteData.WasSet;
            if (!isFirstStart)
            {
                SetABTestConfigName(_remoteData.AbTestId);
                _isSetuped = true;
                return;
            }

            _abTestConfigName = LabraxAnalyticsConstants.AbTestConfigDefaultName;
            _remoteData.GameConfigSource = LabraxAnalyticsConstants.ConfigSourceBuild;

            _remoteData.WasSet = true;
            int abTestId = -1;
            //abTestId = SupersonicWisdom.Api.GetConfigValue(LabraxAnalyticsConstants.AbTestPropertyKey, -1);

            SetABTestConfigName(abTestId);
            SetConfigSource(Application.internetReachability != NetworkReachability.NotReachable);
            _isSetuped = true;
        }

        public static void SetLevelsListName(string name)
        {
            if (!_isSetuped)
                return;

            _remoteData.GameFlowConfiguration.LevelsListName = name;
            SaveData();
        }

        public static void SetMapPlatformsListName(string name)
        {
            if (!_isSetuped)
                return;

            _remoteData.GameFlowConfiguration.MapPlatformsListName = name;
            SaveData();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private static void SaveData() => ServicesProvider.GameDataService.SaveGameData();
        
        private static void SetABTestConfigName(int abTestId)
        {
            var abTestConfigName =  LabraxAnalyticsConstants.AbTestConfigDefaultName;

            if (abTestConfigName == null || abTestConfigName.Equals(string.Empty))
                _abTestConfigName = LabraxAnalyticsConstants.AbTestConfigDefaultName;
            else
                _abTestConfigName = abTestConfigName;
            _remoteData.AbTestId = abTestId;
        }

        private static void SetConfigSource(bool isRemoteConfig)
        {
            if (isRemoteConfig)
                _remoteData.GameConfigSource = LabraxAnalyticsConstants.ConfigSourceRemote;
            else
                _remoteData.GameConfigSource = LabraxAnalyticsConstants.ConfigSourceBuild;
            
            SaveData();
        }
    }
}