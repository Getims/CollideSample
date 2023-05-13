using System.Collections.Generic;
using DG.Tweening;
using LabraxEditor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta
{
    public class GameFieldSettings : ScriptableMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private float _cellSize = 1;

        [Title("Sprites settings")]
        [SerializeField]
        private Sprite _errorSprite;

        [SerializeField]
        private List<Sprite> _fieldCellSprites = new List<Sprite>();

        [Space(10)]
        [SerializeField]
        private List<Sprite> _tilesSprites = new List<Sprite>();

        [Title("Swipe settings")]
        [SerializeField]
        private float _baseSwipeForce = 5.5f;

        [Title("Animation settings")]
        [SerializeField]
        private float _tileSpeed = 6f;

        [SerializeField]
        private float _tileAcceleration = 0.5f;

        [SerializeField]
        private Ease _shortMoveEase = Ease.OutCubic;

        // PROPERTIES: ----------------------------------------------------------------------------

        public float CellSize => _cellSize;

        public float BaseSwipeForce
        {
            get => _baseSwipeForce;
            set => _baseSwipeForce = value;
        }
        
        public float TileSpeed
        {
            get => _tileSpeed;
            set => _tileSpeed = value;
        }

        public float TileAcceleration
        {
            get => _tileAcceleration;
            set => _tileAcceleration = value;
        }

        public Ease ShortMoveEase
        {
            get => _shortMoveEase;
            set => _shortMoveEase = value;
        }
        
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public Sprite GetFieldSprite(int spriteIndex)
        {
            if (spriteIndex >= _fieldCellSprites.Count)
                return _errorSprite;

            Sprite sprite = _fieldCellSprites[spriteIndex];
            if (sprite == null)
                return _errorSprite;

            return sprite;
        }

        public Sprite GetTileSprite(int spriteIndex)
        {
            if (spriteIndex >= _tilesSprites.Count)
                return _errorSprite;

            Sprite sprite = _tilesSprites[spriteIndex];
            if (sprite == null)
                return _errorSprite;

            return sprite;
        }
    }
}