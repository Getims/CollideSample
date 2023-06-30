using System;
using LabraxStudio.Game;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.Tutorial
{
    [Serializable]
    public class Rule
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField, HideIf(nameof(_hideTitle))]
        private bool _replaceTitle = false;

        [SerializeField, ShowIf(nameof(_replaceTitle))]
        private Sprite _newTitle;

        [SerializeField, HideIf(nameof(_replaceTitle))]
        private bool _hideTitle = false;

        [SerializeField]
        private RuleType _ruleType;

        [SerializeField]
        [ShowIf(nameof(TileSwipe))]
        private bool _showHand = true;

        [SerializeField]
        [ShowIf(nameof(TileSwipe))]
        private Vector2Int _tilePosition;

        [SerializeField]
        [ShowIf(nameof(TileSwipe))]
        private Direction _swipeDirection;

        [SerializeField]
        [ShowIf(nameof(TileSwipe))]
        private Swipe _swipeType;

        [SerializeField]
        [ShowIf(nameof(TileSwipe))]
        private bool _waitForMerge = false;

        [SerializeField]
        [ShowIf(nameof(TileSwipe))]
        private bool _waitForGateMove = false;

        [SerializeField]
        [ShowIf(nameof(BoosterUse))]
        private BoosterType _boosterType;

        [SerializeField]
        [ShowIf(nameof(BoosterTarget))]
        private Vector2Int _boosterTargetTile;

        // PROPERTIES: ----------------------------------------------------------------------------

        public RuleType RuleType => _ruleType;
        public bool ReplaceTitle => _replaceTitle;
        public bool HideTitle => _hideTitle;
        public Sprite NewTitle => _newTitle;
        public bool ShowHand => _showHand;
        public Vector2Int TilePosition => _tilePosition;
        public BoosterType BoosterType => _boosterType;
        public Vector2Int BoosterTargetTile => _boosterTargetTile;
        public Direction SwipeDirection => _swipeDirection;
        public Swipe SwipeType => _swipeType;
        public bool WaitForMerge => _waitForMerge;
        public bool WaitForGateMove => _waitForGateMove;


        private bool TileSwipe => _ruleType == RuleType.TileSwipe;
        private bool BoosterUse => _ruleType == RuleType.BoosterUse;
        private bool BoosterTarget => _ruleType == RuleType.BoosterTarget;

        private string ElementName =>
            $"{(_replaceTitle ? "ReplaceTitle | " : string.Empty)}" +
            $"{(_hideTitle ? "HideTitle | " : string.Empty)}" +
            $"{_ruleType} | " +
            $"{(BoosterUse ? (_boosterType + " | ") : string.Empty)}" +
            $"{(TileSwipe ? (_swipeType + " | ") : string.Empty)}";
    }
}