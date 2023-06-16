using UnityEngine.Events;

namespace LabraxStudio.Events
{
    public class GameEvents
    {
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public static UnityEvent OnTileAction = new UnityEvent();
        public static UnityEvent OnLevelComplete = new UnityEvent();
        public static UnityEvent OnLevelFail = new UnityEvent();

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void SendTileAction() => OnTileAction?.Invoke();
        public static void SendLevelComplete() =>  OnLevelComplete?.Invoke();
        public static void SendLevelFail() =>  OnLevelFail?.Invoke();
    }
}