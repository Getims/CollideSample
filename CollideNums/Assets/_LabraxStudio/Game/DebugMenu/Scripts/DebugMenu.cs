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
        private TMP_InputField _minSwipeSpeed;

        [SerializeField]
        private TMP_InputField _oneTileSpeed;

        [SerializeField]
        private TMP_InputField _oneTileMoveTime;

        [SerializeField]
        private TMP_InputField _moveSlowing;

        [SerializeField]
        private TMP_Dropdown _shortMoveEase;

        [SerializeField]
        private TMP_Dropdown _longMoveEase;

        [SerializeField]
        private TextMeshProUGUI _speedCounter;

        // FIELDS: -------------------------------------------------------------------

        private GameFieldSettings _gameFieldSettings;
        private List<string> _easeOptions;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        private void Start()
        {
            base.InitManager();
            _gameFieldSettings = ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSettings;

            _minSwipeSpeed.SetTextWithoutNotify(_gameFieldSettings.MinSwipeSpeed.ToString());
            _oneTileSpeed.SetTextWithoutNotify(_gameFieldSettings.OneTileSpeed.ToString());

            _oneTileMoveTime.SetTextWithoutNotify(_gameFieldSettings.OneTileMoveTime.ToString());
            _moveSlowing.SetTextWithoutNotify((1-_gameFieldSettings.MoveSlowing).ToString());

            PrepareEaseOptions();
            SetOption(_shortMoveEase, _gameFieldSettings.ShortMoveEase);
            SetOption(_longMoveEase, _gameFieldSettings.LongMoveEase);
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void ApplySwipes()
        {
            _gameFieldSettings.MinSwipeSpeed = float.Parse(_minSwipeSpeed.text);
            _gameFieldSettings.OneTileSpeed = float.Parse(_oneTileSpeed.text);
        }

        public void ApplyAnimation()
        {
            _gameFieldSettings.OneTileMoveTime = float.Parse(_oneTileMoveTime.text);

            float moveSlowing = float.Parse(_moveSlowing.text);
            if (moveSlowing < 0)
                moveSlowing = 0.0f;
            if (moveSlowing > 1)
                moveSlowing = 1.0f;
            _gameFieldSettings.MoveSlowing = 1-moveSlowing;
            _moveSlowing.SetTextWithoutNotify((1-_gameFieldSettings.MoveSlowing).ToString());

            _gameFieldSettings.ShortMoveEase = GetEase(_shortMoveEase, _gameFieldSettings.ShortMoveEase);
            SetOption(_shortMoveEase, _gameFieldSettings.ShortMoveEase);

            _gameFieldSettings.LongMoveEase = GetEase(_longMoveEase, _gameFieldSettings.LongMoveEase);
            SetOption(_longMoveEase, _gameFieldSettings.LongMoveEase);
        }

        public void UpdateSpeed(float speed)
        {
            _speedCounter.text = string.Format("Speed: {0:F1} m/s", speed);
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

            _shortMoveEase.options = optionsData;
            _longMoveEase.options = optionsData;
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