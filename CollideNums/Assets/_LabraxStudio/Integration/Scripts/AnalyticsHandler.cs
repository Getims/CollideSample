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
        private int _levelNumber = 0;
        private int _levelStartTime = 0;
        private bool _isRestart = false;
        private bool _wasStarted = false;
        private AnalyticsService AnalyticsService => ServicesProvider.AnalyticsService;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            CommonEvents.OnSceneReload.AddListener(OnSceneReload);
            GameEvents.OnGenerateLevel.AddListener(OnLevelStart);
            GameEvents.OnGameOver.AddListener(OnGameOver);
            GameEvents.OnLevelRestartBoosterUse.AddListener(SetRestartFlag);
            UIEvents.OnMainMenuTapToPlay.AddListener(OnMainMenuTapToPlay);
        }

        private void OnDestroy()
        {
            CommonEvents.OnSceneReload.RemoveListener(OnSceneReload);
            GameEvents.OnGenerateLevel.RemoveListener(OnLevelStart);
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
            UIEvents.OnMainMenuTapToPlay.RemoveListener(OnMainMenuTapToPlay);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void TrackLevelStart()
        {
            if (!_wasStarted)
                return;

            _levelNumber = ServicesProvider.PlayerDataService.CurrentLevel;

            if (_isRestart)
                AnalyticsService.EventsCore.TrackLevelRestart(_levelNumber + 1);
            else
                AnalyticsService.EventsCore.TrackLevelStart(_levelNumber + 1);

            _isRestart = false;
        }

        private void TrackLevelComplete()
        {
            _levelStartTime = UnixTime.Now - _levelStartTime;
            AnalyticsService.EventsCore.TrackLevelComplete(_levelNumber + 1, _levelStartTime);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------
        
        private void OnSceneReload()
        {
            _wasStarted = false;
            _isRestart = false;
        }
        
        private void OnLevelStart()
        {
            _levelStartTime = UnixTime.Now;
            TrackLevelStart();
        }

        private void OnGameOver(bool isPlayerWon)
        {
            if (isPlayerWon)
            {
                _wasStarted = false;
                TrackLevelComplete();
            }
        }

        private void OnMainMenuTapToPlay()
        {
            _wasStarted = true;
            _isRestart = false;
            _levelNumber = ServicesProvider.PlayerDataService.CurrentLevel;
            AnalyticsService.EventsCore.TrackLevelStart(_levelNumber + 1);
        }

        private void SetRestartFlag()
        {
            _isRestart = true;
        }
    }
}