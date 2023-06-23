using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game.Camera;
using LabraxStudio.Game.GameField;
using LabraxStudio.Game.Gates;
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

        // FIELDS: -------------------------------------------------------------------

        public static bool IsLevelGenerated = false;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            GameEvents.OnGameOver.AddListener(OnGameOver);
            GameEvents.OnLevelRestartBoosterUse.AddListener(OnLevelRestartBoosterUse);
            UIEvents.OnWinScreenClaimClicked.AddListener(OnWinScreenClaimClicked);
        }

        private void OnDestroy()
        {
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
            UIEvents.OnWinScreenClaimClicked.AddListener(OnWinScreenClaimClicked);
            GameEvents.OnLevelRestartBoosterUse.RemoveListener(OnLevelRestartBoosterUse);
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            ServicesProvider.GameFlowService.Setup(_gameFieldController,
                _gatesController, _tilesController, _cameraController);

            ServicesProvider.TouchService.SetTouchState(false);

            int currentLevel = ServicesProvider.PlayerDataService.CurrentLevel;
            LevelMeta levelMeta = ServicesProvider.LevelMetaService.GetLevelMeta(currentLevel);

            _gameFieldController.Initialize();
            _gameFieldController.GenerateField(levelMeta);

            _gatesController.Initialize();
            _gatesController.GenerateGates(levelMeta);

            _tilesController.Initialize(levelMeta);
            _cameraController.Initialize(levelMeta.Width, levelMeta.Height);
            _gatesController.CheckGatesState();

            ServicesProvider.TouchService.SetTouchState(true);
            GameEvents.SendLevelGenerated();
            IsLevelGenerated = true;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------


        private void ReloadLevel()
        {
            // GameManager.ReloadScene();

            ServicesProvider.TouchService.SetTouchState(false);

            int currentLevel = ServicesProvider.PlayerDataService.CurrentLevel;
            LevelMeta levelMeta = ServicesProvider.LevelMetaService.GetLevelMeta(currentLevel);

            _tilesController.ClearTiles();
            _tilesController.Initialize(levelMeta);
            _gatesController.CheckGatesState();

            ServicesProvider.TouchService.SetTouchState(true);
            GameEvents.SendLevelGenerated();
            IsLevelGenerated = true;
        }

        private void LoadNewLevel()
        {
            ServicesProvider.TouchService.SetTouchState(false);

            int currentLevel = ServicesProvider.PlayerDataService.CurrentLevel;
            LevelMeta levelMeta = ServicesProvider.LevelMetaService.GetLevelMeta(currentLevel);

            _gameFieldController.ClearField();
            _gameFieldController.GenerateField(levelMeta);

            _gatesController.ClearGates();
            _gatesController.GenerateGates(levelMeta);

            _tilesController.ClearTiles();
            _tilesController.Initialize(levelMeta);
            _cameraController.Initialize(levelMeta.Width, levelMeta.Height);
            _gatesController.CheckGatesState();

            ServicesProvider.TouchService.SetTouchState(true);
            GameEvents.SendLevelGenerated();
            IsLevelGenerated = true;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnGameOver(bool isWin)
        {
            IsLevelGenerated = false;

            if (isWin)
                return;

            int currentLevel = ServicesProvider.PlayerDataService.CurrentLevel;
            LevelMeta levelMeta = ServicesProvider.LevelMetaService.GetLevelMeta(currentLevel);
            if (levelMeta.BoostersSettings.Count == 0 ||
                levelMeta.BoostersSettings.Find(b => b.BoosterMeta.BoosterType == BoosterType.LevelRestart) == null)
                ReloadLevel();
        }

        private void OnWinScreenClaimClicked() => LoadNewLevel();
        private void OnLevelRestartBoosterUse() => ReloadLevel();
    }
}