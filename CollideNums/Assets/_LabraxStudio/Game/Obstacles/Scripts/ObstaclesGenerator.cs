using System;
using System.Collections.Generic;
using LabraxStudio.App.Services;
using LabraxStudio.Meta.GameField;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LabraxStudio.Game.Obstacles
{
    [Serializable]
    public class ObstaclesGenerator
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Transform _obstacleContainer;

        [SerializeField]
        private AObstacle _sawPrefab;

        [SerializeField]
        private AObstacle _holePrefab;

        [SerializeField]
        private AObstacle _pushPrefab;

        [SerializeField]
        private AObstacle _stopperPrefab;

        // FIELDS: -------------------------------------------------------------------

        private GameFieldSettings _gameFieldSettings;
        private GameFieldSprites _gameFieldSprites;
        private int[,] _levelMatrix;
        private bool _isInitialized = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            _gameFieldSettings = ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSettings;
            _gameFieldSprites = ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSprites;
            _isInitialized = true;
        }

        public List<AObstacle> GenerateObstacles(int levelWidth, int levelHeight, int[,] levelMatrix)
        {
            if (!_isInitialized)
                return null;

            List<AObstacle> obstacles = new List<AObstacle>();
            _levelMatrix = levelMatrix;

            for (int i = 0; i < levelWidth; i++)
            {
                for (int j = 0; j < levelHeight; j++)
                {
                    AObstacle _AObstacle = CreateObstacle(i, j, levelMatrix[i, j]);
                    if (_AObstacle != null)
                        obstacles.Add(_AObstacle);
                }
            }

            return obstacles;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private AObstacle CreateObstacle(int x, int y, int obstacleIndex)
        {
            if (obstacleIndex <= 0)
                return null;

            ObstacleType obstacleType = (ObstacleType) obstacleIndex;
            AObstacle aObstacle = null;
            switch (obstacleType)
            {
                case ObstacleType.Null:
                    return null;
                case ObstacleType.Saw:
                    aObstacle = Object.Instantiate(_sawPrefab, _obstacleContainer);
                    break;
                case ObstacleType.Hole:
                    aObstacle = Object.Instantiate(_holePrefab, _obstacleContainer);
                    break;
                case ObstacleType.Push:
                    aObstacle = Object.Instantiate(_pushPrefab, _obstacleContainer);
                    break;
                case ObstacleType.Stopper:
                    aObstacle = Object.Instantiate(_stopperPrefab, _obstacleContainer);
                    break;
            }

            if (aObstacle == null)
                return null;

            Vector3 position = Vector3.zero;
            position.x = _gameFieldSettings.CellSize * x;
            position.y = _gameFieldSettings.CellSize * (-y);
            aObstacle.transform.localPosition = position;

            var gateDirection = GetObstacleDirection(x, y);
            aObstacle.SetupObstacle(obstacleType, new Vector2Int(x, y), _gameFieldSprites.ObstaclesSprites, gateDirection);

            return aObstacle;
        }

        private Direction GetObstacleDirection(int x, int y)
        {
            int width = _levelMatrix.GetLength(0);
            int height = _levelMatrix.GetLength(1);

            int checkLeft = x - 1 > 0 ? _levelMatrix[x - 1, y] : 0;
            int checkTop = y - 1 >= 0 ? _levelMatrix[x, y - 1] : 0;
            int checkRight = x + 1 < width ? _levelMatrix[x + 1, y] : 0;
            int checkBottom = y + 1 < height ? _levelMatrix[x, y + 1] : 0;

            if (checkLeft == 2)
                return Direction.Left;

            if (checkTop == 2)
                return Direction.Up;

            if (checkRight == 2)
                return Direction.Right;

            if (checkBottom == 2)
                return Direction.Down;

            return Direction.Null;
        }
    }
}