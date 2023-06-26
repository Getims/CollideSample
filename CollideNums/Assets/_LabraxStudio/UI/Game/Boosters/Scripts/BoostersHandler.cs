using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game;
using LabraxStudio.Meta.Levels;

namespace LabraxStudio.UI.GameScene.Boosters
{
    public class BoostersHandler
    {
        // FIELDS: -------------------------------------------------------------------

        private LevelRestartBooster _levelRestartBooster = new LevelRestartBooster();
        private SplitBooster _splitBooster = new SplitBooster();

        // PUBLIC METHODS: -----------------------------------------------------------------------
        public void UseBooster(BoosterMeta boosterMeta)
        {
            //if (meta.ReceiveForRv)
            //    WatchAd();
            int moneyCount = ServicesProvider.PlayerDataService.Money;
            bool isEnoughMoney = boosterMeta.MoneyPrice <= moneyCount;
            if (isEnoughMoney)
            {
                ServicesProvider.PlayerDataService.SpendMoney(boosterMeta.MoneyPrice);
                CommonEvents.SendAllCurrencyChanged();
                UseBooster(boosterMeta.BoosterType);
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void UseBooster(BoosterType boosterType)
        {
            switch (boosterType)
            {
                case BoosterType.LevelRestart:
                    _levelRestartBooster.UseBooster();
                    break;
                case BoosterType.Split:
                    _splitBooster.UseBooster();
                    break;
                default:
                    Utils.ReworkPoint("Not implemented booster");
                    break;
            }
        }
    }
}