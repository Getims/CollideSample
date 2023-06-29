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
        private bool _showTitle = false;

        [SerializeField, HideIf(nameof(_showTitle))]
        private bool _hideTitle = false;

        [SerializeField]
        private RuleType _ruleType;

        [SerializeField]
        [ShowIf(nameof(SwipeOrMerge))]
        private bool _showHand = true;

        [SerializeField]
        [ShowIf(nameof(SwipeOrMerge))]
        private Vector2Int _tilePosition;

        [SerializeField]
        [ShowIf(nameof(TileSwipe))]
        private Vector2Int _swipePosition;

        [SerializeField]
        [ShowIf(nameof(TileMerge))]
        private Vector2Int _mergePosition;

        [SerializeField]
        [ShowIf(nameof(BoosterUse))]
        private BoosterType _boosterType;

        [SerializeField]
        [ShowIf(nameof(BoosterTarget))]
        private Vector2Int _boosterTargetTile;

        // PROPERTIES: ----------------------------------------------------------------------------

        public RuleType RuleType => _ruleType;

        public bool ShowHand => _showHand;

        public Vector2Int TilePosition => _tilePosition;

        public Vector2Int SwipePosition => _swipePosition;

        public Vector2Int MergePosition => _mergePosition;

        public BoosterType BoosterType => _boosterType;

        public Vector2Int BoosterTargetTile => _boosterTargetTile;

        private bool TileSwipe => _ruleType == RuleType.TileSwipe;
        private bool TileMerge => _ruleType == RuleType.TileMerge;
        private bool SwipeOrMerge => TileSwipe || TileMerge;
        private bool BoosterUse => _ruleType == RuleType.BoosterUse;
        private bool BoosterTarget => _ruleType == RuleType.BoosterTarget;

        private string ElementName =>
            $"{(_showTitle ? "ShowTitle | " : string.Empty)}" +
            $"{(_hideTitle ? "HideTitle | " : string.Empty)}"+
            $"{_ruleType} | " +
            $"{(BoosterUse ? (_boosterType + " | ") : string.Empty)}";
    }
}