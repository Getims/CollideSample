using System;
using System.Collections.Generic;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Managers;
using LabraxStudio.Meta;
using TMPro;
using UnityEngine;

namespace LabraxStudio.Game.Debug
{
    public class DebugMenu : SharedManager<DebugMenu>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GameObject _menuPanel;

        [SerializeField]
        private TMP_InputField _baseSwipeForce;

        [SerializeField]
        private TMP_InputField _tileSpeed;

        [SerializeField]
        private TMP_InputField _tileAcceleration;

        [SerializeField]
        private TextMeshProUGUI _speedCounter;

        [SerializeField]
        private GameObject _speedCounterPanel;

        // FIELDS: -------------------------------------------------------------------

        private GameFieldSettings _gameFieldSettings;
        private List<string> _easeOptions;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        private void Start()
        {
            base.InitManager();
            _gameFieldSettings = ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSettings;

            _baseSwipeForce.SetTextWithoutNotify(_gameFieldSettings.BaseSwipeForce.ToString());
            _tileSpeed.SetTextWithoutNotify(_gameFieldSettings.TileSpeed.ToString());
            _tileAcceleration.SetTextWithoutNotify(_gameFieldSettings.TileAcceleration.ToString());

            PrepareEaseOptions();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void ApplySettings()
        {
            _gameFieldSettings.BaseSwipeForce = float.Parse(_baseSwipeForce.text);
            _gameFieldSettings.TileSpeed = float.Parse(_tileSpeed.text);

            float acceleration = float.Parse(_tileAcceleration.text);
            if (acceleration < 0)
                acceleration = 0.0f;
            _gameFieldSettings.TileAcceleration = acceleration;
            _tileAcceleration.SetTextWithoutNotify(_gameFieldSettings.TileAcceleration.ToString());
        }

        public void UpdateSpeed(float speed)
        {
            _speedCounter.text = string.Format("Speed: {0:F1} m/s", speed);
        }

        public void ShowMenu()
        {
            _menuPanel.SetActive(true);
        }

        public void HideMenu()
        {
            _menuPanel.SetActive(false);
        }

        public void SwitchSpeedCounter()
        {
            _speedCounterPanel.SetActive(!_speedCounter.IsActive());
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void PrepareEaseOptions()
        {
            _easeOptions = new List<string>()
            {
                "Linear", "InSine", "OutSine", "InOutSine", "InQuad",
                "OutQuad", "InOutQuad", "InCubic", "OutCubic", "InOutCubic", "InQuart", "OutQuart", "InOutQuart",
                "InQuint", "OutQuint", "InOutQuint", "InExpo", "OutExpo", "InOutExpo", "InCirc", "OutCirc", "InOutCirc",
                "InBack", "OutBack", "InOutBack", "InFlash", "OutFlash", "InOutFlash"
            };

            var optionsData = new List<TMP_Dropdown.OptionData>();
            foreach (var option in _easeOptions)
            {
                optionsData.Add(new TMP_Dropdown.OptionData(option));
            }

            //_shortMoveEase.options = optionsData;
        }

        private void SetOption(TMP_Dropdown dropdown, Ease option)
        {
            int index = _easeOptions.IndexOf(option.ToString());
            dropdown.SetValueWithoutNotify(index);
        }

        private Ease GetEase(TMP_Dropdown dropdown, Ease baseEase)
        {
            return ConvertToEase(_easeOptions[dropdown.value], baseEase);
        }

        private Ease ConvertToEase(string text, Ease baseEase)
        {
            var result = baseEase;

            try
            {
                result = (Ease) System.Enum.Parse(typeof(Ease), text);
            }
            catch (Exception e)
            {
            }

            return result;
        }
    }
}