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
        private float _minSwipeSpeed = 2;

        [SerializeField]
        private float _oneTileSpeed = 4.5f;

        [Title("Animation settings")]
        [SerializeField]
        private float _oneTileMoveTime = 0.2f;

        [SerializeField]
        [Range(0.0f, 1)]
        private float _tileMoveSlowing = 0.15f;

        [SerializeField]
        private Ease _shortMoveEase = Ease.OutCubic;

        [SerializeField]
        private Ease _longMoveEase = Ease.InSine;

        // PROPERTIES: ----------------------------------------------------------------------------

        public float CellSize => _cellSize;

        public float OneTileMoveTime
        {
            get => _oneTileMoveTime;
            set => _oneTileMoveTime = value;
        }

        public float MoveSlowing
        {
            get => _tileMoveSlowing;
            set => _tileMoveSlowing = value;
        }

        public Ease ShortMoveEase
        {
            get => _shortMoveEase;
            set => _shortMoveEase = value;
        }

        public float MinSwipeSpeed
        {
            get => _minSwipeSpeed;
            set => _minSwipeSpeed = value;
        }

        public float OneTileSpeed
        {
            get => _oneTileSpeed;
            set => _oneTileSpeed = value;
        }

        public Ease LongMoveEase
        {
            get => _longMoveEase;
            set => _longMoveEase = value;
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