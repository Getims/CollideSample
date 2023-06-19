using LabraxStudio.Data;

namespace LabraxStudio.App.Services
{
    public class GameDataService : IGameDataService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public GameDataService()
        {
            _dataManager = new DataManager();
            _dataManager.PrepareData();
        }

        // FIELDS: --------------------------------------------------------------------------------

        private readonly DataManager _dataManager;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SaveGameData() =>
            _dataManager.SaveGameData();

        public DataManager GetDataManager() => _dataManager;

        public GameData GetGameData() =>
            _dataManager.GameData;
    }
}