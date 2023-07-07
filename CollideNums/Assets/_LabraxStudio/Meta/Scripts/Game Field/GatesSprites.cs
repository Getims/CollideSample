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
        private Color _unlockedColor = Color.white;

        [SerializeField]
        private Color _lockedColor = Color.black;

        [Title("Numbers")] 
        [SerializeField]
        private Color _numberBaseColor = Color.white;

        [SerializeField]
        private Color _numberGlowColor = Color.white;
        
        [SerializeField, LabelWidth(175)]
        private List<Sprite> _gatesNumbers = new List<Sprite>();

        // PROPERTIES: ----------------------------------------------------------------------------

        public Sprite GateVerticalBackground => _gateVerticalBackground;

        public Sprite GateHorizontalBackground => _gateHorizontalBackground;

        public List<Sprite> GatesNumbers => _gatesNumbers;

        public Color UnlockedColor => _unlockedColor;

        public Color LockedColor => _lockedColor;

        public Color NumberBaseColor => _numberBaseColor;

        public Color NumberGlowColor => _numberGlowColor;
    }
}