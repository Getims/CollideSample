using LabraxStudio.App.Services;
using LabraxStudio.Data;
using LabraxStudio.Events;
using UnityEngine;

namespace LabraxStudio.AnalyticsIntegration
{
    public class AnalyticsHandler : MonoBehaviour
    {
        // FIELDS: --------------------------------------------------------------------------------

        private LevelData currentLevelData;
        private int levelNumber = 0;
        private int _levelTime = 0;
        private bool _isRestart = false;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            GameEvents.OnGenerateLevel.AddListener(OnLevelStart);
            GameEvents.OnGameOver.AddListener(OnGameOver);
            GameEvents.OnLevelRestartBoosterUse.AddListener(SetRestartFlag);
        }

        private void OnDestroy()
        {
            GameEvents.OnGenerateLevel.RemoveListener(OnLevelStart);
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
        }

        private void TrackLevelStart()
        {
            levelNumber = ServicesProvider.PlayerDataService.CurrentLevel;

            if (_isRestart)
                AnalyticsManager.EventsCore.TrackLevelRestart(levelNumber + 1);
            else
                AnalyticsManager.EventsCore.TrackLevelStart(levelNumber + 1);

            _isRestart = false;
        }

        private void TrackLevelComplete()
        {
            _levelTime = UnixTime.Now - _levelTime;

            AnalyticsManager.EventsCore.TrackLevelComplete(levelNumber + 1, _levelTime);
        }


        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnLevelStart()
        {
            _levelTime = UnixTime.Now;
            TrackLevelStart();
        }

        private void OnGameOver(bool isPlayerWon)
        {
            if (isPlayerWon)
                TrackLevelComplete();
        }
        
        private void SetRestartFlag()
        {
            _isRestart = true;
        }
    }
}