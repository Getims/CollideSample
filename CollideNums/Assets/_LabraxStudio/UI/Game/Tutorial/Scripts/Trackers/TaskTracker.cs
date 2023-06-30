using System;
using LabraxStudio.Events;
using LabraxStudio.Meta.Tutorial;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    internal class TaskTracker : ARuleTracker
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public TaskTracker(Action checkRules)
        {
            _checkRules = checkRules;
            _type = RuleType.TaskComplete;
            GameEvents.OnAllTasksComplete.AddListener(OnAllTasksComplete);
        }

        // FIELDS: -------------------------------------------------------------------

        private readonly Action _checkRules;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void SetComplete()
        {
            _isComplete = true;
            GameEvents.OnAllTasksComplete.RemoveListener(OnAllTasksComplete);
            _checkRules?.Invoke();
        }

        public override void OnDestroy()
        {
            GameEvents.OnAllTasksComplete.RemoveListener(OnAllTasksComplete);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnAllTasksComplete() => SetComplete();
    }
}