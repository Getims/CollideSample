using LabraxStudio.App;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game.Camera;
using LabraxStudio.Game.GameField;
using LabraxStudio.Game.Gates;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Game
{
    public class GameFlowManager : MonoBehaviour
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
        }

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Initialize()
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
            GameEvents.OnGenerateLevel.Invoke();
        }

        private void SwitchLevel()
        {
            ServicesAccess.PlayerDataService.SwitchToNextLevel();
            GameManager.ReloadScene();
        }

        private void ReloadLevel()
        {
            GameManager.ReloadScene();
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------


        private void OnGameOver(bool isWin)
        {
            if (isWin)
                OnLevelComplete();
            else
                OnLevelFail();
        }

        private void OnLevelComplete() => SwitchLevel();
        private void OnLevelFail() => ReloadLevel();
    }
}