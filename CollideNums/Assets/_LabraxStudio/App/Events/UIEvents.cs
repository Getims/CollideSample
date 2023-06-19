using UnityEngine;
using UnityEngine.Events;

namespace LabraxStudio.Events
{
    public class UIEvents : MonoBehaviour
    {
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public static UnityEvent OnUISelect = new UnityEvent();
        public static UnityEvent OnUIDeselect = new UnityEvent();

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void SendUISelect() => OnUISelect?.Invoke();
        public static void SendUIDeselect() => OnUIDeselect?.Invoke();
    }
}