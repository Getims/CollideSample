using LabraxStudio.Managers;
using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class TilesController : SharedManager<TilesController>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private TilesGenerator _tilesGenerator = new TilesGenerator();

        // FIELDS: -------------------------------------------------------------------
        
        private TilesMover _tilesMover = new TilesMover();

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(LevelMeta levelMeta)
        {
            _tilesGenerator.Initialize();
            _tilesGenerator.GenerateTiles(levelMeta.Width, levelMeta.Height, levelMeta.TilesMatrix);
            _tilesMover.Initialize(levelMeta.LevelMatrix, levelMeta.TilesMatrix);
        }

        public void MoveTile(Tile tile, Direction direction, Swipe swipe) =>
            _tilesMover.MoveTile(tile, direction, swipe);
    }
}