using System;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Meta.GameField;
using UnityEngine;

namespace LabraxStudio.Game.Camera
{
    [Serializable]
    public class CameraMover
    {
        // FIELDS: -------------------------------------------------------------------

        private bool _isSetuped = false;
        private UnityEngine.Camera _camera;
        private float _cellSize = 0;
        private float _maxPosition;
        private float _minPosition;
        private float _cameraSize;
        private Tweener _moveTW;
        private float sizePercent = 0.1f;
        private float _tileOffset = 0.05f;
        private float _moveTime = 0.25f;
        private Ease _moveEase = Ease.OutSine;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(UnityEngine.Camera camera, float cameraSize, float levelHeight, bool moveCamera,
            Ease moveEase, float moveTime)
        {
            _camera = camera;
            _cameraSize = cameraSize;
            _moveEase = moveEase;
            _moveTime = moveTime;

            GameFieldSettings gameFieldSettings =
                ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSettings;

            _cellSize = gameFieldSettings.CellSize;
            float extra = cameraSize * 0.6f;
            _minPosition = levelHeight * (-1) * _cellSize + extra;
            _maxPosition = 0 - extra;
            _tileOffset = _cellSize;
            if (sizePercent * cameraSize < _tileOffset)
                sizePercent = _tileOffset / cameraSize;

            _isSetuped = moveCamera;
        }

        public void FixCameraPosition(TrackedTile tile)
        {
            if (!_isSetuped)
                return;

            float avgPosition = 0;
            float moveTime = _moveTime;

            if (tile != null)
            {
                if (tile.IsMoving)
                {
                    avgPosition = tile.MovePosition;
                    moveTime = tile.MoveTime;
                    if (moveTime < 0)
                        return;
                }
                else
                {
                    if (tile.Tile == null)
                        return;
                    avgPosition = tile.Tile.Position.y;
                }
            }

            avgPosition += _tileOffset;
            bool inBorders = CheckTilesInBorders(avgPosition);
            if (inBorders)
                return;

            Utils.ReworkPoint("Move camera");
            MoveCamera(avgPosition, moveTime);
        }

        public void OnDestroy()
        {
            _moveTW?.Kill();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void MoveCamera(float avgPosition, float moveTime)
        {
            float moveTo = avgPosition;
            if (avgPosition > _maxPosition)
                moveTo = _maxPosition;
            if (avgPosition < _minPosition)
                moveTo = _minPosition;

            Vector3 newPosition = _camera.transform.position;
            newPosition.y = moveTo;
            _moveTW?.Kill();
            _moveTW = _camera.transform.DOMove(newPosition, moveTime)
                .SetEase(_moveEase);
        }

        private bool CheckTilesInBorders(float avgPosition)
        {
            var position = _camera.transform.position;
            float maxBorder = position.y + _cameraSize * sizePercent;
            float minBorder = position.y - _cameraSize * sizePercent;

            return avgPosition <= maxBorder && avgPosition >= minBorder;
        }
    }
}