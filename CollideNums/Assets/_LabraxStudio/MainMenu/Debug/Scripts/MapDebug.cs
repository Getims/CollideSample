using LabraxStudio.App.Services;
using LabraxStudio.Managers;
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
            var level = LevelManager.LevelsCount - 1;
            LevelManager.LockAllLevels();
            PlayerManager.SetLevel(0);
            LevelManager.CalculateUnlockedLevels();
        }

        [TabGroup("All levels control"), Button(ButtonHeight = 25)]
        public void LockAllLevels()
        {
            if (!Application.isPlaying)
                return;

            LevelManager.LockAllLevels();
            PlayerManager.SetLevel(0);
            LevelManager.CalculateUnlockedLevels();
        }
        
    }
}