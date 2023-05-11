using System;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LabraxStudio.Game.Tiles
{
    [Serializable]
    public class TilesGenerator
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField]
        private Transform _tilesContainer;

        [SerializeField]
        private Tile _tilePrefab;

        // FIELDS: -------------------------------------------------------------------

        private GameFieldSettings _gameFieldSettings;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            _gameFieldSettings = ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSettings;
        }

        public void GenerateTiles(int levelWidth, int levelHeight, int[,] tilesMatrix)
        {
            for (int i = 0; i < levelWidth; i++)
            {
                for (int j = 0; j < levelHeight; j++)
                {
                    CreateTile(i, j, tilesMatrix[i, j] - 1);
                }
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CreateTile(int x, int y, int matrixValue)
        {
            if (matrixValue == -1)
                return;

            Tile newTile = Object.Instantiate(_tilePrefab, _tilesContainer);

            Vector2 matrixToPosition =
                GameTypesConverter.MatrixPositionToGamePosition(new Vector2(x, y), _gameFieldSettings.CellSize);
            Vector3 position = Vector3.zero;
            position.x = matrixToPosition.x;
            position.y = matrixToPosition.y; 
            newTile.transform.localPosition = position;
            
            int value = GameTypesConverter.MatrixValueToTile(matrixValue);
            newTile.Initialize("Tile "+ value, GetSprite(matrixValue));
            newTile.SetCell(new Vector2Int(x, y));
            newTile.SetValue(matrixValue);
        }

        private Sprite GetSprite(int spriteIndex)
        {
            return _gameFieldSettings.GetTileSprite(spriteIndex);
        }
    }
}