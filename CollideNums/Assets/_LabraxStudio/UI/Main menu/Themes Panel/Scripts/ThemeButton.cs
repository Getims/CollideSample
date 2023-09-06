using System;
using LabraxStudio.Meta.GameField;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.MainMenu.Themes
{
    public class ThemeButton : MonoBehaviour
    {
        [SerializeField]
        private Image _iconIMG;

        [SerializeField]
        private Image _iconIMGActive;

        [SerializeField]
        private GameObject _activeContainer;

        // FIELDS: -------------------------------------------------------------------

        private bool _isSelected = false;
        private GameTheme _theme;
        private Action<GameTheme> _onClick;

        public void Initialize(GameTheme theme, bool isSelected, Action<GameTheme> tryToSetTheme)
        {
            _theme = theme;
            _isSelected = isSelected;
            SetIcon(_theme.ThemeIcon);
            SetState(isSelected);
            _onClick = tryToSetTheme;
            gameObject.SetActive(true);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetState(bool isSelected)
        {
            _activeContainer.SetActive(isSelected);
        }

        private void SetIcon(Sprite themeIcon)
        {
            _iconIMG.sprite = themeIcon;
            _iconIMGActive.sprite = themeIcon;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------
        public void OnClick()
        {
            if (_isSelected)
                return;

            _onClick.Invoke(_theme);
        }
    }
}