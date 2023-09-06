using System;
using System.Collections.Generic;
using LabraxStudio.App.Services;
using LabraxStudio.Meta.GameField;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LabraxStudio.Game.GameField
{
    [Serializable]
    public class GameFieldGenerator
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Transform _cellsContainer;

        [SerializeField]
        private FieldCell _fieldCellPrefab;

        // FIELDS: -------------------------------------------------------------------

        private GameFieldSettings _gameFieldSettings;
        private GameFieldSprites _gameFieldSprites;
        private int[,] _levelMatrix;
        private int _width;
        private int _height;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            _gameFieldSettings = ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSettings;
            _gameFieldSprites = ServicesProvider.GameSettingsService.SelectedGameTheme.GameSprites;
        }

        public List<FieldCell> GenerateFieldSprites(int levelWidth, int levelHeight, int[,] levelMatrix)
        {
            List<FieldCell> gameField = new List<FieldCell>();
            _levelMatrix = levelMatrix;
            _width = levelWidth;
            _height = levelHeight;

            for (int i = 0; i < levelWidth; i++)
            {
                for (int j = 0; j < levelHeight; j++)
                {
                    var fieldCell = CreateCell(i, j, levelMatrix[i, j] - 1);
                    if (fieldCell != null)
                        gameField.Add(fieldCell);
                }
            }

            return gameField;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private FieldCell CreateCell(int x, int y, int spriteIndex)
        {
            Sprite sprite = null;
            bool isGate = false;
            if (spriteIndex == 0)
                sprite = GetNotPlayableSprite(x, y);
            else if (spriteIndex == 1)
                sprite = GetPlayableSprite(x, y);
            else
                return null;

            if (sprite == null)
                sprite = _gameFieldSprites.ErrorSprite;

            FieldCell newCell = Object.Instantiate(_fieldCellPrefab, _cellsContainer);

            Vector3 position = Vector3.zero;
            position.x = _gameFieldSettings.CellSize * x;
            position.y = _gameFieldSettings.CellSize * (-y);

            newCell.transform.localPosition = position;
            newCell.SetName(GenerateName(x, y));
            newCell.SetSprite(sprite, spriteIndex == 0);
            return newCell;
        }

        private Sprite GetPlayableSprite(int x, int y)
        {
            int checkLeft = GetMatrixValue(x - 1, y);
            int checkTop = GetMatrixValue(x, y - 1);
            int checkRight = GetMatrixValue(x + 1, y);
            int checkBottom = GetMatrixValue(x, y + 1);

            int type = 0;
            type += checkLeft == 1 ? 1 : 0;
            type += checkTop == 1 ? 2 : 0;
            type += checkRight == 1 ? 4 : 0;
            type += checkBottom == 1 ? 8 : 0;

            switch (type)
            {
                case 1:
                    return _gameFieldSprites.PlayableSprites.LeftBackground;
                case 2:
                    return _gameFieldSprites.PlayableSprites.TopBackground;
                case 3:
                    return _gameFieldSprites.PlayableSprites.TopLeftCorner;
                case 4:
                    return _gameFieldSprites.PlayableSprites.RightBackground;
                case 5:
                    return _gameFieldSprites.PlayableSprites.LeftRightBackground;
                case 6:
                    return _gameFieldSprites.PlayableSprites.TopRightCorner;
                case 7:
                    return _gameFieldSprites.PlayableSprites.FullTopCorner;
                case 8:
                    return _gameFieldSprites.PlayableSprites.BottomBackground;
                case 9:
                    return _gameFieldSprites.PlayableSprites.BottomLeftCorner;
                case 10:
                    return _gameFieldSprites.PlayableSprites.TopBottomBackground;
                case 11:
                    return _gameFieldSprites.PlayableSprites.FullLeftCorner;
                case 12:
                    return _gameFieldSprites.PlayableSprites.BottomRightCorner;
                case 13:
                    return _gameFieldSprites.PlayableSprites.FullDownCorner;
                case 14:
                    return _gameFieldSprites.PlayableSprites.FullRightCorner;
            }

            return _gameFieldSprites.PlayableSprites.BaseBackground;
        }

        private Sprite GetNotPlayableSprite(int x, int y)
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

            int type = 0;
            type += checkLeft >= 2 ? 1 : 0;
            type += checkLeftTop >= 2 ? 2 : 0;
            type += checkTop >= 2 ? 4 : 0;
            type += checkRightTop >= 2 ? 8 : 0;
            type += checkRight >= 2 ? 16 : 0;
            type += checkRightBottom >= 2 ? 32 : 0;
            type += checkBottom >= 2 ? 64 : 0;
            type += checkLeftBottom >= 2 ? 128 : 0;

            Sprite sprite = _gameFieldSprites.NotPlayableSprites.GetSpriteByRule(type);
            if (sprite != null)
                return sprite;

            return _gameFieldSprites.NotPlayableSprites.Background;
        }

        private int GetMatrixValue(int x, int y)
        {
            if (x < 0 || x >= _width)
                return 0;
            if (y < 0 || y >= _height)
                return 0;

            return _levelMatrix[x, y];
        }

        private string GenerateName(int x, int y)
        {
            return string.Format("FieldCell [{0};{1}]", x, y);
        }
    }
}