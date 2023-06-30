using System;
using LabraxStudio.Events;
using LabraxStudio.Meta.Tutorial;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    internal class MoveToGateTracker : ARuleTracker
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public MoveToGateTracker(Action checkRules)
        {
            _checkRules = checkRules;
            _type = RuleType.MoveToGate;
            GameEvents.OnMoveTileInGate.AddListener(OnMoveInGate);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private readonly Action _checkRules;
        
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void SetComplete()
        {
            _isComplete = true;
            GameEvents.OnMoveTileInGate.RemoveListener(OnMoveInGate);
            _checkRules?.Invoke();
        }

        public override void OnDestroy()
        {
            GameEvents.OnMoveTileInGate.RemoveListener(OnMoveInGate);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnMoveInGate(int arg0) => SetComplete();
    }
}