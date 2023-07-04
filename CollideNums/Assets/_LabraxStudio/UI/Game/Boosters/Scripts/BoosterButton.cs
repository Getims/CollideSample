using System;
using LabraxStudio.App.Services;
using LabraxStudio.Game;
using LabraxStudio.Meta.Levels;
using LabraxStudio.UI.Common;
using LabraxStudio.UI.GameScene.Tutorial;
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

        [SerializeField]
        private Image _clickBlocker;

        [SerializeField] 
        private BoosterTutorialHand _tutorialHand;

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
            _tutorialHand.OnDestroy();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(BoosterMeta boosterMeta, Action<BoosterButton> onClickAction)
        {
            _onClickAction = onClickAction;
            _boosterMeta = boosterMeta;
            _buttonVisualizer.SetState(boosterMeta.BoosterCost);
            _buttonVisualizer.SetCurrency(boosterMeta.MoneyPrice);
            _buttonVisualizer.SetIcon(boosterMeta.IconSprite);
            _tutorialHand.Initialize(boosterMeta.BoosterType);
            CheckState();
        }

        public void CheckState()
        {
            if (_boosterMeta == null)
                return;

            BoosterType boosterType = _boosterMeta.BoosterType;
            bool canUseBooster = false;
            switch (boosterType)
            {
                case BoosterType.Split:
                    canUseBooster = ServicesProvider.GameFlowService.TilesController.HasTilesExceptTile(1);
                    break;
                case BoosterType.Multiply:
                    canUseBooster = ServicesProvider.GameFlowService.TilesController.HasTilesExceptTile(16);
                    break;
                case BoosterType.LevelRestart:
                    canUseBooster = true;
                    break;
            }

            _clickBlocker.enabled = !canUseBooster;
            _buttonVisualizer.SetInteractable(canUseBooster);
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
            bool canUseBooster = ServicesProvider.TutorialService.CanUseBooster(_boosterMeta.BoosterType);
            if(!canUseBooster)
                return;

            _tutorialHand.OnBoosterClick();
            StopPulsation();
            UseBooster();
        }
    }
}