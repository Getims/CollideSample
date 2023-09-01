using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.GameField
{
    [Serializable]
    public class ObstaclesSprites
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title("Saw")]
        [SerializeField]
        private Sprite _sawMain;

        [SerializeField]
        private Sprite _sawPin;

        [SerializeField]
        private Sprite _sawShadow;

        [SerializeField]
        [ListDrawerSettings(NumberOfItemsPerPage = 10)]
        private List<Color> _particalsColors = new List<Color>();

        [Title("Hole")]
        [SerializeField]
        private Sprite _holeMain;

        [Title("Push")]
        [SerializeField]
        private Sprite _pushMetal;

        [SerializeField]
        private Sprite _pushUpButton;

        [SerializeField]
        private Sprite _pushUpMain;

        [SerializeField]
        private Sprite _pushHorizontalButton;

        [SerializeField]
        private Sprite _pushHorizontalMain;

        [SerializeField]
        private Sprite _pushDownButton;

        [SerializeField]
        private Sprite _pushDownMain;

        [Title("Stopper")]
        [SerializeField]
        private Sprite _stopperMain;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Sprite SawMain => _sawMain;
        public Sprite SawPin => _sawPin;
        public Sprite SawShadow => _sawShadow;
        public List<Color> ParticalsColors => _particalsColors;

        public Sprite HoleMain => _holeMain;

        public Sprite PushMetal => _pushMetal;
        public Sprite PushUpButton => _pushUpButton;
        public Sprite PushUpMain => _pushUpMain;
        public Sprite PushHorizontalButton => _pushHorizontalButton;
        public Sprite PushHorizontalMain => _pushHorizontalMain;
        public Sprite PushDownButton => _pushDownButton;
        public Sprite PushDownMain => _pushDownMain;

        public Sprite StopperMain => _stopperMain;
    }
}