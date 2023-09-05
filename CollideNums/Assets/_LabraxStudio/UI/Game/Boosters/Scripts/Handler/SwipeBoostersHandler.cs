using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Meta.Levels;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Boosters
{
    public class SwipeBoostersHandler : ABoostersHandler
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------
        public SwipeBoostersHandler(ChooseTileText chooseTileText, CanvasGroup panelCanvasGroup) : base(chooseTileText,
            panelCanvasGroup)
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

            if (!CanUseBooster(meta))
                return;

            ServicesProvider.GameFlowService.BoostersController.SetBoosterState(true);
            if (ServicesProvider.GameFlowService.GameOverTracker.IsFail)
                GameEvents.SendLevelCanBePassedAgain();
            LockPanel();

            UseBooster(meta);

            ServicesProvider.GameFlowService.BoostersController.SetBoosterState(false);
            UnlLockPanel();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private bool CanUseBooster(BoosterMeta boosterMeta)
        {
            TrackedTile trackedTile = ServicesProvider.GameFlowService.TilesController.GetTrackedTile();
            if (trackedTile == null)
                return false;

            Tile tile = trackedTile.Tile;
            if (tile == null)
                return false;

            bool canUseBooster = false;

            BoosterType boosterType = boosterMeta.BoosterType;
            switch (boosterType)
            {
                case BoosterType.Split:
                    canUseBooster = _splitBooster.SetTile(tile);
                    break;
                case BoosterType.Multiply:
                    canUseBooster = _multiplyBooster.SetTile(tile);
                    break;
                default:
                    canUseBooster = true;
                    break;
            }

            return canUseBooster;
        }

        protected override void OnTileClick(Tile tile)
        {
        }
    }
}