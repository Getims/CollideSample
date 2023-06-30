using System;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Meta.Tutorial;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    public class BoosterTargetTracker : ARuleTracker
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------
        
        public BoosterTargetTracker(Vector2Int targetTile, TutorialHand tutorialHand, Action checkRules)
        {
            _type = RuleType.BoosterTarget;
            _targetTile = targetTile;
            _checkRules = checkRules;
            
            _tutorialHand = tutorialHand;
            _tutorialHand.PlayClickAnimation(targetTile);
        }
        
        // FIELDS: -------------------------------------------------------------------

        private readonly Vector2Int _targetTile;
        private readonly Action _checkRules;
        private readonly TutorialHand _tutorialHand;

        // PUBLIC METHODS: -----------------------------------------------------------------------


        public override void SetComplete()
        {
            _isComplete = true;
            _checkRules?.Invoke();
            _tutorialHand.StopAnimations();
        }

        public override void OnDestroy()
        {
            _tutorialHand.StopAnimations();
        }

        public bool IsCorrectTarget(Tile tile)
        {
            if (_targetTile != tile.Cell)
                return false;
            
            SetComplete();
            return true;
        }
    }
}