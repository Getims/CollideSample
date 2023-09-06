using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabraxStudio.App;
using LabraxStudio.App.Services;
using LabraxStudio.Game;
using LabraxStudio.Meta.GameField;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.MainMenu.Themes
{
    public class ThemesPanel : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private List<ThemeButton> _themeButtons;

        [SerializeField]
        private HorizontalLayoutGroup _layoutGroup;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start()
        {
            SetupButtons();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetupButtons()
        {
            List<GameTheme> gameThemes = ServicesProvider.GameSettingsService.GetGameSettings().GameThemes;
            GameTheme currentGameTheme = ServicesProvider.GameSettingsService.SelectedGameTheme;

            int themesCount = gameThemes.Count;
            if (themesCount == 1)
            {
                Hide(true);
                return;
            }

            SwipeMode swipeMode = ServicesProvider.GameSettingsService.GetGameSettings().SwipeSettings.SwipeMode;
            int i = 0;
            foreach (var theme in gameThemes)
            {
                _themeButtons[i].Initialize(theme, theme.ThemeId == currentGameTheme.ThemeId, TryToSetTheme);
                i++;
            }

            UpdateLayout();
        }

        private async void UpdateLayout()
        {
            try
            {
                _layoutGroup.enabled = false;
                await Task.Delay(20);
                _layoutGroup.enabled = true;
                await Task.Delay(20);
                _layoutGroup.enabled = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void TryToSetTheme(GameTheme gameTheme)
        {
            if (gameTheme == null)
                return;

            ServicesProvider.PlayerDataService.SetGameThemeID(gameTheme.ThemeId);
            GameDirector.Instance.Restart();
            GameManager.ReloadScene();
        }
    }
}