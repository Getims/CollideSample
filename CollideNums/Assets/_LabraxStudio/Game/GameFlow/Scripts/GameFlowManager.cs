using LabraxStudio.App;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game.Camera;
using LabraxStudio.Game.GameField;
using LabraxStudio.Game.Gates;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Managers;
using LabraxStudio.Meta;
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
            ServicesAccess.TouchService.SetTouchState(false);

            int currentLevel = ServicesAccess.PlayerDataService.CurrentLevel;
            LevelMeta levelMeta = ServicesAccess.LevelMetaService.GetLevelMeta(currentLevel);

            _gameFieldController.Initialize();
            _gameFieldController.GenerateField(levelMeta);

            _gatesController.Initialize();
            _gatesController.GenerateGates(levelMeta);
            int biggestGateNumber = _gatesController.GetBiggestGateNumber();

            _tilesController.Initialize(levelMeta, biggestGateNumber);
            _cameraController.Initialize(levelMeta.Width, levelMeta.Height);
            _gatesController.CheckGatesState();

            ServicesAccess.TouchService.SetTouchState(true);
            GameEvents.SendLevelGenerated();
            IsLevelGenerated = true;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------


        private void ReloadLevel()
        {
            // GameManager.ReloadScene();

            ServicesAccess.TouchService.SetTouchState(false);

            int currentLevel = ServicesAccess.PlayerDataService.CurrentLevel;
            LevelMeta levelMeta = ServicesAccess.LevelMetaService.GetLevelMeta(currentLevel);
            int biggestGateNumber = _gatesController.GetBiggestGateNumber();

            _tilesController.ClearTiles();
            _tilesController.Initialize(levelMeta, biggestGateNumber);
            _gatesController.CheckGatesState();

            ServicesAccess.TouchService.SetTouchState(true);
            GameEvents.SendLevelGenerated();
            IsLevelGenerated = true;
        }

        private void LoadNewLevel()
        {
            ServicesAccess.TouchService.SetTouchState(false);

            int currentLevel = ServicesAccess.PlayerDataService.CurrentLevel;
            LevelMeta levelMeta = ServicesAccess.LevelMetaService.GetLevelMeta(currentLevel);

            _gameFieldController.ClearField();
            _gameFieldController.GenerateField(levelMeta);

            _gatesController.ClearGates();
            _gatesController.GenerateGates(levelMeta);
            int biggestGateNumber = _gatesController.GetBiggestGateNumber();

            _tilesController.ClearTiles();
            _tilesController.Initialize(levelMeta, biggestGateNumber);
            _cameraController.Initialize(levelMeta.Width, levelMeta.Height);
            _gatesController.CheckGatesState();

            ServicesAccess.TouchService.SetTouchState(true);
            GameEvents.SendLevelGenerated();
            IsLevelGenerated = true;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnGameOver(bool isWin)
        {
            IsLevelGenerated = false;
            if (!isWin)
                ReloadLevel();
        }

        private void OnWinScreenClaimClicked() => LoadNewLevel();
        private void OnLevelRestartBoosterUse() => ReloadLevel();
    }
}