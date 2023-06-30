using System;
using LabraxStudio.Game;
using LabraxStudio.Meta.Tutorial;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    internal class BoosterUseTracker : ARuleTracker
    {
        // FIELDS: -------------------------------------------------------------------
        
        private readonly BoosterType _ruleBoosterType;
        private readonly Action _checkRules;

        // PUBLIC METHODS: -----------------------------------------------------------------------
        
        public BoosterUseTracker(BoosterType ruleBoosterType, Action checkRules)
        {
            _type = RuleType.BoosterUse;
            _ruleBoosterType = ruleBoosterType;
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