using Lean.Touch;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.MainMenu.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private LeanPinchScale _leanPinchScale;

        [Space(10)]
        [SerializeField, MinMaxSlider(1, 3, true)]
        private Vector2 _zoomRange = Vector2.one;

        // FIELDS: -------------------------------------------------------------------

        private const float START_SIZE = 5.5f;
        private CameraController _cameraController;
        private float _baseSize = 0;
        private float _currentSize = 0;
        private bool _isZooming = false;
        private bool _isSetuped = false;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void LateUpdate()
        {
            if (_isSetuped && _isZooming)
            {
                float currentZoom = transform.localScale.x;
                currentZoom = Mathf.Clamp(currentZoom, _zoomRange.x, _zoomRange.y);
                transform.localScale = Vector3.one * currentZoom;

                _currentSize = _baseSize / currentZoom;
                _cameraController.Camera.orthographicSize = _currentSize;
            }
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(CameraController cameraController)
        {
            _cameraController = cameraController;
            _isSetuped = true;
        }

        public void SetSize(float screenFactor)
        {
            if (!_isSetuped)
                return;

            _baseSize = START_SIZE;
            _baseSize = _baseSize * screenFactor;
            _currentSize = _baseSize;
            _cameraController.Camera.orthographicSize = _baseSize;
        }

        public void CheckState()
        {
            if (!_isSetuped)
                return;

            _isZooming = _isZooming && _cameraController.State.CanZoom;
            _leanPinchScale.enabled = _isZooming;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetZoom(bool enabled)
        {
            _isZooming = enabled && _cameraController.State.CanZoom;
            _cameraController.State.SetZoomTrigger(_isZooming);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public void OnFingerDown(LeanFinger finger)
        {
            if (!_isSetuped)
                return;

            if (LeanTouch.Fingers.Count >= 2)
                SetZoom(true);
        }

        public void OnFingerUp(LeanFinger finger)
        {
            if (!_isSetuped)
                return;

            if (LeanTouch.Fingers.Count <= 2)
                SetZoom(false);
        }
    }
}