using UnityEngine;

namespace LabraxStudio.Game.Debug
{
    public class CanvasDebugger : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField]
        private Canvas _canvas;

        // PUBLIC METHODS: -----------------------------------------------------------------------'

        public void SwitchState()
        {
            _canvas.enabled = !_canvas.enabled;
        }
    }
}