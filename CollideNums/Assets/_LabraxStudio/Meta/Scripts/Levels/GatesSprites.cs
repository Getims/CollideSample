using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta
{
    [Serializable]
    public class GatesSprites
    {
        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontalUnlocked;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontalLocked;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateVerticalUnlocked;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateVerticalLocked;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateVerticalBackground;

        [SerializeField, LabelWidth(175)]
        private Sprite _gateHorizontalBackground;

        [Space(10)]
        [SerializeField, LabelWidth(175)]
        private List<Sprite> _gatesNumbers = new List<Sprite>();
    }
}