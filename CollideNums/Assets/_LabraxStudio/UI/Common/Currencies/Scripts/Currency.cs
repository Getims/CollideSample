using System.Collections;
using LabraxStudio.UiAnimator;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace LabraxStudio.UI.Common.Currency
{
    public abstract class Currency : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField, Min(0)]
        private float _updateTime = 0.8f;

        [SerializeField, Required]
        private TextMeshProUGUI _valueTMP;

        [SerializeField, Required]
        protected UIAnimator _noValueAnimator;

        // FIELDS: --------------------------------------------------------------------------------

        protected float LastCurrency;
        private Coroutine _valueUpdaterCO;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_valueUpdaterCO == null)
                return;

            StopCoroutine(_valueUpdaterCO);
        }

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected abstract void UpdateCurrencyValue();

        protected void StartValueUpdater(float endValue) =>
            _valueUpdaterCO = StartCoroutine(ValueUpdaterCO(endValue));

        protected void StopValueUpdater()
        {
            if (_valueUpdaterCO == null)
                return;

            StopCoroutine(_valueUpdaterCO);
        }

        protected void UpdateValue(float value) =>
            _valueTMP.text = value.ToString("F0");

        // PRIVATE METHODS: -----------------------------------------------------------------------


        private IEnumerator ValueUpdaterCO(float endValue)
        {
            float startValue = LastCurrency;
            float elapsedTime = 0;

            while (elapsedTime < _updateTime)
            {
                elapsedTime += Time.deltaTime;
                LastCurrency = Mathf.Lerp(startValue, endValue, elapsedTime / _updateTime);
                _valueTMP.text = LastCurrency.ToString("F0");

                yield return null;
            }

            _valueTMP.text = endValue.ToString("F0");
            LastCurrency = endValue;
        }
    }
}