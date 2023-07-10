using LabraxStudio.App;
using LabraxStudio.App.Services;
using LabraxStudio.Meta.GameField;
using LabraxStudio.Meta.Levels;
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

        public void Initialize(LevelMeta levelMeta)
        {
            if (levelMeta.ForAdsSettings.LevelForAds)
            {
                SetPosition(levelMeta.Width, levelMeta.Height, levelMeta.ForAdsSettings.CameraOffset);
                SetupCameraSize(levelMeta.ForAdsSettings.CameraSize);
            }
            else
            {
                SetPosition(levelMeta.Width, levelMeta.Height);
                SetupCameraSize(-1);
            }

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

        private void SetPosition(int levelWidth, int levelHeight, Vector2 cameraOffset)
        {
            SetPosition(levelWidth, levelHeight);

            Vector3 newPosition = transform.localPosition;
            newPosition.x += cameraOffset.x;
            newPosition.y += cameraOffset.y;
            transform.localPosition = newPosition;
        }

        private void SetupCameraSize(float cameraSize)
        {
            _cameraZoom.Setup(this, cameraSize);
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