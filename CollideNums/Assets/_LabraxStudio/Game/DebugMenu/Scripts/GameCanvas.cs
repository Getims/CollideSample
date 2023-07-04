using LabraxStudio.App.Services;

namespace LabraxStudio.Game.Debug
{
    public class GameCanvas : CanvasDebugger
    {
        private void Awake()
        {
            ServicesProvider.DebugService.SetGameCanvas(this);
        }
    }
}