using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game;
using LabraxStudio.Meta.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.GameScene.Boosters
{
    public class BoostersPanel : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private List<BoosterButton> _boosterButtons;

        [SerializeField]
        private HorizontalLayoutGroup _layoutGroup;

        [SerializeField]
        private ChooseTileText _chooseTileText;

        // FIELDS: -------------------------------------------------------------------

        private BoostersHandler _boostersHandler;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            GameEvents.OnGenerateLevel.AddListener(OnLevelGenerate);
            GameEvents.OnGameOver.AddListener(OnGameOver);
            CommonEvents.AllCurrencyChanged.AddListener(OnAllCurrencyChanged);
            GameEvents.OnTileAction.AddListener(OnTileAction);
            GameEvents.OnLevelCanBePassedAgain.AddListener(OnLevelCanBePassedAgain);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameEvents.OnGenerateLevel.RemoveListener(OnLevelGenerate);
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
            CommonEvents.AllCurrencyChanged.RemoveListener(OnAllCurrencyChanged);
            GameEvents.OnTileAction.RemoveListener(OnTileAction);
            GameEvents.OnLevelCanBePassedAgain.RemoveListener(OnLevelCanBePassedAgain);
            _boostersHandler?.OnDestroy();
        }

        private void Start()
        {
            if (GameFlowManager.IsLevelGenerated)
                OnLevelGenerate();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private bool SetupButtons()
        {
            LevelMeta levelMeta = ServicesProvider.LevelMetaService.GetCurrentLevelMeta();
            List<BoostersSettings> boostersSettings = levelMeta.BoostersSettings;
            int boostersCount = boostersSettings.Count;

            if (boostersCount == 0)
                return false;

            for (int i = 0; i < 5; i++)
            {
                if (i >= boostersCount)
                    break;

                BoostersSettings settings = boostersSettings[i];
                _boosterButtons[i].Initialize(settings.BoosterMeta, TryToUseBooster);
            }

            SetBoostersCount(boostersCount);
            UpdateLayout();
            return true;
        }

        private void SetBoostersCount(int count)
        {
            if (count > 5)
                count = 5;
            if (count < 0)
                count = 0;

            for (int i = 0; i < 5; i++)
                _boosterButtons[i].SetActive(i < count);
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

        private void CheckButtonsState()
        {
            for (int i = 0; i < 5; i++)
                _boosterButtons[i].CheckState();
        }

        private void TryToUseBooster(BoosterButton boosterButton) => _boostersHandler.OnBoosterClick(boosterButton);

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnGameOver(bool isWin)
        {
            if (isWin)
            {
                Hide();
                DestroySelfDelayed();
                return;
            }

            bool hasRestartBooster = false;
            foreach (var boosterButton in _boosterButtons)
            {
                if (boosterButton.BoosterMeta == null)
                    continue;

                if (boosterButton.BoosterMeta.BoosterType == BoosterType.LevelRestart)
                {
                    boosterButton.StartPulsation();
                    hasRestartBooster = true;
                    break;
                }
            }

            if (!hasRestartBooster)
            {
                Hide();
                DestroySelfDelayed();
            }
        }

        private void OnLevelGenerate()
        {
            _boostersHandler = new BoostersHandler(_chooseTileText, _targetCG);
            bool hasBoosters = SetupButtons();
            if (hasBoosters)
                Show();
            else
                Hide();
        }

        private void OnAllCurrencyChanged() => CheckButtonsState();
        private void OnTileAction() => CheckButtonsState();

        private void OnLevelCanBePassedAgain()
        {
            foreach (var boosterButton in _boosterButtons)
            {
                if (boosterButton.BoosterMeta == null)
                    continue;

                boosterButton.StopPulsation();
            }
        }
    }
}