using System.Collections.Generic;
using System.Linq;
using _LabraxStudio.Game.GameFlow.Scripts;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game.Tiles;

namespace LabraxStudio.Game
{
    public class GameOverTracker
    {
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void CheckForFail()
        {
            List<Tile> tiles = ServicesProvider.GameFlowService.TilesController.Tiles;

            if (HasTileOverflow(tiles))
            {
                Utils.ReworkPoint("Overflow");
                GameEvents.SendGameOver(false);
                return;
            }

            if (HasMerges(tiles))
                return;

            if (!HasGateForEachTile(tiles))
            {
                Utils.ReworkPoint("No gates");
                GameEvents.SendGameOver(false);
                return;
            }
        }

        public void CheckForWin()
        {
            TasksController taskController = ServicesProvider.GameFlowService.TasksController;

            if (!taskController.HasTasks)
            {
                GameEvents.SendGameOver(true);
                return;
            }

            if (HasNotCompletedTasks(taskController))
            {
                Utils.ReworkPoint("Not complete all tasks");
                GameEvents.SendGameOver(false);
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private bool HasTileOverflow(List<Tile> tiles)
        {
            int _biggestGateNumber = ServicesProvider.GameFlowService.GatesController.GetBiggestGateNumber();

            foreach (var tile in tiles)
            {
                int tileValue = tile.Value;
                int tileGate = (int) GameTypesConverter.TileValueToGateType(tileValue);
                if (tileGate > _biggestGateNumber)
                    return true;
            }

            return false;
        }

        private bool HasMerges(List<Tile> tiles)
        {
            int[] arr = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10};
            var duplicates = tiles.GroupBy(x => x.Value)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key);

            if (duplicates.Count() > 0)
            {
                Utils.ReworkPoint("Has merges");
                return true;
            }

            Utils.ReworkPoint("No merges");
            return false;
        }

        private bool HasGateForEachTile(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                int tileValue = tile.Value;
                int tileGate = (int) GameTypesConverter.TileValueToGateType(tileValue);
                if (!ServicesProvider.GameFlowService.GatesController.HasGate(tileGate))
                    return false;
            }

            return true;
        }
        
        private bool HasNotCompletedTasks( TasksController taskController)
        {
            if (!taskController.HasTasks)
                return false;

            return !taskController.IsAllTasksComplete;
        }
    }
}