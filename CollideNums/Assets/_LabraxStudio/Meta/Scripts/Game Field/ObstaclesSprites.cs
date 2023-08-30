using System;
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
        
        [Title("Stopper")]
        [SerializeField]
        private Sprite _stopperMain;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public Sprite SawMain => _sawMain;
        public Sprite SawPin => _sawPin;
        public Sprite SawShadow => _sawShadow;
        public Sprite StopperMain => _stopperMain;
    }
}