using System;
using System.Collections.Generic;
using LabraxStudio.App.Services;
using LabraxStudio.Meta.GameField;
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

        private float _cellSize;
        private GameFieldSprites _gameFieldSprites;
        private bool _isInitialized = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
            var _gameFieldSettings = ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSettings;
            _cellSize = _gameFieldSettings.CellSize;
            _gameFieldSprites = ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSprites;
            _isInitialized = true;
        }

        public List<Tile> GenerateTiles(int levelWidth, int levelHeight, int[,] tilesMatrix)
        {
            if (!_isInitialized)
                return null;
            
            List<Tile> generatedTiles = new List<Tile>();

            for (int i = 0; i < levelWidth; i++)
            {
                for (int j = 0; j < levelHeight; j++)
                {
                    var tile = CreateTile(i, j, tilesMatrix[i, j]);
                    if (tile != null)
                        generatedTiles.Add(tile);
                }
            }

            return generatedTiles;
        }

        public Sprite GetSprite(int spriteIndex)
        {
            if (!_isInitialized)
                return null;
            
            return _gameFieldSprites.GetTileSprite(spriteIndex);
        } 
        
        public Sprite GetHighlightSprite(int spriteIndex)
        {
            if (!_isInitialized)
                return null;
            
            return _gameFieldSprites.GetTileHighlight(spriteIndex);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private Tile CreateTile(int x, int y, int matrixValue)
        {
            if (matrixValue == 0)
                return null;

            Tile newTile = Object.Instantiate(_tilePrefab, _tilesContainer);

            Vector2 matrixToPosition =
                GameTypesConverter.MatrixPositionToGamePosition(new Vector2(x, y), _cellSize);
            Vector3 position = Vector3.zero;
            position.x = matrixToPosition.x;
            position.y = matrixToPosition.y;
            newTile.transform.localPosition = position;

            int value = GameTypesConverter.MatrixValueToTile(matrixValue);
            newTile.Initialize("Tile " + value);
            newTile.SetCell(new Vector2Int(x, y));
            newTile.SetValue(matrixValue, GetSprite(matrixValue), GetHighlightSprite(matrixValue));

            return newTile;
        }
    }
}