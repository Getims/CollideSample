using System;

namespace LabraxStudio.Game.Camera
{
    [Serializable]
    public class CameraZoom
    {
        // FIELDS: -------------------------------------------------------------------

        private const float START_SIZE = 7.15f;
        private CameraController _cameraController;
        private float _currentSize = 0;
        private bool _isSetuped = false;
        
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(CameraController cameraController)
        {
            _cameraController = cameraController;
            _isSetuped = true;
        }

        public void SetSize(float screenFactor)
        {
            if(!_isSetuped)
                return;
            
            _currentSize = START_SIZE * screenFactor;
            _cameraController.Camera.orthographicSize = _currentSize;
        }

    }
}