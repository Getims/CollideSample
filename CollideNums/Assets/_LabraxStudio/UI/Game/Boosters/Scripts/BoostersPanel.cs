using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Meta;
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

        // FIELDS: -------------------------------------------------------------------

        private BoostersHandler _boostersHandler = new BoostersHandler();

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            GameEvents.OnGenerateLevel.AddListener(OnLevelGenerate);
            GameEvents.OnGameOver.AddListener(OnGameOver);
            CommonEvents.AllCurrencyChanged.AddListener(OnAllCurrencyChanged);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            CancelInvoke(nameof(DestroySelf));
            GameEvents.OnGenerateLevel.RemoveListener(OnLevelGenerate);
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
            CommonEvents.AllCurrencyChanged.RemoveListener(OnAllCurrencyChanged);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private bool SetupButtons()
        {
            LevelMeta levelMeta = ServicesAccess.LevelMetaService.GetCurrentLevelMeta();
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
                await Task.Delay(30);
                _layoutGroup.enabled = true;
                await Task.Delay(30);
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
                _boosterButtons[i].CheckAdState();
        }

        private void TryToUseBooster(BoosterButton boosterButton)
        {
            BoosterMeta meta = boosterButton.BoosterMeta;
            if (meta == null)
            {
                Debug.LogError("No boosters meta");
                return;
            }

            _boostersHandler.UseBooster(meta);
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnGameOver(bool arg0)
        {
            Hide();
            Invoke(nameof(DestroySelf), FadeTime);
        }

        private void OnLevelGenerate()
        {
            bool hasBoosters = SetupButtons();
            if (hasBoosters)
                Show();
            else
                Hide();
        }

        private void OnAllCurrencyChanged() => CheckButtonsState();
    }
}