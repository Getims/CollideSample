using System;
using LabraxStudio.Events;
using LabraxStudio.Game;
using LabraxStudio.Meta.Tutorial;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    public class BoosterUseTracker : ARuleTracker
    {
        // FIELDS: -------------------------------------------------------------------
        
        private readonly BoosterType _boosterType;
        private readonly Action _checkRules;

        // PUBLIC METHODS: -----------------------------------------------------------------------
        
        public BoosterUseTracker(BoosterType boosterType, Action checkRules)
        {
            _type = RuleType.BoosterUse;
            _boosterType = boosterType;
            _checkRules = checkRules;
            UIEvents.SendNeedBoosterHand(_boosterType);
        }

        public override void SetComplete()
        {
            _isComplete = true;
            _checkRules?.Invoke();
        }

        public override void OnDestroy()
        {
        }

        public bool IsCorrectBooster(BoosterType boosterType)
        {
            if (_boosterType != boosterType)
                return false;
            
            SetComplete();
            return true;
        }
    }
}