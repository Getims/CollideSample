using System.Collections.Generic;
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

        // PROPERTIES: ----------------------------------------------------------------------------

        public float CellSize => _cellSize;

        public Vector2 ShortSwipeDelta => _shortSwipeDelta;

        public Vector2 LongSwipeDelta => _longSwipeDelta;

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