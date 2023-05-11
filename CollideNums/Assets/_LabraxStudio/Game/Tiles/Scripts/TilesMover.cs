using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class TilesMover
    {
        // FIELDS: -------------------------------------------------------------------

        private GameFieldSettings _gameFieldSettings;
        private int[,] _tilesMatrix;
        private int[,] _levelMatrix;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(int[,] levelMatrix, int[,] tilesMatrix)
        {
            _levelMatrix = levelMatrix;
            _tilesMatrix = tilesMatrix;
            _gameFieldSettings = ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSettings;
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
                switch (direction)
                {
                    case Direction.Down:
                        if (IsPlayableCell(movePoint.x, movePoint.y + 1))
                            movePoint.y = movePoint.y + 1;
                        break;
                    case Direction.Up:
                        if (IsPlayableCell(movePoint.x, movePoint.y - 1))
                            movePoint.y = movePoint.y - 1;
                        break;
                    case Direction.Left:
                        if (IsPlayableCell(movePoint.x - 1, movePoint.y))
                            movePoint.x = movePoint.x - 1;
                        break;
                    case Direction.Right:
                        if (IsPlayableCell(movePoint.x + 1, movePoint.y))
                            movePoint.x = movePoint.x + 1;
                        break;
                }
            }

            AnimateMove(tile, movePoint);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void AnimateMove(Tile tile, Vector2Int moveTo)
        {
            float time = 0.7f;
            Ease ease = Ease.InSine;

            Vector2 matrixToPosition =
                GameTypesConverter.MatrixPositionToGamePosition(moveTo, _gameFieldSettings.CellSize);
            Vector3 newPosition = tile.Position;
            newPosition.x = matrixToPosition.x;
            newPosition.y = matrixToPosition.y;

            tile.SetCell(moveTo);
            ServicesFabric.TouchService.SetTouchState(false);
            tile.transform.DOMove(newPosition, time)
                .SetEase(ease)
                .OnComplete(() => ServicesFabric.TouchService.SetTouchState(true));
        }

        private bool IsPlayableCell(int x, int y)
        {
            int width = _levelMatrix.GetLength(0);
            int height = _levelMatrix.GetLength(1);

            if (x < 0 || y < 0)
                return false;

            if (x >= width || y >= height)
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
    }
}