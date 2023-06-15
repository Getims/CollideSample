using LabraxStudio.App.Services;
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
            _gatesController.Initialize(levelMeta);
            _tilesController.Initialize(levelMeta);
            _cameraController.Initialize(levelMeta.Width, levelMeta.Height);

            _gatesController.CheckGatesState();
            ServicesFabric.TouchService.SetTouchState(true);
        }
    }
}