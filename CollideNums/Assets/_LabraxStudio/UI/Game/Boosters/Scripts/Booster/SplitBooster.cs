using LabraxStudio.App.Services;
using LabraxStudio.Game.Tiles;

namespace LabraxStudio.UI.GameScene.Boosters
{
    public class SplitBooster : IBooster
    {
        // FIELDS: -------------------------------------------------------------------

        private Tile _tile;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public bool SetTile(Tile tile)
        {
            _tile = tile;

            if (_tile == null)
                return false;

            if (_tile.Value == 1)
                return false;

            return true;
        }

        public bool CanUseBooster()
        {
            return ServicesProvider.GameFlowService.TilesController.HasTilesExceptTile(1);
        }

        public void UseBooster()
        {
            ServicesProvider.GameFlowService.TilesController.ChangeTileValue(_tile, false);
        }
    }
}