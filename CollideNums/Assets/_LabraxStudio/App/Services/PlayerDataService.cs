using LabraxStudio.Data;
using UnityEngine;

namespace LabraxStudio.App.Services
{
    public class PlayerDataService
    {
        // PROPERTIES: -----------------------------------------------------------------------------

        public bool IsFirstStart => _gameData.IsFirstStart;
        public bool IsSoundOn => _playerData.IsSoundOn;
        public bool IsMusicOn => _playerData.IsMusicOn;
        public int CurrentLevel => _playerData.CurrentLevel;
        public int Money => _playerData.Money;

        // FIELDS: --------------------------------------------------------------------------------

        private IGameDataService _gameDataService;
        private GameData _gameData;
        private PlayerData _playerData;
        private bool _isInitialized;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Initialize()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
            _gameDataService = ServicesAccess.GameDataService;
            _gameData = _gameDataService.GetGameData();
            _playerData = _gameData.PlayerData;

            if (!_gameData.IsFirstStart)
                return;

            _playerData.SetMoney(ServicesAccess.GameSettingsService.GetGameSettings().BalanceSettings.StartMoney);
            _gameDataService.SaveGameData();
        }

        public void AddMoney(int amount)
        {
            int money = _playerData.Money + amount;

            _playerData.SetMoney(money);
            _gameDataService.SaveGameData();
        }

        public void SpendMoney(int amount)
        {
            int money = _playerData.Money - amount;

            _playerData.SetMoney(money);
            _gameDataService.SaveGameData();
        }

        public void SetMoney(int amount)
        {
            int money = Mathf.Max(amount, 0);

            _playerData.SetMoney(money);
            _gameDataService.SaveGameData();
        }

        public void SetLevel(int levelIndex)
        {
            levelIndex = Mathf.Min(levelIndex, ServicesAccess.LevelMetaService.LevelsCount - 1);
            _playerData.SetCurrentLevel(levelIndex);
            _gameDataService.SaveGameData();
        }

        public void SwitchToNextLevel()
        {
            int newLevel = _playerData.CurrentLevel + 1;
            if (newLevel >= ServicesAccess.LevelMetaService.LevelsCount)
                newLevel = 0;

            _playerData.SetCurrentLevel(newLevel);
            _gameDataService.SaveGameData();
        }

        public void SwitchToNextLevelNoReset()
        {
            int newLevel = Mathf.Min(_playerData.CurrentLevel + 1, ServicesAccess.LevelMetaService.LevelsCount - 1);
            _playerData.SetCurrentLevel(newLevel);
            _gameDataService.SaveGameData();
        }

        public void SwitchToPreviousLevel()
        {
            int newLevel = _playerData.CurrentLevel - 1;
            if (newLevel < 0)
                newLevel = 0;
            _playerData.SetCurrentLevel(newLevel);
            _gameDataService.SaveGameData();
        }

        public void SetTutorialState(bool isTutorialDone)
        {
            _playerData.SetTutorialState(isTutorialDone);
            _gameDataService.SaveGameData();
        }

        public void SetSoundState(bool enabled)
        {
            _playerData.SetSoundState(enabled);
            _gameDataService.SaveGameData();
        }

        public void SetMusicState(bool enabled)
        {
            _playerData.SetMusicState(enabled);
            _gameDataService.SaveGameData();
        }

        public void SetFirstStartState(bool state)
        {
            _gameData.IsFirstStart = state;
            _gameDataService.SaveGameData();
        }
    }
}