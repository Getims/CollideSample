using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.GameField
{
    [Serializable]
    public class PlayableSprites
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title("Backgrounds")]
        [SerializeField]
        private Sprite _baseBackground;

        [SerializeField]
        private Sprite _topBackground;

        [SerializeField]
        private Sprite _leftBackground;

        [SerializeField]
        private Sprite _rightBackground;

        [SerializeField]
        private Sprite _bottomBackground;
        
        [SerializeField]
        private Sprite _topBottomBackground;
        
        [SerializeField]
        private Sprite _leftRightBackground;

        [Title("Corners")]
        [SerializeField]
        private Sprite _topLeftCorner;

        [SerializeField]
        private Sprite _topRightCorner;

        [SerializeField]
        private Sprite _bottomRightCorner;

        [SerializeField]
        private Sprite _bottomLeftCorner;

        [SerializeField]
        private Sprite _fullLeftCorner;

        [SerializeField]
        private Sprite _fullTopCorner;

        [SerializeField]
        private Sprite _fullRightCorner;

        [SerializeField]
        private Sprite _fullDownCorner;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Sprite TopLeftCorner => _topLeftCorner;

        public Sprite TopRightCorner => _topRightCorner;

        public Sprite BottomRightCorner => _bottomRightCorner;

        public Sprite BottomLeftCorner => _bottomLeftCorner;

        public Sprite FullLeftCorner => _fullLeftCorner;

        public Sprite FullTopCorner => _fullTopCorner;

        public Sprite FullRightCorner => _fullRightCorner;

        public Sprite FullDownCorner => _fullDownCorner;

        public Sprite BaseBackground => _baseBackground;

        public Sprite TopBackground => _topBackground;

        public Sprite LeftBackground => _leftBackground;

        public Sprite RightBackground => _rightBackground;

        public Sprite BottomBackground => _bottomBackground;

        public Sprite TopBottomBackground => _topBottomBackground;

        public Sprite LeftRightBackground => _leftRightBackground;
    }
}