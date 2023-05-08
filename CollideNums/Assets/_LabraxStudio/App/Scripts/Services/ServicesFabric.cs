using LabraxStudio.App.Services;

namespace LabraxStudio.App
{
    public static class ServicesFabric
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public static GameDataService GameDataService => _gameDataService;
        public static GameSettingsService GameSettingsService => _gameSettingsService;

        // FIELDS: -------------------------------------------------------------------
        private static GameDataService _gameDataService = new GameDataService();
        private static GameSettingsService _gameSettingsService = new GameSettingsService();
    }
}