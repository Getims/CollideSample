using System;
using UnityEngine;

namespace LabraxStudio.Game
{
    public static class GameTypesConverter
    {
        public static GameCellType MatrixValueToCellType(int cellValue)
        {
            if (cellValue <= 1)
                return GameCellType.Locked;

            if (cellValue == 2)
                return GameCellType.Unlocked;

            int gateInt = cellValue - 1;

            return (GameCellType) gateInt;
        }
        
        public static GameCellType TileValueToGateType(int tileValue)
        {
            int gateInt = tileValue + 1;
            
            return (GameCellType) gateInt;
        }

        public static int MatrixValueToTile(int matrixValue)
        {
            int powValue = matrixValue;
            int tileValue = (int) Math.Pow(2, powValue);
            return tileValue;
        }

        public static Vector2 MatrixPositionToGamePosition(Vector2 matrixPosition, float cellSize)
        {
            Vector2 result = Vector2.zero;

            result.x = cellSize * matrixPosition.x;
            result.y = -cellSize * matrixPosition.y;

            return result;
        }
        
        public static Vector2Int DirectionToVector2Int(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return new Vector2Int(0, 1);
                case Direction.Up:
                    return new Vector2Int(0, -1);
                case Direction.Left:
                    return new Vector2Int(-1, 0);
                case Direction.Right:
                    return new Vector2Int(1, 0);
                default:
                    return new Vector2Int(0, 0);
            }
        }
    }
}