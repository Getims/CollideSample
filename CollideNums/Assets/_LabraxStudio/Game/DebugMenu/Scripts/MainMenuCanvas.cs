using LabraxStudio.App.Services;

namespace LabraxStudio.Game.Debug
{
    public class MainMenuCanvas : CanvasDebugger
    {
        private void Awake()
        {
            ServicesProvider.DebugService.SetMenuCanvas(this);
        }
    }
}