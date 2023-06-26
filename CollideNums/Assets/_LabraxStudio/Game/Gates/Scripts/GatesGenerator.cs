using System;
using System.Collections.Generic;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LabraxStudio.Game.Gates
{
    [Serializable]
    public class GatesGenerator
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Transform _gateContainer;

        [SerializeField]
        private Gate _gatePrefab;

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

        public List<Gate> GenerateGates(int levelWidth, int levelHeight, int[,] levelMatrix)
        {
            if (!_isInitialized)
                return null;

            List<Gate> gates = new List<Gate>();
            _levelMatrix = levelMatrix;

            for (int i = 0; i < levelWidth; i++)
            {
                for (int j = 0; j < levelHeight; j++)
                {
                    Gate _gate = CreateGate(i, j, levelMatrix[i, j] - 1);
                    if (_gate != null)
                        gates.Add(_gate);
                }
            }

            return gates;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private Gate CreateGate(int x, int y, int spriteIndex)
        {
            if (spriteIndex < 2)
                return null;

            Gate _gate = Object.Instantiate(_gatePrefab, _gateContainer);
            Vector3 position = Vector3.zero;
            position.x = _gameFieldSettings.CellSize * x;
            position.y = _gameFieldSettings.CellSize * (-y);

            _gate.transform.localPosition = position;

            int gateType = 0;
            var gateDirection = GetGateDirection(x, y, ref gateType);
            _gate.SetupGate(spriteIndex - 2, _gameFieldSprites, gateDirection, gateType);
            _gate.SetCell(new Vector2Int(x, y));
            _gate.SetType((GameCellType) spriteIndex);

            return _gate;
        }

        private Direction GetGateDirection(int x, int y, ref int type)
        {
            int width = _levelMatrix.GetLength(0);
            int height = _levelMatrix.GetLength(1);

            int checkLeft = x - 1 > 0 ? _levelMatrix[x - 1, y] : 0;
            int checkLeftTop = x - 1 >= 0 && y - 1 >= 0 ? _levelMatrix[x - 1, y - 1] : 0;
            int checkTop = y - 1 >= 0 ? _levelMatrix[x, y - 1] : 0;
            int checkRightTop = x + 1 < width && y - 1 >= 0 ? _levelMatrix[x + 1, y - 1] : 0;
            int checkRight = x + 1 < width ? _levelMatrix[x + 1, y] : 0;
            int checkRightBottom = x + 1 < width && y + 1 < height ? _levelMatrix[x + 1, y + 1] : 0;
            int checkBottom = y + 1 < height ? _levelMatrix[x, y + 1] : 0;
            int checkLeftBottom = x - 1 >= 0 && y + 1 < height ? _levelMatrix[x - 1, y + 1] : 0;

            if (checkLeft == 2)
            {
                if (checkLeftTop == 2 && checkLeftBottom != 2)
                    type = 1;

                if (checkLeftTop != 2 && checkLeftBottom == 2)
                    type = 2;
                
                if (checkLeftTop != 2 && checkLeftBottom != 2)
                    type = 3;

                return Direction.Left;
            }

            if (checkTop == 2)
            {
                if (checkLeftTop == 2 && checkRightTop != 2)
                    type = 1;

                if (checkLeftTop != 2 && checkRightTop == 2)
                    type = 2;
                
                if (checkLeftTop != 2 && checkRightTop != 2)
                    type = 3;

                return Direction.Up;
            }

            if (checkRight == 2)
            {
                if (checkRightTop == 2 && checkRightBottom != 2)
                    type = 1;

                if (checkRightTop != 2 && checkRightBottom == 2)
                    type = 2;
                
                if (checkRightTop != 2 && checkRightBottom != 2)
                    type = 3;

                return Direction.Right;
            }

            if (checkBottom == 2)
                return Direction.Down;

            return Direction.Null;
        }
    }
}