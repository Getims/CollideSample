using System.Collections.Generic;
using LabraxStudio.Data;
using UnityEngine;

namespace LabraxStudio.App.Services
{
    public static class LevelDataService
    {
        // PROPERTIES: ----------------------------------------------------------------------------
        public static int UnlockedLevelsCount;

        // FIELDS: --------------------------------------------------------------------------------

        private static IGameDataService _gameDataService;
        private static List<LevelData> _levelsDataList = new List<LevelData>();

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void Initialize()
        {
            _gameDataService = ServicesFabric.GameDataService;
        }

        public static void AddLevelDataToList(LevelData levelData)
        {
            _levelsDataList.Add(levelData); 
        }
        
        public static LevelData GetCurrentLevelData() => GetLevelData(PlayerDataService.CurrentLevel);

        public static LevelData GetLevelData(string levelName) => _levelsDataList.Find(d => d.LevelName == levelName);
        
        public static LevelData GetLevelData(int levelIndex)
        {
            if (levelIndex >= _levelsDataList.Count || levelIndex < 0)
                levelIndex = 0;

            return _levelsDataList[levelIndex];
        }

        public static void SetCurrentLevel(string levelName)
        {
            int levelIndex = LevelMetaService.GetLevelIndex(levelName);
            PlayerDataService.SetLevel(levelIndex);
        }

        public static int CalculateUnlockedLevels()
        {
            LevelData levelData = null;
            int unlockedlevels = 0;
            int levelsCount = _levelsDataList.Count;
            
            for (int i = 0; i <  levelsCount; i++)
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

        public static void UnlockNextLevel()
        {
            int levelsCount = _levelsDataList.Count;
            int nextLevelIndex = Mathf.Min(PlayerDataService.CurrentLevel + 1, levelsCount - 1);
            var levelData = GetLevelData(nextLevelIndex);

            if (levelData.IsUnlocked)
                return;

            levelData.SetUnlocked(true);
            _gameDataService.SaveGameData();
        }

        public static void UnlockAllLevels()
        {
            foreach (var levelData in _levelsDataList)
            {
                levelData.SetUnlocked(true);
            }

            _gameDataService.SaveGameData();
        }

        public static void LockAllLevels()
        {
            foreach (var levelData in _levelsDataList)
            {
                levelData.SetUnlocked(false);
            }

            _gameDataService.SaveGameData();
        }
        
        public static void UnlockFirstLevel()
        {
            var levelData = _levelsDataList[0];
            levelData.SetUnlocked(true);
            _gameDataService.SaveGameData();
        }

    }
}