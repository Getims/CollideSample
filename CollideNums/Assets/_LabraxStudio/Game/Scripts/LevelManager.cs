using System.Collections.Generic;
using LabraxStudio.App;
using LabraxStudio.Data;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Managers
{
    public static class LevelManager
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public static int LevelsCount => _levelsMetaList.Count;
        public static string CurrentLevelName => GetCurrentLevelMeta().LevelName;
        public static int UnlockedLevelsCount;

        // FIELDS: --------------------------------------------------------------------------------

        private static IGameDataService _gameDataService;
        private static List<LevelData> _levelsDataList = new List<LevelData>();
        private static List<LevelMeta> _levelsMetaList = new List<LevelMeta>();
        private static LevelsData _levelsData;
        private static bool _isInitialized;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void Initialize(List<LevelMeta> levels)
        {
            _isInitialized = true;
            _gameDataService = ServicesFabric.GameDataService;
            _levelsData = _gameDataService.GetGameData().LevelsData;
            _levelsMetaList = levels;

            SetupDatas();
            UnlockFirstLevel();
            CheckLevelIndex();

            void SetupDatas()
            {
                foreach (var levelMeta in _levelsMetaList)
                {
                    LevelData levelData = _levelsData.GetLevelData(levelMeta.LevelName);
                    if (levelData == null)
                    {
                        levelData = new LevelData(levelMeta.LevelName);
                        _levelsData.AddLevelData(levelData);
                    }

                    _levelsDataList.Add(levelData);
                }
            }

            void CheckLevelIndex()
            {
                if (PlayerManager.CurrentLevel >= LevelsCount)
                    PlayerManager.SetLevel(0);
            }
        }

        public static LevelData GetCurrentLevelData() => GetLevelData(PlayerManager.CurrentLevel);

        public static LevelMeta GetCurrentLevelMeta() => GetLevelMeta(PlayerManager.CurrentLevel);

        public static LevelData GetLevelData(string levelName) => _levelsDataList.Find(d => d.LevelName == levelName);

        public static LevelMeta GetLevelMeta(string levelName) => _levelsMetaList.Find(m => m.LevelName == levelName);

        public static LevelData GetLevelData(int levelIndex)
        {
            if (levelIndex >= _levelsDataList.Count || levelIndex < 0)
                levelIndex = 0;

            return _levelsDataList[levelIndex];
        }

        public static LevelMeta GetLevelMeta(int levelIndex)
        {
            if (levelIndex >= _levelsDataList.Count)
                levelIndex = 0;

            return _levelsMetaList[levelIndex];
        }

        public static void SetCurrentLevel(string levelName)
        {
            int levelIndex = GetLevelIndex(levelName);
            PlayerManager.SetLevel(levelIndex);
        }

        public static int CalculateUnlockedLevels()
        {
            LevelData levelData = null;
            int unlockedlevels = 0;

            for (int i = 0; i < LevelsCount; i++)
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
            int nextLevelIndex = Mathf.Min(PlayerManager.CurrentLevel + 1, LevelsCount - 1);
            var levelData = GetLevelData(nextLevelIndex);

            if (levelData.IsUnlocked)
                return;

            levelData.SetUnlocked(true);
            _gameDataService.SaveGameData();
        }

        public static int GetLevelIndex(string levelName)
        {
            var levelMeta = GetLevelMeta(levelName);
            int levelIndex = _levelsMetaList.IndexOf(levelMeta);
            return levelIndex;
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

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static void UnlockFirstLevel()
        {
            var levelData = _levelsDataList[0];
            levelData.SetUnlocked(true);
            _gameDataService.SaveGameData();
        }
    }
}