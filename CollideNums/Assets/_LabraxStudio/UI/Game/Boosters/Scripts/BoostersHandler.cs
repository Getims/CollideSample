using System;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game;
using LabraxStudio.Meta.Levels;
using UnityEngine;

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
            switch (boosterMeta.BoosterCost)
            {
                case BoosterCost.Free:

                    UseBooster(boosterMeta.BoosterType);
                    break;
                case BoosterCost.Money:
                    int moneyCount = ServicesProvider.PlayerDataService.Money;
                    bool isEnoughMoney = boosterMeta.MoneyPrice <= moneyCount;
                    if (isEnoughMoney)
                    {
                        ServicesProvider.PlayerDataService.SpendMoney(boosterMeta.MoneyPrice);
                        CommonEvents.SendAllCurrencyChanged();
                        UseBooster(boosterMeta.BoosterType);
                    }
                    else
                        CommonEvents.PlayNoCurrencyAnimation(CurrencyType.Money);

                    break;
                case BoosterCost.RV:
                    Debug.LogWarning("RV not implemented");
                    break;
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