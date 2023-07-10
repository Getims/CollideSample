using System;
using UnityEngine;

namespace LabraxStudio.Game.Camera
{
    [Serializable]
    public class CameraZoom
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private float _startSize = 7.15f;

        // FIELDS: -------------------------------------------------------------------

        private CameraController _cameraController;
        private float _cameraBaseSize = 0;
        private float _currentSize = 0;
        private bool _isSetuped = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(CameraController cameraController, float cameraSize)
        {
            _cameraController = cameraController;

            if (cameraSize >= 0)
                _cameraBaseSize = cameraSize;
            else
                _cameraBaseSize = _startSize;

            _isSetuped = true;
        }

        public void SetSize(float screenFactor)
        {
            if (!_isSetuped)
                return;

            _currentSize = _cameraBaseSize * screenFactor;
            _cameraController.Camera.orthographicSize = _currentSize;
        }
    }
}