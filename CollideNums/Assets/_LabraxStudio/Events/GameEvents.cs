using UnityEngine.Events;

namespace LabraxStudio.Events
{
    public class GameEvents
    {
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public static UnityEvent OnTileAction = new UnityEvent();

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void SendTileAction() => OnTileAction?.Invoke();
    }
}