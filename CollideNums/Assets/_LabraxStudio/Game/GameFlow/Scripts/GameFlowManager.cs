using System;
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
            GameEvents.OnLevelComplete.AddListener(OnLevelComplete);
            GameEvents.OnLevelFail.AddListener(OnLevelFail);
        }

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            GameEvents.OnLevelComplete.RemoveListener(OnLevelComplete);
            GameEvents.OnLevelFail.RemoveListener(OnLevelFail);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Initialize()
        {
            ServicesFabric.TouchService.SetTouchState(false);

            int currentLevel = PlayerDataService.CurrentLevel;
            LevelMeta levelMeta = LevelMetaService.GetLevelMeta(currentLevel);

            _gameFieldController.Initialize(levelMeta);
            _gatesController.Initialize(levelMeta);
            int biggestGateNumber = _gatesController.GetBiggestGateNumber();
            _tilesController.Initialize(levelMeta, biggestGateNumber);
            _cameraController.Initialize(levelMeta.Width, levelMeta.Height);

            _gatesController.CheckGatesState();
            ServicesFabric.TouchService.SetTouchState(true);
        }

        private void SwitchLevel()
        {
            PlayerDataService.SwitchToNextLevel();
            GameManager.ReloadScene();
        }

        private void ReloadLevel()
        {
            GameManager.ReloadScene();
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnLevelComplete() => SwitchLevel();
        private void OnLevelFail() => ReloadLevel();
    }
}