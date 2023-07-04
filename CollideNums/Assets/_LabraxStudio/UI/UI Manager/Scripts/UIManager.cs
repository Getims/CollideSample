using LabraxStudio.App.Services;
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
            UIEvents.OnTaskWindowClosed.AddListener(OnTaskWindowClosed);
            GameEvents.OnGameOver.AddListener(OnGameOver);
        }

        private void OnDestroy()
        {
            UIEvents.OnMainMenuTapToPlay.RemoveListener(OnMainMenuTapToPlay);
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
            UIEvents.OnWinScreenClaimClicked.RemoveListener(OnWinScreenClaimClicked);
            UIEvents.OnTaskWindowClosed.RemoveListener(OnTaskWindowClosed);
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void InitializeGameUI()
        {
            if (_levelIndexPanelGame == null)
                _levelIndexPanelGame = _gameUIFactory.Create<LevelIndexPanel>(MenuType.LevelIndexPanel);
            _gameUIFactory.Create(MenuType.BoostersPanel);

            int currentLevel = ServicesProvider.PlayerDataService.CurrentLevel;
            if (currentLevel > 1)
                _gameUIFactory.Create(MenuType.TasksPanel);
            _gameUIFactory.Create(MenuType.TutorialPanel);

            if (!_currencies)
                _gameUIFactory.Create(MenuType.CurrenciesBase, out _currencies);
        }

        public void InitializeMenuUI()
        {
            ServicesProvider.MusicService.PlayMainMenuMusic();

            _menuUIFactory.Create(MenuType.MainMenuOverlay);
            if (_levelIndexPanelMenu == null)
                _levelIndexPanelMenu = _menuUIFactory.Create<LevelIndexPanel>(MenuType.LevelIndexPanel);
            if (!_currencies)
                _menuUIFactory.Create(MenuType.CurrenciesBase, out _currencies);
        }

        public void InitializeDebugMenu()
        {
            bool enableDebug = ServicesProvider.GameSettingsService.GetGameSettings().LaunchSettings.EnableDebug;
            if (enableDebug)
                _menuUIFactory.Create(MenuType.DebugMenu);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnMainMenuTapToPlay()
        {
            int currentLevel = ServicesProvider.PlayerDataService.CurrentLevel;
            if (currentLevel > 1)
            {
                CreateTaskWindow();
                return;
            }

            ServicesProvider.MusicService.PlayGameMusic();

            if (_levelIndexPanelMenu != null)
            {
                _levelIndexPanelMenu.HideAndDestroy();
                _levelIndexPanelMenu = null;
            }

            InitializeGameUI();
        }

        private void CreateTaskWindow()
        {
            ServicesProvider.MusicService.PlayGameMusic();

            if (_levelIndexPanelMenu != null)
            {
                _levelIndexPanelMenu.HideAndDestroy();
                _levelIndexPanelMenu = null;
            }

            if (_levelIndexPanelGame == null)
                _levelIndexPanelGame = _gameUIFactory.Create<LevelIndexPanel>(MenuType.LevelIndexPanel);
            _gameUIFactory.Create(MenuType.TaskPopupWindow);
        }

        private void OnTaskWindowClosed() => InitializeGameUI();

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
            {
                ServicesProvider.MusicService.PlayWinScreenMusic();
                _menuUIFactory.Create(MenuType.WinScreen);
                if (_levelIndexPanelMenu == null)
                    _levelIndexPanelMenu = _menuUIFactory.Create<LevelIndexPanel>(MenuType.LevelIndexPanel);
            }
        }
    }
}