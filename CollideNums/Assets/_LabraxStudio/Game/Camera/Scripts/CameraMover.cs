using System;
using System.Collections.Generic;
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
        private float sizePercent = 0.1f;
        private float _tileOffset = 0.05f;
        private Tile _lastMoveTile = null;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(bool moveCamera, UnityEngine.Camera camera, float cameraSize, float levelHeight, Ease moveEase)
        {
            _camera = camera;
            _cameraSize = cameraSize;
            _moveEase = moveEase;
            
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

        public void FixCameraPosition(MoveAction moveAction)
        {
            if (!_isSetuped)
                return;

            float avgPosition = 0;

            if (moveAction != null)
                avgPosition = CalculatePosition(moveAction);
            else
            {
                if (_lastMoveTile != null)
                    return;
                
                List<Tile> tiles = ServicesProvider.GameFlowService.TilesController.Tiles;
                avgPosition = CalculatePosition(tiles);
            }

            avgPosition += _tileOffset;
            bool inBorders = CheckTilesInBorders(avgPosition);
            if (inBorders)
                return;

            float moveTime = moveAction == null ? _moveTime : moveAction.GetTime();
            MoveCamera(avgPosition, moveTime);
        }

        public void FixCameraPositionMerge(MergeAction mergeAction)
        {
            if (!_isSetuped)
                return;
            if (mergeAction == null)
                return;
            
            float avgPosition = mergeAction.MergeTo.Position.y;
            avgPosition += _tileOffset;
            _lastMoveTile = mergeAction.MergeTo;
            bool inBorders = CheckTilesInBorders(avgPosition);
            if (inBorders)
                return;

            MoveCamera(avgPosition, _moveTime);
        }

        public void OnDestroy()
        {
            _moveTW?.Kill();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private float CalculatePosition(MoveAction moveAction)
        {
            _lastMoveTile = moveAction.Tile;
            Vector2 matrixToPosition =
                GameTypesConverter.MatrixPositionToGamePosition(moveAction.MoveTo, _cellSize);

            return matrixToPosition.y;
        }

        private float CalculatePosition(List<Tile> tiles)
        {
            float minPosition = 1000;
            float cameraPosition = _camera.transform.position.y + _tileOffset;
            float result = cameraPosition;

            foreach (var tile in tiles)
            {
                if (tile != null)
                {
                    float tilePosition = tile.Position.y;
                    float distance = Math.Abs(cameraPosition - tilePosition);
                    if (distance < minPosition)
                    {
                        minPosition = distance;
                        result = tilePosition;
                    }
                }
            }

            return result;
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
            float maxBorder = position.y + _cameraSize * sizePercent;
            float minBorder = position.y - _cameraSize * sizePercent;

            return avgPosition <= maxBorder && avgPosition >= minBorder;
        }
    }
}