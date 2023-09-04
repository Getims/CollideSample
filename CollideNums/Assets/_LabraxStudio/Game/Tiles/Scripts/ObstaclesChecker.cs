using LabraxStudio.Meta.Levels;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class ObstaclesChecker
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public ObstaclesChecker(LevelMeta levelMeta)
        {
            _obstaclesMatrix = levelMeta.ObstaclesMatrix;
            _width = _obstaclesMatrix.GetLength(0);
            _height = _obstaclesMatrix.GetLength(1);
        }
        
        public ObstaclesChecker(int[,] obstaclesMatrix)
        {
            _obstaclesMatrix = obstaclesMatrix;
            _width = _obstaclesMatrix.GetLength(0);
            _height = _obstaclesMatrix.GetLength(1);
        }

        // FIELDS: -------------------------------------------------------------------

        private int[,] _obstaclesMatrix;
        private int _width;
        private int _height;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public (ObstacleType, Vector2Int) CheckObstacle(int x, int y)
        {
            return (GetValue(x, y), new Vector2Int(x, y));
        }

        public (ObstacleType, Vector2Int) CheckObstacle(int x, int y, Vector2Int moveVector, ObstacleType currentType)
        {
            if (currentType == ObstacleType.Hole)
                return (ObstacleType.Hole, new Vector2Int(x, y));

            (bool, ObstacleType, Vector2Int) checkResult;
            //Check saw on path
            if (currentType != ObstacleType.Stopper)
            {
                checkResult = CheckCustom(x + moveVector.x, y + moveVector.y, ObstacleType.Saw);
                if (checkResult.Item1)
                    return (checkResult.Item2, checkResult.Item3);
            }

            checkResult = CheckCustom(x - 1, y, ObstacleType.Push);
            if (checkResult.Item1)
                return (checkResult.Item2, checkResult.Item3);

            checkResult = CheckCustom(x + 1, y, ObstacleType.Push);
            if (checkResult.Item1)
                return (checkResult.Item2, checkResult.Item3);

            checkResult = CheckCustom(x, y - 1, ObstacleType.Push);
            if (checkResult.Item1)
                return (checkResult.Item2, checkResult.Item3);

            checkResult = CheckCustom(x, y + 1, ObstacleType.Push);
            if (checkResult.Item1)
                return (checkResult.Item2, checkResult.Item3);

            return (currentType, new Vector2Int(x, y));
        }

        public (ObstacleType, Vector2Int) CheckObstacle(Vector2Int currentCell, Vector2Int moveVector, ObstacleType currentType)
        {
            return CheckObstacle(currentCell.x, currentCell.y, moveVector, currentType);
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private ObstacleType GetValue(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _width || y >= _height)
                return 0;

            return (ObstacleType) _obstaclesMatrix[x, y];
        }

        private (bool, ObstacleType, Vector2Int) CheckCustom(int x, int y, ObstacleType type)
        {
            bool isEqual = GetValue(x, y) == type;
            return (isEqual, type, new Vector2Int(x, y));
        }
    }
}