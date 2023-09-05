using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Meta.Levels;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Boosters
{
    public class DefaultBoostersHandler : ABoostersHandler
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public DefaultBoostersHandler(ChooseTileText chooseTileText, CanvasGroup panelCanvasGroup) : base(
            chooseTileText, panelCanvasGroup)
        {
            _chooseTileText = chooseTileText;
            _panelCanvasGroup = panelCanvasGroup;
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void OnBoosterClick(BoosterButton boosterButton)
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

            if (ServicesProvider.GameFlowService.GameOverTracker.IsFail)
                GameEvents.SendLevelCanBePassedAgain();

            _preUsedBooster = boosterMeta;
            _chooseTileText.Show();
            LockPanel();
        }
        
        protected override void OnTileClick(Tile tile)
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
        
    }
}