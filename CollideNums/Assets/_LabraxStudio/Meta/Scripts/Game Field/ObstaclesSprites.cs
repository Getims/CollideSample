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
        private Sprite _pushMain;
        
        [SerializeField]
        private Sprite _pushMetal;
        
        [SerializeField]
        private Sprite _pushButton;
        
        [Title("Stopper")]
        [SerializeField]
        private Sprite _stopperMain;
        
        // PROPERTIES: ----------------------------------------------------------------------------
        
        public Sprite SawMain => _sawMain;
        public Sprite SawPin => _sawPin;
        public Sprite SawShadow => _sawShadow;
        public Sprite HoleMain => _holeMain;
        public Sprite PushMain => _pushMain;
        public Sprite PushMetal => _pushMetal;
        public Sprite PushButton => _pushButton;
        public Sprite StopperMain => _stopperMain;
        public List<Color> ParticalsColors => _particalsColors;
    }
}