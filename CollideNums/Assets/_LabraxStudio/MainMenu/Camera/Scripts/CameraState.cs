namespace LabraxStudio.MainMenu.Camera
{
    public class CameraState
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool CanMove => _canMove && !_isLocked && !_isZoom && !_isCoveredByUI;
        public bool CanZoom => !_isLocked && !_isCoveredByUI;
        public bool IsLocked => _isLocked;
        
        // FIELDS: -------------------------------------------------------------------
        
        private CameraController _cameraController;
        private bool _canMove = true;
        private bool _isZoom = false;
        private bool _isLocked = false;
        private bool _isCoveredByUI = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(CameraController cameraController)
        {
            _cameraController = cameraController;
        }

        public void SetMoveTrigger(bool canMove)
        {
            _canMove = canMove;

            if (_cameraController != null)
                _cameraController.OnStateUpdate();
        }

        public void SetZoomTrigger(bool isZoom)
        {
            _isZoom = isZoom;

            if (_cameraController != null)
                _cameraController.OnStateUpdate();
        }

        public void SetUITrigger(bool isCoveredByUI)
        {
            _isCoveredByUI = isCoveredByUI;
            if (_cameraController != null)
                _cameraController.OnStateUpdate();
        }

        public void SetLockTrigger(bool isLocked)
        {
            _isLocked = isLocked;

            if (_cameraController != null)
                _cameraController.OnStateUpdate();
        }
    }
}