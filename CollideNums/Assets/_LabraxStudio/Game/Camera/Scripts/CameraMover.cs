using System;
using System.Collections.Generic;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Meta.GameField;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Game.Camera
{
    [Serializable]
    public class CameraMover
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField]
        private float _moveTime = 0.25f;

        [SerializeField]
        private Ease _moveEase = Ease.OutSine;

        // FIELDS: -------------------------------------------------------------------

        private bool _isSetuped = false;
        private UnityEngine.Camera _camera;
        private float _cellSize = 0;
        private float _maxPosition;
        private float _minPosition;
        private float _cameraSize;
        private Tweener _moveTW;
        private const float SIZE_PERCENT = 0.6f;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(bool moveCamera, UnityEngine.Camera camera, float cameraSize, float levelHeight)
        {
            _camera = camera;
            _cameraSize = cameraSize;

            GameFieldSettings gameFieldSettings =
                ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSettings;

            _cellSize = gameFieldSettings.CellSize;
            float extra = cameraSize * SIZE_PERCENT;
            _minPosition = levelHeight * (-1) * _cellSize + extra;
            _maxPosition = 0 - extra;
            _isSetuped = moveCamera;
        }

        public void FixCameraPosition(MoveAction moveAction)
        {
            if (!_isSetuped)
                return;

            List<Tile> tiles = ServicesProvider.GameFlowService.TilesController.Tiles;
            float avgPosition = 0;
            int count = 0;

            if (tiles.Count == 0)
                return;

            avgPosition = CalculateAvgPosition(moveAction, tiles, avgPosition, ref count);
            avgPosition = avgPosition / count;

            bool inBorders = CheckTilesInBorders(avgPosition);
            if (inBorders)
                return;

            float moveTime = moveAction == null ? _moveTime : moveAction.GetTime();
            MoveCamera(avgPosition, moveTime);
        }

        public void OnDestroy()
        {
            _moveTW?.Kill();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private float CalculateAvgPosition(MoveAction moveAction, List<Tile> tiles, float avgPosition, ref int count)
        {
            foreach (var tile in tiles)
            {
                if (tile != null)
                {
                    if (moveAction != null && tile == moveAction.Tile)
                    {
                        Vector2 matrixToPosition =
                            GameTypesConverter.MatrixPositionToGamePosition(moveAction.MoveTo, _cellSize);
                        avgPosition += matrixToPosition.y;
                    }
                    else
                        avgPosition += tile.Position.y;

                    count++;
                }
            }

            return avgPosition;
        }

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
            float maxBorder = position.y + _cameraSize * SIZE_PERCENT;
            float minBorder = position.y - _cameraSize * SIZE_PERCENT;

            return avgPosition <= maxBorder && avgPosition >= minBorder;
        }
    }
}