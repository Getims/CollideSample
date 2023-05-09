using LabraxStudio.App.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.MainMenu
{
    public class MapDebug : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private bool _autoSetupCamera = true;

        [SerializeField]
        private bool _autoStars = false;

        // FIELDS: -------------------------------------------------------------------

        private IGameDataService _gameDataService;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        [TabGroup("All levels control"), Button(ButtonHeight = 25)]
        public void UnlockAllLevels()
        {
            var level = LevelMetaService.LevelsCount - 1;
            LevelDataService.LockAllLevels();
            PlayerDataService.SetLevel(0);
            LevelDataService.CalculateUnlockedLevels();
        }

        [TabGroup("All levels control"), Button(ButtonHeight = 25)]
        public void LockAllLevels()
        {
            if (!Application.isPlaying)
                return;

            LevelDataService.LockAllLevels();
            PlayerDataService.SetLevel(0);
            LevelDataService.CalculateUnlockedLevels();
        }
        
    }
}