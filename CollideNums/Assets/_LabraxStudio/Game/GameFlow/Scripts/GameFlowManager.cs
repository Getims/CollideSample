using System;
using LabraxStudio.App.Services;
using LabraxStudio.Game.Camera;
using LabraxStudio.Game.GameField;
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

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void Start()
        {
            Initialize();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Initialize()
        {
            ServicesFabric.TouchService.SetTouchState(false);
            
            int currentLevel = PlayerDataService.CurrentLevel;
            LevelMeta levelMeta = LevelMetaService.GetLevelMeta(currentLevel);
            
            _gameFieldController.Initialize(levelMeta);
            _tilesController.Initialize(levelMeta);
            _cameraController.Initialize(levelMeta.Width, levelMeta.Height);

            ServicesFabric.TouchService.SetTouchState(true);
        }
    }
}