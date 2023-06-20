using System;
using System.Collections.Generic;
using DG.Tweening;
using LabraxStudio.App;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
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

        [SerializeField]
        private TMP_Dropdown _dropdown;


        // FIELDS: -------------------------------------------------------------------

        private GameFieldSettings _gameFieldSettings;
        private List<string> _easeOptions;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            GameEvents.OnGenerateLevel.AddListener(OnLevelGenerated);
        }

        private void OnDestroy()
        {
            GameEvents.OnGenerateLevel.RemoveListener(OnLevelGenerated);
        }

        private void Start()
        {
            base.InitManager();
            _gameFieldSettings = ServicesAccess.GameSettingsService.GetGameSettings().GameFieldSettings;

            _baseSwipeForce.SetTextWithoutNotify(_gameFieldSettings.BaseSwipeForce.ToString());
            _tileSpeed.SetTextWithoutNotify(_gameFieldSettings.TileSpeed.ToString());
            _tileAcceleration.SetTextWithoutNotify(_gameFieldSettings.TileAcceleration.ToString());

            PrepareEaseOptions();
            PrepareLevelsDropDown();
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

        public void RestartLevel()
        {
            GameManager.ReloadScene();
        }

        public void NextLevel()
        {
            ServicesAccess.PlayerDataService.SwitchToNextLevel();
            GameManager.ReloadScene();
        }

        public void PreviousLevel()
        {
            ServicesAccess.PlayerDataService.SwitchToPreviousLevel();
            GameManager.ReloadScene();
        }

        public void OnDropDownChange(int value)
        {
            ServicesAccess.PlayerDataService.SetLevel(value);
            GameManager.ReloadScene();
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

        private void PrepareLevelsDropDown()
        {
            _dropdown.options.Clear();

            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            var selectableLevels = ServicesAccess.GameSettingsService.GetGlobalSettings().GameSettings.LevelsList;
            int currentIndex = ServicesAccess.PlayerDataService.CurrentLevel;
            if (selectableLevels == null)
                return;

            int i = 1;
            foreach (var levelMeta in selectableLevels)
            {
                _dropdown.options.Add(new TMP_Dropdown.OptionData($"{i}. {levelMeta.FileName}"));
                i++;
            }

            _dropdown.SetValueWithoutNotify(currentIndex);
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

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnLevelGenerated() => PrepareLevelsDropDown();
    }
}