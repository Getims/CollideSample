using System;
using LabraxStudio.Events;
using LabraxStudio.Meta.Tutorial;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    internal class MergeTracker : ARuleTracker
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public MergeTracker(Action checkRules)
        {
            _checkRules = checkRules;
            _type = RuleType.Merge;
            GameEvents.OnTileMergesComplete.AddListener(OnMergesComplete);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private readonly Action _checkRules;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void SetComplete()
        {
            _isComplete = true;
            GameEvents.OnTileMergesComplete.RemoveListener(OnMergesComplete);
            _checkRules?.Invoke();
        }

        public override void OnDestroy()
        {
            GameEvents.OnTileMergesComplete.RemoveListener(OnMergesComplete);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnMergesComplete() => SetComplete();
    }
}