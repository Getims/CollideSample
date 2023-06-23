using System;
using LabraxStudio.Events;
using LabraxStudio.Managers;
using LabraxStudio.UI.Common;
using LabraxStudio.UI.Common.Factory;
using UnityEngine;

namespace LabraxStudio.UI
{
    public class UIManager : SharedManager<UIManager>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private UIFactory _gameUIFactory;

        [SerializeField]
        private UIFactory _menuUIFactory;

        // FIELDS: -------------------------------------------------------------------

        private GameObject _currencies;
        private LevelIndexPanel _levelIndexPanelMenu;
        private LevelIndexPanel _levelIndexPanelGame;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            UIEvents.OnMainMenuTapToPlay.AddListener(OnMainMenuTapToPlay);
            UIEvents.OnWinScreenClaimClicked.AddListener(OnWinScreenClaimClicked);
            GameEvents.OnGameOver.AddListener(OnGameOver);
        }

        private void OnDestroy()
        {
            UIEvents.OnMainMenuTapToPlay.RemoveListener(OnMainMenuTapToPlay);
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
            UIEvents.OnWinScreenClaimClicked.RemoveListener(OnWinScreenClaimClicked);
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void InitializeGameUI()
        {
            _gameUIFactory.Create(MenuType.LevelIndexPanel);
            _gameUIFactory.Create(MenuType.BoostersPanel);

            if (!_currencies)
                _gameUIFactory.Create(MenuType.CurrenciesBase, out _currencies);
        }

        public void InitializeMenuUI()
        {
            _menuUIFactory.Create(MenuType.MainMenuOverlay);
            _levelIndexPanelMenu = _menuUIFactory.Create<LevelIndexPanel>(MenuType.LevelIndexPanel);
            if (!_currencies)
                _menuUIFactory.Create(MenuType.CurrenciesBase, out _currencies);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnMainMenuTapToPlay()
        {
            if (_levelIndexPanelMenu != null)
                _levelIndexPanelMenu.HideAndDestroy();

            InitializeGameUI();
        }


        private void OnWinScreenClaimClicked()
        {
            InitializeMenuUI();
        }

        private void OnGameOver(bool isWin)
        {
            /*
            if (isWin)
                InitializeMenuUI();
            */
            if (isWin)
                _menuUIFactory.Create(MenuType.WinScreen);
        }
    }
}