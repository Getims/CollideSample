using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game.Background;
using LabraxStudio.Game.Camera;
using LabraxStudio.Game.GameField;
using LabraxStudio.Game.Gates;
using LabraxStudio.Game.Obstacles;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Managers;
using LabraxStudio.Meta.Levels;
using UnityEngine;

namespace LabraxStudio.Game
{
    public class GameFlowManager : SharedManager<GameFlowManager>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GameFieldController _gameFieldController;

        [SerializeField]
        private TilesController _tilesController;

        [SerializeField]
        private CameraController _cameraController;

        [SerializeField]
        private GatesController _gatesController;

        [SerializeField]
        private ObstaclesController _obstaclesController;

        [SerializeField]
        private BackgroundController _backgroundController;

        // FIELDS: -------------------------------------------------------------------

        public static bool IsLevelGenerated = false;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            GameEvents.OnGameOver.AddListener(OnGameOver);
            GameEvents.OnLevelRestartBoosterUse.AddListener(OnLevelRestartBoosterUse);
            GameEvents.OnTileDestroyedByObstacle.AddListener(OnTileDestroyedByObstacle);
            UIEvents.OnWinScreenClaimClicked.AddListener(OnWinScreenClaimClicked);
        }

        private void OnDestroy()
        {
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
            UIEvents.OnWinScreenClaimClicked.AddListener(OnWinScreenClaimClicked);
            GameEvents.OnLevelRestartBoosterUse.RemoveListener(OnLevelRestartBoosterUse);
            GameEvents.OnTileDestroyedByObstacle.RemoveListener(OnTileDestroyedByObstacle);
            ServicesProvider.GameFlowService.OnDestroy();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            ServicesProvider.GameFlowService.Setup(_gameFieldController, _gatesController,
                _obstaclesController, _tilesController, _cameraController);

            LoadLevel();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void LoadLevel()
        {
            LockTouch();
            LevelMeta levelMeta = ServicesProvider.LevelMetaService.GetCurrentLevelMeta();

            _gameFieldController.Initialize();
            _gatesController.Initialize();
            _obstaclesController.Initialize();

            _cameraController.Initialize(levelMeta);
            _backgroundController.Initialize();
            _gameFieldController.GenerateField(levelMeta);
            _gatesController.GenerateGates(levelMeta);
            _obstaclesController.GenerateObstacles(levelMeta);
            _tilesController.Initialize(levelMeta);

            _gatesController.CheckGatesState();
            ServicesProvider.GameFlowService.GameOverTracker.ResetFailFlag();
            ServicesProvider.GameFlowService.GameOverTracker.ResetWinFlag();
            ServicesProvider.GameFlowService.TasksController.Initialize(levelMeta.TaskSettings);

            SetLevelGenerated();
        }

        private void ReloadLevel()
        {
            LockTouch();
            LevelMeta levelMeta = ServicesProvider.LevelMetaService.GetCurrentLevelMeta();

            _tilesController.RemoveAllTiles();
            _tilesController.Initialize(levelMeta);

            _gatesController.CheckGatesState();
            ServicesProvider.GameFlowService.GameOverTracker.ResetFailFlag();
            ServicesProvider.GameFlowService.GameOverTracker.ResetWinFlag();
            ServicesProvider.GameFlowService.TasksController.Initialize(levelMeta.TaskSettings);

            SetLevelGenerated();
        }

        private void LoadNewLevel()
        {
            LockTouch();
            LevelMeta levelMeta = ServicesProvider.LevelMetaService.GetCurrentLevelMeta();

            _gameFieldController.ClearField();
            _gatesController.ClearGates();
            _obstaclesController.ClearObstacles();
            _tilesController.RemoveAllTiles();

            _cameraController.Initialize(levelMeta);
            _backgroundController.Initialize();
            _gameFieldController.GenerateField(levelMeta);
            _gatesController.GenerateGates(levelMeta);
            _obstaclesController.GenerateObstacles(levelMeta);
            _tilesController.Initialize(levelMeta);

            _gatesController.CheckGatesState();
            ServicesProvider.GameFlowService.GameOverTracker.ResetFailFlag();
            ServicesProvider.GameFlowService.GameOverTracker.ResetWinFlag();
            ServicesProvider.GameFlowService.TasksController.Initialize(levelMeta.TaskSettings);

            SetLevelGenerated();
        }

        private static void SetLevelGenerated()
        {
            UnlockTouch();
            GameEvents.SendLevelGenerated();
            IsLevelGenerated = true;
        }

        private static void UnlockTouch() =>
            ServicesProvider.TouchService.SetTouchState(true);

        private static void LockTouch() =>
            ServicesProvider.TouchService.SetTouchState(false);

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnGameOver(bool isWin)
        {
            IsLevelGenerated = false;

            if (isWin)
                return;

            LevelMeta levelMeta = ServicesProvider.LevelMetaService.GetCurrentLevelMeta();
            if (levelMeta.BoostersSettings.Count == 0 ||
                levelMeta.BoostersSettings.Find(b => b.BoosterMeta.BoosterType == BoosterType.LevelRestart) == null)
                ReloadLevel();
        }

        private void OnWinScreenClaimClicked() => LoadNewLevel();
        private void OnLevelRestartBoosterUse() => ReloadLevel();

        private void OnTileDestroyedByObstacle()
        {
            int currentLevel = ServicesProvider.PlayerDataService.CurrentLevel + 1;
            ServicesProvider.AnalyticsService.EventsCore.TrackLevelFail(currentLevel);
            ReloadLevel();
        }
    }
}