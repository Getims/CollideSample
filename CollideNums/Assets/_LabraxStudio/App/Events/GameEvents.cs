using LabraxStudio.Game;
using LabraxStudio.Game.Tiles;
using UnityEngine.Events;

namespace LabraxStudio.Events
{
    public class GameEvents
    {
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public static UnityEvent OnTileAction = new UnityEvent();
        public static UnityEvent<MoveAction> OnPreMoveTile = new UnityEvent<MoveAction>();
        public static UnityEvent<MergeAction> OnPreMergeTile = new UnityEvent<MergeAction>();
        public static UnityEvent<Tile> OnTileSelectForBooster = new UnityEvent<Tile>();
        public static UnityEvent OnGenerateLevel = new UnityEvent();
        public static UnityEvent<bool> OnGameOver = new UnityEvent<bool>();
        public static UnityEvent<FailReason> OnGameFail = new UnityEvent<FailReason>();
        public static UnityEvent OnLevelRestartBoosterUse = new UnityEvent();
        public static UnityEvent<int> OnMoveTileInGate = new UnityEvent<int>();
        public static UnityEvent<int> OnLevelTaskProgress = new UnityEvent<int>();
        public static UnityEvent<int> OnLevelTaskComplete = new UnityEvent<int>();
        public static UnityEvent OnAllTasksComplete = new UnityEvent();
        public static UnityEvent OnTileMergesComplete = new UnityEvent();
        public static UnityEvent OnLevelCanBePassedAgain = new UnityEvent();
        public static UnityEvent<TrackedTile> OnTrackedTileUpdate = new UnityEvent<TrackedTile>();

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void SendTileAction() => OnTileAction?.Invoke();
        public static void SendTileMergesComplete() => OnTileMergesComplete?.Invoke();
        public static void SendMoveTileInGate(int tileNumber) => OnMoveTileInGate?.Invoke(tileNumber);
        public static void SendPreMoveTile(MoveAction moveAction) => OnPreMoveTile?.Invoke(moveAction);
        public static void SendPreMergeTile(MergeAction mergeAction) => OnPreMergeTile?.Invoke(mergeAction);
        public static void SendTileSelectForBooster(Tile tile) => OnTileSelectForBooster?.Invoke(tile);

        public static void SendGameOver(bool isWin, FailReason failReason = FailReason.None)
        {
            OnGameOver?.Invoke(isWin);
            if (!isWin)
                OnGameFail?.Invoke(failReason);
        }

        public static void SendLevelGenerated() => OnGenerateLevel?.Invoke();
        public static void SendLevelRestartBoosterUse() => OnLevelRestartBoosterUse?.Invoke();
        public static void SendLevelTaskProgress(int tileNumber) => OnLevelTaskProgress?.Invoke(tileNumber);
        public static void SendLevelTaskComplete(int tileNumber) => OnLevelTaskComplete?.Invoke(tileNumber);
        public static void SendAllTasksComplete() => OnAllTasksComplete?.Invoke();
        public static void SendLevelCanBePassedAgain() => OnLevelCanBePassedAgain?.Invoke();
        public static void SendTrackedTileUpdate(TrackedTile tile) => OnTrackedTileUpdate?.Invoke(tile);
    }
}