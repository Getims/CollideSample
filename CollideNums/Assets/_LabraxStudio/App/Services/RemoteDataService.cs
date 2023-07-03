using LabraxStudio.AnalyticsIntegration;
using LabraxStudio.AnalyticsIntegration.RemoteControl;
using LabraxStudio.Data;
namespace LabraxStudio.App.Services
{
    public class RemoteDataService
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public string AbTestConfigName => _abTestConfigName;
        public string GameConfigSource => _remoteData.GameConfigSource;
        public GameFlowConfiguration GameFlowConfiguration => _remoteData.GameFlowConfiguration;

        // FIELDS: -------------------------------------------------------------------

        private bool _isSetuped;
        private RemoteData _remoteData = new RemoteData();
        private string _abTestConfigName = LabraxAnalyticsConstants.AB_NONE_VALUE;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            if (_isSetuped)
                return;
            
            GameData gameData = ServicesProvider.GameDataService.GetGameData();
            _remoteData = gameData.RemoteData;
            
            /*
            bool isFirstStart = gameData.IsFirstStart && !_remoteData.WasSet;
            if (!isFirstStart)
            {
                SetABTestConfigName(_remoteData.AbTestId);
                _isSetuped = true;
                return;
            }

            _abTestConfigName = LabraxAnalyticsConstants.AB_NONE_VALUE;
            _remoteData.GameConfigSource = LabraxAnalyticsConstants.CONFIG_SOURCE_BUILD_VALUE;

            _remoteData.WasSet = true;
            int abTestId = -1;
            //abTestId = SupersonicWisdom.Api.GetConfigValue(LabraxAnalyticsConstants.AbTestPropertyKey, -1);

            SetABTestConfigName(abTestId);
            SetConfigSource(Application.internetReachability != NetworkReachability.NotReachable);
            */
            _isSetuped = true;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void SaveData() => ServicesProvider.GameDataService.SaveGameData();
        
        private void SetABTestConfigName(int abTestId)
        {
            /*
            string abTestConfigName = LabraxAnalyticsConstants.AB_NONE_VALUE;

            if (abTestConfigName == null || abTestConfigName.Equals(string.Empty))
                _abTestConfigName = LabraxAnalyticsConstants.AB_NONE_VALUE;
            else
                _abTestConfigName = abTestConfigName;
            _remoteData.AbTestId = abTestId;
            */
        }

        private void SetConfigSource(bool isRemoteConfig)
        {
            /*
            if (isRemoteConfig)
                _remoteData.GameConfigSource = LabraxAnalyticsConstants.CONFIG_SOURCE_BUILD_VALUE;
            else
                _remoteData.GameConfigSource = LabraxAnalyticsConstants.CONFIG_SOURCE_REMOTE_VALUE;
            
            SaveData();
            */
        }
    }
}