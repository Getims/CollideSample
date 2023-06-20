using System.Collections.Generic;
using LabraxStudio.Data;
using LabraxStudio.Meta;

namespace LabraxStudio.App.Services
{
    public class LevelMetaService
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public int LevelsCount => _levelsMetaList.Count;

        public string CurrentLevelName => GetCurrentLevelMeta().LevelName;

        // FIELDS: --------------------------------------------------------------------------------

        private List<LevelMeta> _levelsMetaList = new List<LevelMeta>();

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(List<LevelMeta> levels)
        {
            _levelsMetaList = levels;
            var levelsData = ServicesProvider.GameDataService.GetGameData().LevelsData;

            foreach (var levelMeta in _levelsMetaList)
            {
                LevelData levelData = levelsData.GetLevelData(levelMeta.LevelName);
                if (levelData == null)
                {
                    levelData = new LevelData(levelMeta.LevelName);
                    levelsData.AddLevelData(levelData);
                }

                ServicesProvider.LevelDataService.AddLevelDataToList(levelData);
            }

            ServicesProvider.LevelDataService.UnlockFirstLevel();

            if (ServicesProvider.PlayerDataService.CurrentLevel >= LevelsCount)
                ServicesProvider.PlayerDataService.SetLevel(0);
        }

        public LevelMeta GetLevelMeta(string levelName) => _levelsMetaList.Find(m => m.LevelName == levelName);

        public LevelMeta GetLevelMeta(int levelIndex)
        {
            if (levelIndex >= _levelsMetaList.Count)
                levelIndex = 0;

            return _levelsMetaList[levelIndex];
        }

        public LevelMeta GetCurrentLevelMeta() => GetLevelMeta(ServicesProvider.PlayerDataService.CurrentLevel);

        public int GetLevelIndex(string levelName)
        {
            var levelMeta = GetLevelMeta(levelName);
            int levelIndex = _levelsMetaList.IndexOf(levelMeta);
            return levelIndex;
        }
    }
}