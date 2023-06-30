using System;
using LabraxStudio.Game;
using LabraxStudio.Meta.Tutorial;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    public class SwipeTracker : ARuleTracker
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public SwipeTracker(Vector2Int tilePosition, Direction swipeDirection, Swipe swipeType,
            TutorialHand tutorialHand, Action checkRules)
        {
            _type = RuleType.TileSwipe;
            _tilePosition = tilePosition;
            _swipeDirection = swipeDirection;
            _swipeType = swipeType;
            _tutorialHand = tutorialHand;
            _checkRules = checkRules;

            _tutorialHand.PlaySwipeAnimation(tilePosition, swipeDirection, swipeType);
        }

        // FIELDS: -------------------------------------------------------------------

        private readonly Vector2Int _tilePosition;
        private readonly Direction _swipeDirection;
        private readonly Swipe _swipeType;
        private readonly TutorialHand _tutorialHand;
        private readonly Action _checkRules;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void SetComplete()
        {
            _isComplete = true;
            _tutorialHand.StopSwipeAnimation();
            _checkRules?.Invoke();
        }

        public override void OnDestroy()
        {
        }

        public bool IsCorrectSwipe(Vector2Int tile, Direction direction, Swipe swipe)
        {
            if (tile != _tilePosition)
                return false;

            if (direction != _swipeDirection)
                return false;

            if (_swipeType != Swipe.Null && swipe != _swipeType)
                return false;

            SetComplete();
            return true;
        }
    }
}