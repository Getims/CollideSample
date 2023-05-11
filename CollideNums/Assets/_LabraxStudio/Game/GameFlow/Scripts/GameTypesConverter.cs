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

            if (cellValue <= 10)
                return GameCellType.Unlocked;

            switch (cellValue)
            {
                case 11:
                    return GameCellType.Gate2;
                case 12:
                    return GameCellType.Gate4;
                case 13:
                    return GameCellType.Gate8;
                case 14:
                    return GameCellType.Gate16;
                case 15:
                    return GameCellType.Gate32;
                case 16:
                    return GameCellType.Gate64;
            }

            return GameCellType.Locked;
        }

        public static int MatrixValueToTile(int matrixValue)
        {
            int powValue = matrixValue + 1;
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
    }
}