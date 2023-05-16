namespace LabraxStudio.App.Services
{
    public static class ServicesFabric
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public static GameDataService GameDataService => _gameDataService;
        public static GameSettingsService GameSettingsService => _gameSettingsService;
        public static TouchService TouchService => _touchService;

        // FIELDS: -------------------------------------------------------------------
        
        private static GameDataService _gameDataService = new GameDataService();
        private static GameSettingsService _gameSettingsService = new GameSettingsService();
        private static TouchService _touchService = new TouchService();
    }
}