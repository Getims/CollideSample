using System;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
using TMPro;
using UnityEngine;

namespace LabraxStudio.Game.Debug
{
    public class DebugMenu : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private TMP_InputField _swipeShortMin;

        [SerializeField]
        private TMP_InputField _swipeShortMax;

        [SerializeField]
        private TMP_InputField _swipeLongMin;

        [SerializeField]
        private TMP_InputField _swipeLongMax;

        [SerializeField]
        private TMP_InputField _shortMoveTime;

        [SerializeField]
        private TMP_InputField _moveSlowing;

        [SerializeField]
        private TMP_InputField _moveEase;

        // FIELDS: -------------------------------------------------------------------
        private GameFieldSettings _gameFieldSettings;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        private void Start()
        {
            _gameFieldSettings = ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSettings;

            _swipeShortMin.SetTextWithoutNotify(_gameFieldSettings.ShortSwipeDelta.x.ToString());
            _swipeShortMax.SetTextWithoutNotify(_gameFieldSettings.ShortSwipeDelta.y.ToString());

            _swipeLongMin.SetTextWithoutNotify(_gameFieldSettings.LongSwipeDelta.x.ToString());
            _swipeLongMax.SetTextWithoutNotify(_gameFieldSettings.LongSwipeDelta.y.ToString());

            _shortMoveTime.SetTextWithoutNotify(_gameFieldSettings.OneCellTime.ToString());
            _moveSlowing.SetTextWithoutNotify(_gameFieldSettings.MoveSlowing.ToString());
            _moveEase.SetTextWithoutNotify(_gameFieldSettings.MoveEase.ToString());
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void ApplySwipes()
        {
            _gameFieldSettings.ShortSwipeDelta = new Vector2(
                float.Parse(_swipeShortMin.text),
                float.Parse(_swipeShortMax.text)
            );

            _gameFieldSettings.LongSwipeDelta = new Vector2(
                float.Parse(_swipeLongMin.text),
                float.Parse(_swipeLongMax.text)
            );
        }

        public void ApplyAnimation()
        {
            _gameFieldSettings.OneCellTime = float.Parse(_shortMoveTime.text);
            _gameFieldSettings.MoveSlowing = float.Parse(_moveSlowing.text);
            _gameFieldSettings.MoveEase = ConvertToEase(_moveEase.text, _gameFieldSettings.MoveEase);
            _moveEase.SetTextWithoutNotify(_gameFieldSettings.MoveEase.ToString());
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
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