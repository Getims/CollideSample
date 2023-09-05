using LabraxStudio.App.Services;
using LabraxStudio.Game;
using LabraxStudio.Game.Tiles;

namespace LabraxStudio.UI.GameScene.Boosters
{
    public static class BoosterStateChecker
    {
        public static bool CanUseBoosterSwipe(BoosterType boosterType)
        {
            if (boosterType == BoosterType.LevelRestart)
                return true;

            TrackedTile trackedTile = ServicesProvider.GameFlowService.TilesController.GetTrackedTile();
            if (trackedTile == null)
                return false;

            Tile tile = trackedTile.Tile;
            if (tile == null)
                return false;

            bool canUseBooster = false;

            switch (boosterType)
            {
                case BoosterType.Split:
                    canUseBooster = tile.Value != 1;
                    break;
                case BoosterType.Multiply:
                    canUseBooster = tile.Value != 16;
                    break;
                default:
                    canUseBooster = true;
                    break;
            }

            return canUseBooster;
        }

        public static bool CanUseBoosterBase(BoosterType boosterType)
        {
            bool canUseBooster = false;
            switch (boosterType)
            {
                case BoosterType.Split:
                    canUseBooster = ServicesProvider.GameFlowService.TilesController.HasTilesExceptTile(1);
                    break;
                case BoosterType.Multiply:
                    canUseBooster = ServicesProvider.GameFlowService.TilesController.HasTilesExceptTile(16);
                    break;
                case BoosterType.LevelRestart:
                    canUseBooster = true;
                    break;
            }

            return canUseBooster;
        }
    }
}