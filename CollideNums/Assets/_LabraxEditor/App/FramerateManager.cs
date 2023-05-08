using UnityEngine;
using UnityEngine.Rendering;

namespace LabraxStudio.App
{
    public class FramerateManager
    {
        // FIELDS: --------------------------------------------------------------------------------
        
        private const int DefaultFPS = 60;
        private const int SkipFPS = 60;

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public static void SetTargetFramerate(bool defaultFps) =>
            Application.targetFrameRate = defaultFps ? DefaultFPS : SkipFPS;

        public static void SetRenderingInterval(int frames = 1) =>
            OnDemandRendering.renderFrameInterval = frames;
    }
}