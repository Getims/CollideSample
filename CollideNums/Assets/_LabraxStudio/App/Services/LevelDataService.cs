using System.Collections.Generic;
using LabraxStudio.Data;
using UnityEngine;

namespace LabraxStudio.App.Services
{
    public class LevelDataService
    {
        // PROPERTIES: ----------------------------------------------------------------------------
        public int UnlockedLevelsCount;

        // FIELDS: --------------------------------------------------------------------------------

        private IGameDataService _gameDataService;
        private List<LevelData> _levelsDataList = new List<LevelData>();

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Initialize()
        {
            _gameDataService = ServicesProvider.GameDataService;
            _levelsDataList = _gameDataService.GetGameData().LevelsData.LevelDatas;
        }

        public void AddLevelDataToList(LevelData levelData)
        {
            _levelsDataList.Add(levelData);
        }

        public LevelData GetCurrentLevelData() => GetLevelData(ServicesProvider.PlayerDataService.CurrentLevel);

        public LevelData GetLevelData(string levelName) => _levelsDataList.Find(d => d.LevelName == levelName);

        public LevelData GetLevelData(int levelIndex)
        {
            if (levelIndex >= _levelsDataList.Count || levelIndex < 0)
                levelIndex = 0;

            return _levelsDataList[levelIndex];
        }

        public void SetCurrentLevel(string levelName)
        {
            int levelIndex = ServicesProvider.LevelMetaService.GetLevelIndex(levelName);
            ServicesProvider.PlayerDataService.SetLevel(levelIndex);
        }

        public int CalculateUnlockedLevels()
        {
            LevelData levelData = null;
            int unlockedlevels = 0;
            int levelsCount = _levelsDataList.Count;

            for (int i = 0; i < levelsCount; i++)
            {
                levelData = GetLevelData(i);
                if (levelData != null && levelData.IsUnlocked)
                    unlockedlevels++;
                else
                    break;
            }

            UnlockedLevelsCount = unlockedlevels;
            return unlockedlevels;
        }

        public void UnlockNextLevel()
        {
            int levelsCount = _levelsDataList.Count;
            int nextLevelIndex = Mathf.Min(ServicesProvider.PlayerDataService.CurrentLevel + 1, levelsCount - 1);
            var levelData = GetLevelData(nextLevelIndex);

            if (levelData.IsUnlocked)
                return;

            levelData.SetUnlocked(true);
            _gameDataService.SaveGameData();
        }

        public void UnlockAllLevels()
        {
            foreach (var levelData in _levelsDataList)
            {
                levelData.SetUnlocked(true);
            }

            _gameDataService.SaveGameData();
        }

        public void LockAllLevels()
        {
            foreach (var levelData in _levelsDataList)
            {
                levelData.SetUnlocked(false);
            }

            _gameDataService.SaveGameData();
        }

        public void UnlockFirstLevel()
        {
            var levelData = _levelsDataList[0];
            levelData.SetUnlocked(true);
            _gameDataService.SaveGameData();
        }

        public string GetLevelsListName() => _gameDataService.GetGameData().LevelsData.LevelsListName;

        public void SetLevelsListName(string listName)
        {
            _gameDataService.GetGameData().LevelsData.SetLevelsListName(listName);
            _gameDataService.SaveGameData();
        }
    }
}