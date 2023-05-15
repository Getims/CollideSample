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

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            _gameFieldSettings = ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSettings;
            _gameFieldSprites = ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSprites;
        }

        public GameCellType[,] GenerateGameField(int levelWidth, int levelHeight, int[,] levelMatrix)
        {
            GameCellType[,] gamefield = new GameCellType[levelWidth, levelHeight];

            for (int i = 0; i < levelWidth; i++)
            {
                for (int j = 0; j < levelHeight; j++)
                {
                    gamefield[i, j] = GameTypesConverter.MatrixValueToCellType(levelMatrix[i, j]);
                }
            }

            return gamefield;
        }

        public void GenerateFieldSprites(int levelWidth, int levelHeight, int[,] levelMatrix)
        {
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
            FieldCell newCell = Object.Instantiate(_fieldCellPrefab, _cellsContainer);

            Vector3 position = Vector3.zero;
            position.x = _gameFieldSettings.CellSize * x;
            position.y = _gameFieldSettings.CellSize * (-y);

            newCell.transform.localPosition = position;
            newCell.SetName(GenerateName(x, y));
            newCell.SetSprite(GetSprite(spriteIndex));
        }

        private Sprite GetSprite(int spriteIndex)
        {
            return _gameFieldSprites.GetFieldSprite(spriteIndex);
        }

        private string GenerateName(int x, int y)
        {
            return string.Format("FieldCell [{0};{1}]", x, y);
        }
    }
}