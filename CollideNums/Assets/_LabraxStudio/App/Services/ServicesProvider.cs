using LabraxStudio.AnalyticsIntegration;

namespace LabraxStudio.App.Services
{
    public static class ServicesProvider
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public static GameDataService GameDataService => _gameDataService;
        public static GameSettingsService GameSettingsService => _gameSettingsService;
        public static TouchService TouchService => _touchService;
        public static LevelDataService LevelDataService => _levelDataService;
        public static LevelMetaService LevelMetaService => _levelMetaService;
        public static PlayerDataService PlayerDataService => _playerDataService;
        public static GameFlowService GameFlowService => _gameFlowService;
        public static MusicService MusicService => _musicService;
        public static TutorialService TutorialService => _tutorialService;
        public static RemoteDataService RemoteDataService => _remoteDataService;
        public static AnalyticsService AnalyticsService => _analyticsService;

        // FIELDS: -------------------------------------------------------------------

        private static readonly GameDataService _gameDataService = new GameDataService();
        private static readonly GameSettingsService _gameSettingsService = new GameSettingsService();
        private static readonly TouchService _touchService = new TouchService();
        private static readonly LevelDataService _levelDataService = new LevelDataService();
        private static readonly LevelMetaService _levelMetaService = new LevelMetaService();
        private static readonly PlayerDataService _playerDataService = new PlayerDataService();
        private static readonly GameFlowService _gameFlowService = new GameFlowService();
        private static readonly MusicService _musicService = new MusicService();
        private static readonly TutorialService _tutorialService = new TutorialService();
        private static readonly RemoteDataService _remoteDataService = new RemoteDataService();
        private static readonly AnalyticsService _analyticsService = new AnalyticsService();
    }
}