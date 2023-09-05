using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabraxStudio.Events;
using LabraxStudio.Game;
using LabraxStudio.Meta.Tutorial;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    public class TutorialController
    {
        // FIELDS: -------------------------------------------------------------------

        private LevelRules _currentRules;
        private Image _tutorialTitleText;
        private TutorialHand _tutorialHand;
        private Action _onTutorialComplete;
        private int _currentStep = 0;
        private List<ARuleTracker> _ruleTrackers = new List<ARuleTracker>();

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void OnDestroy()
        {
            GameEvents.OnTileAction.RemoveListener(CheckRules);
            foreach (var ruleTracker in _ruleTrackers)
                ruleTracker.OnDestroy();
            _ruleTrackers.Clear();
        }

        public void Initialize(LevelRules currentRules, Image tutorialTitleText, TutorialHand tutorialHand,
            Action onTutorialComplete)
        {
            OnDestroy();
            _currentRules = currentRules;
            _tutorialTitleText = tutorialTitleText;
            SetTitleState(null, true);
            _tutorialHand = tutorialHand;
            _onTutorialComplete = onTutorialComplete;
            _currentStep = 0;
            GameEvents.OnTileAction.AddListener(CheckRules);
        }

        public void StartTutorial()
        {
            StartStep(_currentStep);
        }

        public SwipeTracker GetSwipeTracker()
        {
            ARuleTracker tracker = _ruleTrackers.Find(r => r.Type == RuleType.TileSwipe);
            if (tracker == null)
                return null;

            return (SwipeTracker) tracker;
        }

        public BoosterUseTracker GetBoosterUseTracker()
        {
            ARuleTracker tracker = _ruleTrackers.Find(r => r.Type == RuleType.BoosterUse);
            if (tracker == null)
                return null;

            return (BoosterUseTracker) tracker;
        }
        
        public BoosterTargetTracker GetBoosterTargetTracker()
        {
            ARuleTracker tracker = _ruleTrackers.Find(r => r.Type == RuleType.BoosterTarget);
            if (tracker == null)
                return null;

            return (BoosterTargetTracker) tracker;
        }
        
        public bool HasLockTracker()
        {
            foreach (var ruleTracker in _ruleTrackers)
            {
                if (ruleTracker.IsComplete)
                    continue;

                if (ruleTracker.LockMoves())
                    return true;
            }

            return false;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async void StartNextStep()
        {
            await Task.Delay(100);
            _currentStep++;
            if (_currentStep >= _currentRules.RulesCount)
                SendTutorialComplete();
            else
                StartStep(_currentStep);
        }

        private void StartStep(int stepIndex)
        {
            Rule rule = _currentRules.GetRule(stepIndex);
            if (rule == null)
                return;

            if (rule.HideTitle)
                SetTitleState(null, false);

            if (rule.ReplaceTitle)
                SetTitleState(rule.NewTitle, true);

            switch (rule.RuleType)
            {
                case RuleType.TileSwipe:
                    _ruleTrackers.Add(new SwipeTracker(rule.TilePosition, rule.SwipeDirection, rule.SwipeType,
                        _tutorialHand, CheckRules, rule.TilePositionOffset));
                    if (rule.WaitForMerge)
                        _ruleTrackers.Add(new MergeTracker(CheckRules));
                    if (rule.WaitForGateMove)
                        _ruleTrackers.Add(new MoveToGateTracker(CheckRules));
                    break;
                case RuleType.TaskComplete:
                    _ruleTrackers.Add(new TaskTracker(CheckRules));
                    break;
                case RuleType.BoosterUse:
                    _ruleTrackers.Add(new BoosterUseTracker(rule.BoosterType, CheckRules));
                    break;
                case RuleType.BoosterTarget:
                    _ruleTrackers.Add(new BoosterTargetTracker(rule.BoosterTargetTile, _tutorialHand, CheckRules));
                    break;
                case RuleType.Merge:
                    if (rule.WaitForMerge)
                        _ruleTrackers.Add(new MergeTracker(CheckRules));
                    break;
            }
        }

        private void SendTutorialComplete()
        {
            if (_onTutorialComplete != null)
                _onTutorialComplete.Invoke();
        }

        private void SetTitleState(Sprite sprite, bool isEnabled)
        {
            if (sprite != null)
                _tutorialTitleText.sprite = sprite;

            _tutorialTitleText.enabled = isEnabled;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnStepComplete() => StartNextStep();

        private void CheckRules()
        {
            if (_ruleTrackers.Count == 0)
                return;

            foreach (var ruleTracker in _ruleTrackers)
            {
                if (!ruleTracker.IsComplete)
                    return;
            }

            _ruleTrackers.Clear();
            OnStepComplete();
        }

    }
}