using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game;

namespace LabraxStudio.UI.Common.Currency
{
    public class MoneyCurrency : Currency
    {
        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();

            CommonEvents.AllCurrencyChanged.AddListener(OnAllCurrencyChanged);
            CommonEvents.CurrencyChanged.AddListener(OnCurrencyChanged);
            CommonEvents.OnNoCurrencyAnimationPlay.AddListener(OnPlayNoCurrencyAnimation);
        }

        private void Start()
        {
            LastCurrency = ServicesProvider.PlayerDataService.Money;

            UpdateValue(LastCurrency);
            Show();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            CommonEvents.AllCurrencyChanged.RemoveListener(OnAllCurrencyChanged);
            CommonEvents.CurrencyChanged.RemoveListener(OnCurrencyChanged);
            CommonEvents.OnNoCurrencyAnimationPlay.RemoveListener(OnPlayNoCurrencyAnimation);

            StopValueUpdater();
            _noValueAnimator.Stop();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        protected override void UpdateCurrencyValue()
        {
            if (!gameObject.activeSelf)
                return;

            float endValue = ServicesProvider.PlayerDataService.Money;
            int difference = (int) (LastCurrency - endValue);

            StopValueUpdater();
            StartValueUpdater(endValue);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnAllCurrencyChanged() =>
            UpdateCurrencyValue();

        private void OnCurrencyChanged(CurrencyType currencyType)
        {
            if (currencyType != CurrencyType.Money)
                return;

            UpdateCurrencyValue();
        }

        private void OnPlayNoCurrencyAnimation(CurrencyType currencyType)
        {
            if (currencyType != CurrencyType.Money)
                return;

            _noValueAnimator.Stop();
            _noValueAnimator.Play();
        }
    }
}