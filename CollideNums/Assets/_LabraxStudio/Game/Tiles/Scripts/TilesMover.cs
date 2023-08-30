using System;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    [Serializable]
    public class TilesMover
    {
        // FIELDS: -------------------------------------------------------------------

        private int[,] _tilesMatrix;
        private int[,] _levelMatrix;
        private int[,] _obstaclesMatrix;
        private int _width;
        private int _height;
        private int _infiniteMovesCount;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(int[,] levelMatrix, int[,] tilesMatrix, int[,] obstaclesMatrix, int infiniteMovesCount)
        {
            _levelMatrix = levelMatrix;
            _tilesMatrix = tilesMatrix;
            _obstaclesMatrix = obstaclesMatrix;

            _width = _levelMatrix.GetLength(0);
            _height = _levelMatrix.GetLength(1);
            _infiniteMovesCount = infiniteMovesCount > 0 ? infiniteMovesCount : 25;
        }

        public MoveAction CalculateMoveAction(Tile tile, Direction direction, Swipe swipe)
        {
            CheckResult result = new CheckResult();
            int moves = 0;
            Vector2Int startPoint = tile.Cell;
            Vector2Int movePoint = startPoint;
            Vector2Int moveVector = GameTypesConverter.DirectionToVector2Int(direction);

            switch (swipe)
            {
                case Swipe.OneTile:
                    moves = 1;
                    break;
                case Swipe.TwoTiles:
                    moves = 2;
                    break;
                case Swipe.Infinite:
                    moves = _infiniteMovesCount;
                    break;
            }

            for (int i = 0; i < moves; i++)
            {
                var tempPoint = movePoint;
                tempPoint += moveVector;

                CheckCell(tempPoint.x, tempPoint.y, tile.Value, ref result);
                if (!result.IsPlayableCell)
                    break;

                if (HasTile(tempPoint.x, tempPoint.y))
                    break;

                movePoint = tempPoint;
                if (result.Obstacle != ObstacleType.Null)
                    break;
            }

            RemoveTileFromMatrix(tile.Cell.x, tile.Cell.y);
            SetTileToMatrix(movePoint.x, movePoint.y, tile.Value);
            tile.SetCell(movePoint);
            if (result.NeedMoveToGate)
            {
                result.CollideWithGate = false;
                tile.SetGateFlag();
            }

            return new MoveAction(tile, movePoint, swipe, direction, result.CollideWithGate);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private CheckResult CheckCell(int x, int y, int tileValue, ref CheckResult result)
        {
            if (x < 0 || y < 0 || x >= _width || y >= _height)
            {
                result.IsPlayableCell = false;
                return result;
            }

            result.GameCellType = GameTypesConverter.MatrixValueToCellType(_levelMatrix[x, y]);
            switch (result.GameCellType)
            {
                case GameCellType.Locked:
                    result.IsPlayableCell = false;
                    break;
                case GameCellType.Unlocked:
                    result.IsPlayableCell = true;
                    result.Obstacle = (ObstacleType) _obstaclesMatrix[x, y];
                    break;
                default:
                    GameCellType tileGate = GameTypesConverter.TileValueToGateType(tileValue);
                    bool isEqualType = tileGate == result.GameCellType;
                    result.CollideWithGate = true;
                    if (isEqualType)
                        result.NeedMoveToGate = true;

                    result.IsPlayableCell = isEqualType;
                    break;
            }

            return result;
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

        private ObstacleType HasObstacle(int x, int y)
        {
            return ObstacleType.Null;
        }

        private void RemoveTileFromMatrix(int x, int y)
        {
            _tilesMatrix[x, y] = 0;
        }

        private void SetTileToMatrix(int x, int y, int value)
        {
            _tilesMatrix[x, y] = value;
        }

        private struct CheckResult
        {
            public bool IsPlayableCell;
            public GameCellType GameCellType;
            public bool NeedMoveToGate;
            public bool CollideWithGate;
            public ObstacleType Obstacle;

            public CheckResult(bool isPlayableCell = false, GameCellType gameCellType = GameCellType.Locked,
                bool needMoveToGate = false, bool collideWithGate = false, ObstacleType obstacle = ObstacleType.Null)
            {
                IsPlayableCell = isPlayableCell;
                GameCellType = gameCellType;
                NeedMoveToGate = needMoveToGate;
                CollideWithGate = collideWithGate;
                Obstacle = obstacle;
            }
        }
    }
}