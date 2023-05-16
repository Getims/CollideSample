using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LabraxStudio.Meta
{
    [Serializable]
    public class PlayableSprites
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField, LabelText("Backgrounds")]
        private List<Sprite> _playableCells = new List<Sprite>();

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

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public Sprite GetRandomBackground()
        {
            int count = _playableCells.Count;
            return _playableCells[Random.Range(0, count)];
        }
    }
}