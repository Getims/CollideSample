using LabraxStudio.Game.Debug;

namespace LabraxStudio.App.Services
{
    public class DebugService
    {
        // FIELDS: -------------------------------------------------------------------
        
        private CanvasDebugger _gameCanvas;
        private CanvasDebugger _menuCanvas;

        // PUBLIC METHODS: -----------------------------------------------------------------------
        
        public void SetGameCanvas(CanvasDebugger canvasDebugger) => _gameCanvas = canvasDebugger;
        public void SetMenuCanvas(CanvasDebugger canvasDebugger) => _menuCanvas = canvasDebugger;

        public void SwitchCanvases()
        {
            _gameCanvas?.SwitchState();
            _menuCanvas?.SwitchState();
        }
    }
}