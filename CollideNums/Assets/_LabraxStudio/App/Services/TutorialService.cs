using LabraxStudio.Game;
using LabraxStudio.Game.Tiles;
using LabraxStudio.UI.GameScene.Tutorial;
using UnityEngine;

namespace LabraxStudio.App.Services
{
    public class TutorialService
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        private bool IsInitialized => _tutorialController != null;

        // FIELDS: -------------------------------------------------------------------

        private TutorialController _tutorialController;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(TutorialController tutorialController)
        {
            _tutorialController = tutorialController;
        }

        public bool CanMoveTile(Vector2Int tile, Direction direction, Swipe swipe)
        {
            if (!IsInitialized)
                return true;

            SwipeTracker _swipeTracker = _tutorialController.GetSwipeTracker();
            if (_swipeTracker == null || _swipeTracker.IsComplete)
                return !_tutorialController.HasLockTracker();

            if (!_swipeTracker.IsCorrectSwipe(tile, direction, swipe))
                return false;

            return true;
        }

        public bool CanUseBooster(BoosterType boosterType)
        {
            if (!IsInitialized)
                return true;
            
            BoosterUseTracker boosterUseTracker = _tutorialController.GetBoosterUseTracker();

            if (boosterUseTracker == null || boosterUseTracker.IsComplete)
                return true;

            return boosterUseTracker.IsCorrectBooster(boosterType);
        }

        public bool CanUseBoosterOnTile(Tile tile)
        {
            if (!IsInitialized)
                return true;
            
            BoosterTargetTracker boosterTargetTracker = _tutorialController.GetBoosterTargetTracker();

            if (boosterTargetTracker == null || boosterTargetTracker.IsComplete)
                return true;

            return boosterTargetTracker.IsCorrectTarget(tile);
        }
    }
}