using LabraxStudio.Game;
using LabraxStudio.Game.Camera;
using LabraxStudio.Game.GameField;
using LabraxStudio.Game.Gates;
using LabraxStudio.Game.Tiles;

namespace LabraxStudio.App.Services
{
    public class GameFlowService
    {
        // PROPERTIES: ----------------------------------------------------------------------------
        
        public GameFieldController FieldController => _gameFieldController;
        public GatesController GatesController => _gatesController;
        public TilesController TilesController =>  _tilesController;
        public CameraController CameraController => _cameraController;
        public FailTracker FailTracker=> _failTracker;

        // FIELDS: -------------------------------------------------------------------

        private GameFieldController _gameFieldController;
        private GatesController _gatesController;
        private TilesController _tilesController;
        private CameraController _cameraController;
        private FailTracker _failTracker;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(GameFieldController gameFieldController, GatesController gatesController,
            TilesController tilesController, CameraController cameraController)
        {
            _gameFieldController = gameFieldController;
            _gatesController = gatesController;
            _tilesController = tilesController;
            _cameraController = cameraController;
            _failTracker = new FailTracker();
        }
    }
}