using System.Collections.Generic;
using System.Linq;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game.Tiles;

namespace LabraxStudio.Game
{
    public class GameOverTrackerScreenSwipe : AGameOverTracker
    {
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void CheckForFail()
        {
            List<Tile> tiles = ServicesProvider.GameFlowService.TilesController.Tiles;
            
            if (HasTilesForGates(tiles))
            {
                CheckForFailRevert();
                return;
            }
            
            if (HasMerges(tiles))
            {
                CheckForFailRevert();
                return;
            }
            
            if (HasUncompletableTask(tiles))
            {
                Utils.InfoPoint("Not complete all tasks");
                _isFail = true;
                GameEvents.SendGameOver(false, FailReason.NotCompleteAllTasks);
                return;
            }

            if (!HasGateForEachTile(tiles))
            {
                Utils.InfoPoint("No gates");
                _isFail = true;
                GameEvents.SendGameOver(false, FailReason.NoGatesForTiles);
                return;
            }

            //CheckForFailRevert();
        }

        public override void CheckForWin()
        {
            if (_isWin)
                return;

            TasksController taskController = ServicesProvider.GameFlowService.TasksController;

            if (!taskController.HasTasks)
            {
                GameEvents.SendGameOver(true);
                return;
            }

            if (HasNotCompletedTasks(taskController))
            {
                Utils.InfoPoint("Not complete all tasks");
                _isFail = true;
                GameEvents.SendGameOver(false, FailReason.NotCompleteAllTasks);
                return;
            }

            _isWin = true;
            GameEvents.SendGameOver(true);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CheckForFailRevert()
        {
            if (_isFail)
            {
                _isFail = false;
                GameEvents.SendLevelCanBePassedAgain();
            }
        }

        private bool HasTiles()
        {
            List<Tile> tiles = ServicesProvider.GameFlowService.TilesController.Tiles;
            return tiles.Count > 0;
        }

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
                return true;

            Utils.InfoPoint("No merges");
            return false;
        }

        private bool HasTilesForGates(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                int tileValue = tile.Value;
                int tileGate = (int) GameTypesConverter.TileValueToGateType(tileValue);
                if (ServicesProvider.GameFlowService.GatesController.HasGate(tileGate))
                    return true;
            }

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

        private bool HasNotCompletedTasks(TasksController taskController)
        {
            if (!taskController.HasTasks)
                return false;

            return !taskController.IsAllTasksComplete;
        }

        private bool HasUncompletableTask(List<Tile> tiles)
        {
            TasksController taskController = ServicesProvider.GameFlowService.TasksController;

            if (!taskController.HasTasks)
                return false;

            List<int> tilesFromTasks = taskController.GetUncompleteTilesNumbers();
            if (tilesFromTasks.Count == 0)
                return false;

            foreach (var tileValue in tilesFromTasks)
            {
                if (tiles.Exists(t => t.Value == tileValue))
                    return false;
            }

            return true;
        }
    }
}