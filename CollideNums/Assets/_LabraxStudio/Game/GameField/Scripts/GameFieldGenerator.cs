using System;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
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

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            _gameFieldSettings = ServicesAccess.GameSettingsService.GetGameSettings().GameFieldSettings;
            _gameFieldSprites = ServicesAccess.GameSettingsService.GetGameSettings().GameFieldSprites;
        }

        public void GenerateFieldSprites(int levelWidth, int levelHeight, int[,] levelMatrix)
        {
            _levelMatrix = levelMatrix;

            for (int i = 0; i < levelWidth; i++)
            {
                for (int j = 0; j < levelHeight; j++)
                {
                    CreateCell(i, j, levelMatrix[i, j] - 1);
                }
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CreateCell(int x, int y, int spriteIndex)
        {
            Sprite sprite = null;
            bool isGate = false;
            if (spriteIndex == 0)
                sprite = GetNotPlayableSprite(x, y);
            else if (spriteIndex == 1)
                sprite = GetPlayableSprite(x, y);
            else
                return;

            if (sprite == null)
                sprite = _gameFieldSprites.ErrorSprite;

            FieldCell newCell = Object.Instantiate(_fieldCellPrefab, _cellsContainer);

            Vector3 position = Vector3.zero;
            position.x = _gameFieldSettings.CellSize * x;
            position.y = _gameFieldSettings.CellSize * (-y);

            newCell.transform.localPosition = position;
            newCell.SetName(GenerateName(x, y));
            newCell.SetSprite(sprite);
        }

        private Sprite GetPlayableSprite(int x, int y)
        {
            int checkLeft = _levelMatrix[x - 1, y];
            int checkTop = _levelMatrix[x, y - 1];
            int checkRight = _levelMatrix[x + 1, y];
            int checkBottom = _levelMatrix[x, y + 1];

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

            switch (type)
            {
                case 7:
                case 15:
                case 23:
                case 39:
                case 135:
                case 143:
                case 167:
                case 175:
                    return _gameFieldSprites.NotPlayableSprites.TopLeftCorner;
                case 28:
                case 29:
                case 30:
                case 60:
                case 61:
                case 62:
                case 156:
                case 188:
                case 190:
                    return _gameFieldSprites.NotPlayableSprites.TopRightCorner;
                case 31:
                case 63:
                case 159:
                case 191:
                    return _gameFieldSprites.NotPlayableSprites.FullTopCorner;
                case 224:
                case 226:
                case 228:
                case 230:
                case 232:
                case 234:
                case 236:
                case 238:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable1;
                case 240:
                case 242:
                case 244:
                case 246:
                case 248:
                    //case 560:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable2;
                case 225:
                case 227:
                case 237:
                    // case 385:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable3;
                case 241:
                case 243:
                case 249:
                case 251:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable4;
                case 96:
                case 97:
                case 98:
                case 99:
                case 104:
                case 106:
                case 108:
                case 109:
                case 110:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable5;
                case 192:
                case 194:
                case 196:
                case 198:
                case 200:
                case 202:
                case 204:
                case 206:
                case 208:
                case 214:
                case 216:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable6;
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 74:
                case 75:
                case 76:
                case 77:
                case 80:
                case 81:
                case 84:
                case 86:
                case 88:
                case 92:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable7;
                case 193:
                case 195:
                case 197:
                case 201:
                case 203:
                case 209:
                case 211:
                case 217:
                case 219:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable8;
                case 112:
                case 113:
                case 114:
                case 115:
                case 116:
                case 120:
                case 121:
                case 122:
                case 123:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable9;
                case 124:
                case 125:
                case 127:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable10;
                case 199:
                case 207:
                case 223:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable11;
                case 231:
                case 239:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable12;
                case 252:
                case 254:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable13;
                case 255:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable14;
                case 247:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable15;
                case 253:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable16;
                case 95:
                    return _gameFieldSprites.NotPlayableSprites.NotPlayable17;
                case 0:
                    return _gameFieldSprites.NotPlayableSprites.Background;
            }

            return _gameFieldSprites.NotPlayableSprites.Background;
        }

        private string GenerateName(int x, int y)
        {
            return string.Format("FieldCell [{0};{1}]", x, y);
        }
    }
}