using LabraxStudio.Game;
using UnityEngine;
using UnityEngine.Events;

namespace LabraxStudio.Events
{
    public class UIEvents : MonoBehaviour
    {
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public static UnityEvent OnMainMenuTapToPlay = new UnityEvent();
        public static UnityEvent OnWinScreenClaimClicked = new UnityEvent();
        public static UnityEvent OnTaskWindowClosed = new UnityEvent();
        public static UnityEvent OnUISelect = new UnityEvent();
        public static UnityEvent OnUIDeselect = new UnityEvent();
        public static UnityEvent<BoosterType> OnNeedBoosterHand = new UnityEvent<BoosterType>();

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void SendUISelect() => OnUISelect?.Invoke();
        public static void SendUIDeselect() => OnUIDeselect?.Invoke();
        public static void SendMainMenuTapToPlay() => OnMainMenuTapToPlay?.Invoke();
        public static void SendWinScreenClaimClicked() => OnWinScreenClaimClicked?.Invoke();
        public static void SendNeedBoosterHand(BoosterType type) => OnNeedBoosterHand?.Invoke(type);
        public static void SendTaskWindowClosed() => OnTaskWindowClosed?.Invoke();
    }
}