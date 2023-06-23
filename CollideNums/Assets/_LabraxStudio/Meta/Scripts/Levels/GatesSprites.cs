using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta
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
        private Color _unlockedColor = Color.white;
        
        [SerializeField]
        private Color _lockedColor = Color.black;
        
        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontal;
        
        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontalBottom_1;
        
        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontalBottom_2;
        
        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontalBottom_3;
        
        [SerializeField, LabelWidth(175)]
        private Sprite _gateVertical;
        
        [SerializeField, LabelWidth(175)]
        private Sprite _gateVerticalTop;
        
        [SerializeField, LabelWidth(175)]
        private Sprite _gateVerticalBottom;

        [Space(10)]
        [SerializeField, LabelWidth(175)]
        private List<Sprite> _gatesNumbers = new List<Sprite>();

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public Sprite GateHorizontal => _gateHorizontal;
        public Sprite GateVertical => _gateVertical;

        public Sprite GateVerticalBackground => _gateVerticalBackground;

        public Sprite GateHorizontalBackground => _gateHorizontalBackground;

        public List<Sprite> GatesNumbers => _gatesNumbers;

        public Color UnlockedColor => _unlockedColor;

        public Color LockedColor => _lockedColor;

        public Sprite GateHorizontalBottom1 => _gateHorizontalBottom_1;

        public Sprite GateHorizontalBottom2 => _gateHorizontalBottom_2;

        public Sprite GateHorizontalBottom3 => _gateHorizontalBottom_3;

        public Sprite GateVerticalTop => _gateVerticalTop;

        public Sprite GateVerticalBottom => _gateVerticalBottom;
    }
}