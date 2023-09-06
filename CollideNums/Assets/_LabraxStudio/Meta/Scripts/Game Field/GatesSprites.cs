using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.GameField
{
    [Serializable]
    public class GatesSprites
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title("Background")]
        [SerializeField, LabelWidth(175)]
        private Sprite _gateVerticalBackground;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontalBackground;

        [Title("Gate line")]
        [SerializeField]
        private bool _useColor = true;

        [SerializeField]
        [ShowIf(nameof(_useColor))]
        private Color _unlockedColor = Color.white;

        [SerializeField]
        [ShowIf(nameof(_useColor))]
        private Color _lockedColor = Color.black;

        [SerializeField]
        [HideIf(nameof(_useColor))]
        private Sprite _horizontalOpenLine;
        
        [SerializeField]
        [HideIf(nameof(_useColor))]
        private Sprite _verticalOpenLine;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontal;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontalBottom1;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontalBottom2;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontalBottom3;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontalBottom4;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateVertical;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateVerticalTop;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateVerticalBottom;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateVerticalCut;

        [Title("Numbers")]
        [SerializeField]
        private Color _numberBaseColor = Color.white;

        [SerializeField]
        private Color _numberGlowColor = Color.white;

        [SerializeField, LabelWidth(175)]
        private List<Sprite> _gatesNumbers = new List<Sprite>();

        public GatesSprites(Sprite verticalOpenLine)
        {
            _verticalOpenLine = verticalOpenLine;
        }

        // PROPERTIES: ----------------------------------------------------------------------------

        public Sprite GateVerticalBackground => _gateVerticalBackground;

        public Sprite GateHorizontalBackground => _gateHorizontalBackground;

        public List<Sprite> GatesNumbers => _gatesNumbers;

        public Color UnlockedColor => _unlockedColor;

        public Color LockedColor => _lockedColor;

        public Color NumberBaseColor => _numberBaseColor;

        public Color NumberGlowColor => _numberGlowColor;
        
        public Sprite HorizontalOpenLine => _horizontalOpenLine;
        public Sprite VerticalOpenLine => _verticalOpenLine;
        public bool UseColor => _useColor;
        public Sprite GateHorizontal => _gateHorizontal;
        public Sprite GateHorizontalBottom1 => _gateHorizontalBottom1;
        public Sprite GateHorizontalBottom2 => _gateHorizontalBottom2;
        public Sprite GateHorizontalBottom3 => _gateHorizontalBottom3;
        public Sprite GateHorizontalBottom4 => _gateHorizontalBottom4;
        public Sprite GateVertical => _gateVertical;
        public Sprite GateVerticalTop => _gateVerticalTop;
        public Sprite GateVerticalBottom => _gateVerticalBottom;
        public Sprite GateVerticalCut => _gateVerticalCut;

    }
}