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

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(int levelWidth, int levelHeight)
        {
            SetPosition(levelWidth, levelHeight);

            GameFieldSprites gameFieldSprites =
                ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSprites;
            _camera.backgroundColor = gameFieldSprites.BackgroundColor;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        private void SetPosition(int levelWidth, int levelHeight)
        {
            GameFieldSettings gameFieldSettings =
                ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSettings;

            float cellSize = gameFieldSettings.CellSize;
            float offsetX = levelWidth * 0.5f * cellSize - cellSize * 0.5f;
            float offsetY = levelHeight * (-0.5f) * cellSize + cellSize * 0.5f;

            Vector3 newPosition = transform.localPosition;
            newPosition.x = offsetX;
            newPosition.y = offsetY;
            transform.localPosition = newPosition;
        }
    }
}