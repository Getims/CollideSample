using LabraxStudio.Meta;
using LabraxStudio.Meta.GameField;
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
        private GameTheme _selectedGameTheme;

        // PROPERTIES: ----------------------------------------------------------------------------

        public AllGlobalSettings GetGlobalSettings() => _allGlobalSettings;

        public GameSettings GetGameSettings() => _allGlobalSettings.GameSettings;

        public SoundSettings GetSoundSettings => _allGlobalSettings.SoundSettings;

        public GameTheme SelectedGameTheme => _selectedGameTheme;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetGameTheme(GameTheme gameTheme) => _selectedGameTheme = gameTheme;
    }
}