namespace LabraxStudio.App.Services
{
    public static class ServicesAccess
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public static GameDataService GameDataService => _gameDataService;
        public static GameSettingsService GameSettingsService => _gameSettingsService;
        public static TouchService TouchService => _touchService;
        public static LevelDataService LevelDataService => _levelDataService;
        public static LevelMetaService LevelMetaService => _levelMetaService;
        public static PlayerDataService PlayerDataService => _playerDataService;

        // FIELDS: -------------------------------------------------------------------

        private static readonly GameDataService _gameDataService = new GameDataService();
        private static readonly GameSettingsService _gameSettingsService = new GameSettingsService();
        private static readonly TouchService _touchService = new TouchService();
        private static readonly LevelDataService _levelDataService = new LevelDataService();
        private static readonly LevelMetaService _levelMetaService = new LevelMetaService();
        private static readonly PlayerDataService _playerDataService = new PlayerDataService();
    }
}