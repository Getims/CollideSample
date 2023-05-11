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
        private Vector2 _shortSwipeDelta;

        [SerializeField]
        [LabelText("Middle Swipe Delta")]
        private Vector2 _longSwipeDelta;

        [Title("Animation settings")]
        [SerializeField]
        private float _oneCellTime = 0.15f;

        [SerializeField]
        private float _moveSlowing = 0.25f;

        [SerializeField]
        private Ease _moveEase = Ease.OutSine;

        // PROPERTIES: ----------------------------------------------------------------------------

        public float CellSize => _cellSize;

        public Vector2 ShortSwipeDelta
        {
            get => _shortSwipeDelta;
            set => _shortSwipeDelta = value;
        }

        public Vector2 LongSwipeDelta
        {
            get => _longSwipeDelta;
            set => _longSwipeDelta = value;
        }

        public float OneCellTime
        {
            get => _oneCellTime;
            set => _oneCellTime = value;
        }

        public float MoveSlowing
        {
            get => _moveSlowing;
            set => _moveSlowing = value;
        }

        public Ease MoveEase
        {
            get => _moveEase;
            set => _moveEase = value;
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