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

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            GameEvents.OnGameOver.AddListener(OnGameOver);
            UIEvents.OnWinScreenClaimClicked.AddListener(SwitchLevel);
        }

        private void OnDestroy()
        {
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
            UIEvents.OnWinScreenClaimClicked.AddListener(SwitchLevel);
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            ServicesAccess.TouchService.SetTouchState(false);

            int currentLevel = ServicesAccess.PlayerDataService.CurrentLevel;
            LevelMeta levelMeta = ServicesAccess.LevelMetaService.GetLevelMeta(currentLevel);

            _gameFieldController.Initialize(levelMeta);
            _gatesController.Initialize(levelMeta);
            int biggestGateNumber = _gatesController.GetBiggestGateNumber();
            _tilesController.Initialize(levelMeta, biggestGateNumber);
            _cameraController.Initialize(levelMeta.Width, levelMeta.Height);
            _gatesController.CheckGatesState();
            
            ServicesAccess.TouchService.SetTouchState(true);
            GameEvents.SendLevelGenerated();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        

        private void ReloadLevel()
        {
            GameManager.ReloadScene();
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------


        private void OnGameOver(bool isWin)
        {
            if(!isWin)
                ReloadLevel();
        }

        private void SwitchLevel()
        {
            Initialize();
        }
    }
}