using System.Collections.Generic;
using LabraxStudio.Data;
using LabraxStudio.Meta;

namespace LabraxStudio.App.Services
{
    public class LevelMetaService
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public static int LevelsCount => _levelsMetaList.Count;

        public static string CurrentLevelName => GetCurrentLevelMeta().LevelName;

        // FIELDS: --------------------------------------------------------------------------------

        private static List<LevelMeta> _levelsMetaList = new List<LevelMeta>();

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public static void Initialize(List<LevelMeta> levels)
        {
            _levelsMetaList = levels;
            var levelsData = ServicesFabric.GameDataService.GetGameData().LevelsData;

            foreach (var levelMeta in _levelsMetaList)
            {
                LevelData levelData = levelsData.GetLevelData(levelMeta.LevelName);
                if (levelData == null)
                {
                    levelData = new LevelData(levelMeta.LevelName);
                    levelsData.AddLevelData(levelData);
                }

                LevelDataService.AddLevelDataToList(levelData);
            }

            LevelDataService.UnlockFirstLevel();

            if (PlayerDataService.CurrentLevel >= LevelsCount)
                PlayerDataService.SetLevel(0);
        }

        public static LevelMeta GetLevelMeta(string levelName) => _levelsMetaList.Find(m => m.LevelName == levelName);

        public static LevelMeta GetLevelMeta(int levelIndex)
        {
            if (levelIndex >= _levelsMetaList.Count)
                levelIndex = 0;

            return _levelsMetaList[levelIndex];
        }

        public static LevelMeta GetCurrentLevelMeta() => GetLevelMeta(PlayerDataService.CurrentLevel);

        public static int GetLevelIndex(string levelName)
        {
            var levelMeta = GetLevelMeta(levelName);
            int levelIndex = _levelsMetaList.IndexOf(levelMeta);
            return levelIndex;
        }
    }
}