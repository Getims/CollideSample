using _LabraxStudio.Game.GameFlow.Scripts;
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
        public GameOverTracker GameOverTracker=> _gameOverTracker;
        public TasksController TasksController => _tasksController;

        // FIELDS: -------------------------------------------------------------------

        private GameFieldController _gameFieldController;
        private GatesController _gatesController;
        private TilesController _tilesController;
        private CameraController _cameraController;
        private GameOverTracker _gameOverTracker;
        private TasksController _tasksController;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(GameFieldController gameFieldController, GatesController gatesController,
            TilesController tilesController, CameraController cameraController)
        {
            _gameFieldController = gameFieldController;
            _gatesController = gatesController;
            _tilesController = tilesController;
            _cameraController = cameraController;
            _gameOverTracker = new GameOverTracker();
            _tasksController = new TasksController();
        }

        public void OnDestroy()
        {
            _tasksController.OnDestroy();
        }
    }
}