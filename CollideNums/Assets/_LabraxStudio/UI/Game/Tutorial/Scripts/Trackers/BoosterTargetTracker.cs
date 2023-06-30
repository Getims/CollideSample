using System;
using LabraxStudio.Meta.Tutorial;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    internal class BoosterTargetTracker : ARuleTracker
    {
        // FIELDS: -------------------------------------------------------------------

        private readonly Vector2Int _ruleBoosterTargetTile;
        private readonly Action _checkRules;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public BoosterTargetTracker(Vector2Int ruleBoosterTargetTile, Action checkRules)
        {
            _type = RuleType.BoosterTarget;
            _ruleBoosterTargetTile = ruleBoosterTargetTile;
            _checkRules = checkRules;
        }

        public override void SetComplete()
        {
            _isComplete = true;
            _checkRules?.Invoke();
        }

        public override void OnDestroy()
        {
        }
    }
}