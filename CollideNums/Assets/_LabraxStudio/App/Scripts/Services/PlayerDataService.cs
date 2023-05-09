using LabraxStudio.Data;
using UnityEngine;

namespace LabraxStudio.App.Services
{
    public static class PlayerDataService
    {
        // PROPERTIES: -----------------------------------------------------------------------------

        public static bool IsFirstStart => _gameData.IsFirstStart;
        public static bool IsSoundOn => _playerData.IsSoundOn;
        public static bool IsMusicOn => _playerData.IsMusicOn;
        public static int CurrentLevel => _playerData.CurrentLevel;

        // FIELDS: --------------------------------------------------------------------------------

        private static IGameDataService _gameDataService;
        private static GameData _gameData;
        private static PlayerData _playerData;
        private static bool _isInitialized;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void Initialize()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
            _gameDataService = ServicesFabric.GameDataService;
            _gameData = _gameDataService.GetGameData();
            _playerData = _gameData.PlayerData;

            if (!_gameData.IsFirstStart)
                return;

            _playerData.SetMoney( ServicesFabric.GameSettingsService.GetGameSettings().BalanceSettings.StartMoney);
            _gameDataService.SaveGameData();
        }

        public static void AddMoney(int amount)
        {
            int money = _playerData.Money + amount;

            _playerData.SetMoney(money);
            _gameDataService.SaveGameData();
        }

        public static void SpendMoney(int amount)
        {
            int money = _playerData.Money - amount;

            _playerData.SetMoney(money);
            _gameDataService.SaveGameData();
        }
        
        public static void SetMoney(int amount)
        {
            int money = Mathf.Max(amount, 0);

            _playerData.SetMoney(money);
            _gameDataService.SaveGameData();
        }
        
        public static void SetLevel(int levelIndex)
        {
            levelIndex = Mathf.Min(levelIndex, LevelMetaService.LevelsCount - 1);
            _playerData.SetCurrentLevel(levelIndex);
            _gameDataService.SaveGameData();
        }

        public static void SwitchToNextLevel()
        {
            int newLevel = _playerData.CurrentLevel + 1;
            if (newLevel >= LevelMetaService.LevelsCount)
                newLevel = 0;

            _playerData.SetCurrentLevel(newLevel);
            _gameDataService.SaveGameData();
        }

        public static void SwitchToNextLevelNoReset()
        {
            int newLevel = Mathf.Min(_playerData.CurrentLevel + 1, LevelMetaService.LevelsCount - 1);
            _playerData.SetCurrentLevel(newLevel);
            _gameDataService.SaveGameData();
        }

        public static void SwitchToPreviousLevel()
        {
            int newLevel = _playerData.CurrentLevel - 1;
            _playerData.SetCurrentLevel(newLevel);
            _gameDataService.SaveGameData();
        }

        public static void SetTutorialState(bool isTutorialDone)
        {
            _playerData.SetTutorialState(isTutorialDone);
            _gameDataService.SaveGameData();
        }

        public static void SetSoundState(bool enabled)
        {
            _playerData.SetSoundState(enabled);
            _gameDataService.SaveGameData();
        }

        public static void SetMusicState(bool enabled)
        {
            _playerData.SetMusicState(enabled);
            _gameDataService.SaveGameData();
        }

        public static void SetFirstStartState(bool state)
        {
            _gameData.IsFirstStart = state;
            _gameDataService.SaveGameData();
        }

    }
}