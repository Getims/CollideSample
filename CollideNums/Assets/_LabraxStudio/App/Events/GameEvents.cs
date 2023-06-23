using UnityEngine.Events;

namespace LabraxStudio.Events
{
    public class GameEvents
    {
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public static UnityEvent OnTileAction = new UnityEvent();
        public static UnityEvent OnGenerateLevel = new UnityEvent();
        public static UnityEvent<bool> OnGameOver = new UnityEvent<bool>();
        public static UnityEvent OnLevelRestartBoosterUse = new UnityEvent();

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void SendTileAction() => OnTileAction?.Invoke();
        public static void SendGameOver(bool isWin) =>  OnGameOver?.Invoke(isWin);
        public static void SendLevelGenerated() =>  OnGenerateLevel?.Invoke();
        public static void SendLevelRestartBoosterUse () =>  OnLevelRestartBoosterUse ?.Invoke();
    }
}