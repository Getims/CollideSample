using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Meta.Levels;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Boosters
{
    public abstract class ABoostersHandler
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public ABoostersHandler(ChooseTileText chooseTileText, CanvasGroup panelCanvasGroup)
        {
            _chooseTileText = chooseTileText;
            _panelCanvasGroup = panelCanvasGroup;
        }

        // FIELDS: -------------------------------------------------------------------

        protected ChooseTileText _chooseTileText;
        protected CanvasGroup _panelCanvasGroup;
        protected LevelRestartBooster _levelRestartBooster = new LevelRestartBooster();
        protected SplitBooster _splitBooster = new SplitBooster();
        protected MultiplyBooster _multiplyBooster = new MultiplyBooster();
        protected BoosterMeta _preUsedBooster;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void OnDestroy()
        {
            GameEvents.OnTileSelectForBooster?.RemoveListener(OnTileClick);
        }

        public abstract void OnBoosterClick(BoosterButton boosterButton);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        protected abstract void OnTileClick(Tile tile);

        protected bool HasEnoughMoney(int boosterPrice)
        {
            int moneyCount = ServicesProvider.PlayerDataService.Money;
            return boosterPrice <= moneyCount;
        }

        protected void UseBooster(BoosterMeta boosterMeta)
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

        protected void LockPanel() => _panelCanvasGroup.blocksRaycasts = false;
        protected void UnlLockPanel() => _panelCanvasGroup.blocksRaycasts = true;

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
    }
}