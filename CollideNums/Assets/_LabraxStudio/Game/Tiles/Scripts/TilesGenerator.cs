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

        private void CreateTile(int x, int y, int spriteIndex)
        {
            if(spriteIndex==-1)
                return;
            
            Tile newCell = Object.Instantiate(_tilePrefab, _tilesContainer);

            Vector3 position = Vector3.zero;
            position.x = _gameFieldSettings.CellSize * x;
            position.y = _gameFieldSettings.CellSize * (-y);

            newCell.transform.localPosition = position;
            newCell.SetSprite(GetSprite(spriteIndex));
        }

        private Sprite GetSprite(int spriteIndex)
        {
            return _gameFieldSettings.GetTileSprite(spriteIndex);
        }
    }
}