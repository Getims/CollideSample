using System;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Meta.Levels;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Boosters
{
    public class BoostersHandler
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public BoostersHandler(ChooseTileText chooseTileText, CanvasGroup panelCanvasGroup)
        {
            _chooseTileText = chooseTileText;
            _panelCanvasGroup = panelCanvasGroup;
        }

        // FIELDS: -------------------------------------------------------------------

        private readonly ChooseTileText _chooseTileText;
        private readonly CanvasGroup _panelCanvasGroup;
        private LevelRestartBooster _levelRestartBooster = new LevelRestartBooster();
        private SplitBooster _splitBooster = new SplitBooster();
        private MultiplyBooster _multiplyBooster = new MultiplyBooster();
        private BoosterMeta _preUsedBooster;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void OnDestroy()
        {
            GameEvents.OnTileSelectForBooster?.RemoveListener(OnTileClick);
        }

        public void OnBoosterClick(BoosterButton boosterButton)
        {
            BoosterMeta meta = boosterButton.BoosterMeta;
            if (meta == null)
            {
                Debug.LogError("No boosters meta");
                return;
            }

            if (meta.BoosterCost == BoosterCost.Money)
            {
                if (!HasEnoughMoney(meta.MoneyPrice))
                {
                    CommonEvents.PlayNoCurrencyAnimation(CurrencyType.Money);
                    return;
                }
            }

            BoosterType boosterType = meta.BoosterType;
            if (boosterType == BoosterType.LevelRestart)
                UseBooster(meta);
            else
                PreUseBooster(meta);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void PreUseBooster(BoosterMeta boosterMeta)
        {
            bool canUseBooster = false;
            BoosterType boosterType = boosterMeta.BoosterType;
            switch (boosterType)
            {
                case BoosterType.Split:
                    canUseBooster = _splitBooster.CanUseBooster();
                    break;
                case BoosterType.Multiply:
                    canUseBooster = _multiplyBooster.CanUseBooster();
                    break;
            }

            if (!canUseBooster)
                return;

            ServicesProvider.GameFlowService.BoostersController.SetBoosterState(true);
            GameEvents.OnTileSelectForBooster.AddListener(OnTileClick);

            _preUsedBooster = boosterMeta;
            _chooseTileText.Show();
            LockPanel();
        }

        private void UseBooster(BoosterMeta boosterMeta)
        {
            switch (boosterMeta.BoosterCost)
            {
                case BoosterCost.Free:
                    UseBooster(boosterMeta.BoosterType);
                    break;
                case BoosterCost.Money:
                    ServicesProvider.PlayerDataService.SpendMoney(boosterMeta.MoneyPrice);
                    CommonEvents.SendAllCurrencyChanged();
                    TrackBoosterBuy(boosterMeta.BoosterType);
                    UseBooster(boosterMeta.BoosterType);
                    break;
                case BoosterCost.RV:
                    Debug.LogWarning("RV not implemented");
                    break;
            }
        }

        private void UseBooster(BoosterType boosterType)
        {
            TrackBoosterUse(boosterType);

            switch (boosterType)
            {
                case BoosterType.LevelRestart:
                    _levelRestartBooster.UseBooster();
                    break;
                case BoosterType.Split:
                    _splitBooster.UseBooster();
                    break;
                case BoosterType.Multiply:
                    _multiplyBooster.UseBooster();
                    break;
                default:
                    Utils.ReworkPoint("Not implemented booster");
                    break;
            }
        }

        private void OnTileClick(Tile tile)
        {
            bool isCorrectTile = false;
            BoosterType boosterType = _preUsedBooster.BoosterType;
            switch (boosterType)
            {
                case BoosterType.Split:
                    isCorrectTile = _splitBooster.SetTile(tile);
                    break;
                case BoosterType.Multiply:
                    isCorrectTile = _multiplyBooster.SetTile(tile);
                    break;
                default:
                    Utils.ReworkPoint("Not implemented booster");
                    break;
            }

            if (!isCorrectTile)
                return;

            _chooseTileText.Hide();
            UnlLockPanel();

            GameEvents.OnTileSelectForBooster.RemoveListener(OnTileClick);
            ServicesProvider.GameFlowService.BoostersController.SetBoosterState(false);
            UseBooster(_preUsedBooster);
        }

        private bool HasEnoughMoney(int boosterPrice)
        {
            int moneyCount = ServicesProvider.PlayerDataService.Money;
            return boosterPrice <= moneyCount;
        }

        private void TrackBoosterUse(BoosterType boosterType)
        {
            int currentLevel = ServicesProvider.PlayerDataService.CurrentLevel + 1;

            if (boosterType != BoosterType.LevelRestart)
                ServicesProvider.AnalyticsService.EventsCore.TrackBoosterUse(currentLevel, boosterType.ToString());
            else
            {
                if (ServicesProvider.GameFlowService.GameOverTracker.IsFail)
                    ServicesProvider.AnalyticsService.EventsCore.TrackLevelFail(currentLevel);
                else
                    ServicesProvider.AnalyticsService.EventsCore.TrackBoosterUse(currentLevel, boosterType.ToString());
            }
        }

        private void TrackBoosterBuy(BoosterType boosterType)
        {
            int currentLevel = ServicesProvider.PlayerDataService.CurrentLevel + 1;
            ServicesProvider.AnalyticsService.EventsCore.TrackBoosterBuy(currentLevel, boosterType.ToString());
        }

        private void LockPanel() => _panelCanvasGroup.blocksRaycasts = false;
        private void UnlLockPanel() => _panelCanvasGroup.blocksRaycasts = true;
    }
}