using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.App.Services
{
    public class GameSettingsService : IGameSettingsService
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public GameSettingsService() =>
            _allGlobalSettings = Resources.Load<AllGlobalSettings>("GlobalSettings");

        // FIELDS: --------------------------------------------------------------------------------
        
        private readonly AllGlobalSettings _allGlobalSettings;

        // PROPERTIES: ----------------------------------------------------------------------------

        public AllGlobalSettings GetGlobalSettings() => _allGlobalSettings;

        public GameSettings GetGameSettings() => _allGlobalSettings.GameSettings;
    }
}