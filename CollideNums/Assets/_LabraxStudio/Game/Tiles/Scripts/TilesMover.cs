using System;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    [Serializable]
    public class TilesMover
    {
        // FIELDS: -------------------------------------------------------------------

        private GameFieldSettings _gameFieldSettings;
        [ShowInInspector]
        private int[,] _tilesMatrix;
        private int[,] _levelMatrix;
        private int _width;
        private int _height;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(int[,] levelMatrix, int[,] tilesMatrix)
        {
            _levelMatrix = (int[,]) levelMatrix.Clone();
            _tilesMatrix = (int[,]) tilesMatrix.Clone();
            _gameFieldSettings = ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSettings;

            _width = _levelMatrix.GetLength(0);
            _height = _levelMatrix.GetLength(1);
        }

        public void MoveTile(Tile tile, Direction direction, Swipe swipe)
        {
            int moves = 0;
            int path = 0;
            Vector2Int startPoint = tile.Cell;
            Vector2Int movePoint = startPoint;

            switch (swipe)
            {
                case Swipe.Short:
                    moves = 1;
                    break;
                case Swipe.Long:
                    moves = 2;
                    break;
                case Swipe.Infinite:
                    moves = 10;
                    break;
            }

            for (int i = 0; i < moves; i++)
            {
                var tempPoint = movePoint;

                switch (direction)
                {
                    case Direction.Down:
                        tempPoint.y = tempPoint.y + 1;
                        break;
                    case Direction.Up:
                        tempPoint.y = tempPoint.y - 1;
                        break;
                    case Direction.Left:
                        tempPoint.x = tempPoint.x - 1;
                        break;
                    case Direction.Right:
                        tempPoint.x = tempPoint.x + 1;
                        break;
                }

                if (IsPlayableCell(tempPoint.x, tempPoint.y))
                {
                    if (HasTile(tempPoint.x, tempPoint.y))
                    {
                        break;
                    }
                    else
                    {
                        movePoint = tempPoint;
                    }
                }
            }

            RemoveTileFromMatrix(tile.Cell.x, tile.Cell.y);
            SetTileToMatrix(movePoint.x, movePoint.y, tile.Value+1);
            tile.SetCell(movePoint);
            AnimateMove(tile, movePoint);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void AnimateMove(Tile tile, Vector2Int moveTo)
        {
            Vector2 matrixToPosition =
                GameTypesConverter.MatrixPositionToGamePosition(moveTo, _gameFieldSettings.CellSize);
            Vector3 newPosition = tile.Position;
            newPosition.x = matrixToPosition.x;
            newPosition.y = matrixToPosition.y;

            Ease ease = Ease.OutSine;
            float time = CalculateTime(0.155f, tile.Position - newPosition);

            ServicesFabric.TouchService.SetTouchState(false);
            tile.transform.DOMove(newPosition, time)
                .SetEase(ease)
                .OnComplete(() => ServicesFabric.TouchService.SetTouchState(true));
        }

        private bool IsPlayableCell(int x, int y)
        {
            if (x < 0 || y < 0)
                return false;

            if (x >= _width || y >= _height)
                return false;

            GameCellType gameCellType = GameTypesConverter.MatrixValueToCellType(_levelMatrix[x, y]);
            switch (gameCellType)
            {
                case GameCellType.Locked:
                    return false;
                case GameCellType.Unlocked:
                    return true;
                case GameCellType.Gate2:
                case GameCellType.Gate4:
                case GameCellType.Gate8:
                case GameCellType.Gate16:
                case GameCellType.Gate32:
                case GameCellType.Gate64:
                    return true;
            }

            return false;
        }

        private bool HasTile(int x, int y)
        {
            if (x < 0 || y < 0)
                return false;

            if (x >= _width || y >= _height)
                return false;

            if (_tilesMatrix[x, y] != 0)
                return true;

            return false;
        }

        private void RemoveTileFromMatrix(int x, int y)
        {
            _tilesMatrix[x, y] = 0;
        }

        private void SetTileToMatrix(int x, int y, int value)
        {
            _tilesMatrix[x, y] = value;
        }

        private float CalculateTime(float oneCellTime, Vector3 moveDelta)
        {
            float x = Math.Abs(moveDelta.x);
            float y = Math.Abs(moveDelta.y);
            float n = 0;
            
            if (x > 0)
                n = x;

            if (y > 0)
                n = y;

            float time =  oneCellTime + (n - 1) * oneCellTime/ (n - n  * 0.35f);
            Utils.ReworkPoint("Time: " + time);
            return time;
        }
    }
}