using LabraxEditor.Data;

namespace LabraxStudio.App.Services
{
    public interface IGameDataService
    {
        void SaveGameData();
        DataManager GetDataManager();
        GameData GetGameData();
    }
}