using System;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
using LabraxStudio.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.GameScene.Boosters
{
    public class BoosterButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private BoosterButtonVisualizer _buttonVisualizer;

        [SerializeField]
        private Pulsation _pulsation;

        // PROPERTIES: ----------------------------------------------------------------------------

        public BoosterMeta BoosterMeta => _boosterMeta;

        // FIELDS: -------------------------------------------------------------------

        private BoosterMeta _boosterMeta;
        private Action<BoosterButton> _onClickAction;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(BoosterMeta boosterMeta, Action<BoosterButton> onClickAction)
        {
            _onClickAction = onClickAction;
            _boosterMeta = boosterMeta;
            _buttonVisualizer.SetState(boosterMeta.ReceiveForRv);
            _buttonVisualizer.SetCurrency(boosterMeta.MoneyPrice);
            _buttonVisualizer.SetIcon(boosterMeta.IconSprite);
            CheckAdState();
        }

        public void CheckAdState()
        {
            if (_boosterMeta == null)
                return;

            int moneyCount = ServicesProvider.PlayerDataService.Money;
            bool isEnoughMoney = _boosterMeta.MoneyPrice <= moneyCount;
            _buttonVisualizer.SetState(!isEnoughMoney);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void StartPulsation()
        {
            if (!_pulsation.IsPulsing)
                _pulsation.StartPulse();
        }

        public void StopPulsation() => _pulsation.StopPulse();

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void UseBooster()
        {
            if (_onClickAction != null)
                _onClickAction.Invoke(this);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnButtonClick()
        {
            StopPulsation();
            UseBooster();
        }
    }
}