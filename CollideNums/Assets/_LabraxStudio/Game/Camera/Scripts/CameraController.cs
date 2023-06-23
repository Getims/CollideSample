using LabraxStudio.App;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Game.Camera
{
    public class CameraController : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private UnityEngine.Camera _camera;

        [SerializeField]
        private CameraZoom _cameraZoom;

        // PROPERTIES: ----------------------------------------------------------------------------

        public UnityEngine.Camera Camera => _camera;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(int levelWidth, int levelHeight)
        {
            SetPosition(levelWidth, levelHeight);
            SetupCameraSize();

            GameFieldSprites gameFieldSprites =
                ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSprites;
            _camera.backgroundColor = gameFieldSprites.BackgroundColor;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetPosition(int levelWidth, int levelHeight)
        {
            GameFieldSettings gameFieldSettings =
                ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSettings;

            float cellSize = gameFieldSettings.CellSize;
            float offsetX = levelWidth * 0.5f * cellSize - cellSize * 0.5f;
            float offsetY = levelHeight * (-0.5f) * cellSize + cellSize * 0.5f;

            Vector3 newPosition = transform.localPosition;
            newPosition.x = offsetX;
            newPosition.y = offsetY;
            transform.localPosition = newPosition;
        }

        private void SetupCameraSize()
        {
            _cameraZoom.Setup(this);
            float screenFactor = 1;

            if (ScreenManager.Instance.IsPhone)
            {
                if (ScreenManager.Instance.CurrentAspectRatio < ScreenManager.Instance.BaseAspectRatio)
                    screenFactor = ScreenManager.Instance.BaseAspectRatio / ScreenManager.Instance.CurrentAspectRatio;

                _cameraZoom.SetSize(screenFactor);
            }
            else
                _cameraZoom.SetSize(screenFactor);
        }
    }
}