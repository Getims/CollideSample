using LabraxStudio.Meta;

namespace LabraxStudio.App.Services
{
    public interface IGameSettingsService
    {
        AllGlobalSettings GetGlobalSettings();
        GameSettings GetGameSettings();
    }
}
