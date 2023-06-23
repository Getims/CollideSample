using LabraxStudio.Game;
using UnityEngine.Events;

namespace LabraxStudio.Events
{
    public class CommonEvents
    {
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public static UnityEvent AllCurrencyChanged = new UnityEvent();
        public static UnityEvent<CurrencyType> CurrencyChanged = new UnityEvent<CurrencyType>();
        public static UnityEvent<CurrencyType> OnNoCurrencyAnimationPlay = new UnityEvent<CurrencyType>();

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void SendAllCurrencyChanged() => AllCurrencyChanged?.Invoke();
        public static void SendCurrencyChanged(CurrencyType currencyType) => CurrencyChanged?.Invoke(currencyType);

        public static void PlayNoCurrencyAnimation(CurrencyType currencyType) =>
            OnNoCurrencyAnimationPlay?.Invoke(currencyType);
    }
}