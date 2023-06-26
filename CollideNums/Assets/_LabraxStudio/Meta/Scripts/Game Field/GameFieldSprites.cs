using System;
using System.Collections.Generic;
using LabraxEditor;
using UnityEngine;

namespace LabraxStudio.Meta.GameField
{
    [Serializable]
    public class GameFieldSprites : ScriptableMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Color _backgroundColor = Color.white;
        
        [SerializeField]
        private Sprite _errorSprite;

        [Space(10)]
        [SerializeField]
        private PlayableSprites _playableSprites;

        [Space(10)]
        [SerializeField]
        private NotPlayableSprites _notPlayableSprites;
        
        [Space(10)]
        [SerializeField]
        private GatesSprites _gatesSprites;
        
        [Space(10)]
        [SerializeField]
        private List<Sprite> _tilesSprites = new List<Sprite>();

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public Color BackgroundColor => _backgroundColor;
        
        public Sprite ErrorSprite => _errorSprite;
        
        public PlayableSprites PlayableSprites => _playableSprites;

        public NotPlayableSprites NotPlayableSprites => _notPlayableSprites;

        public GatesSprites GateSprites => _gatesSprites;


        // PUBLIC METHODS: -----------------------------------------------------------------------

        public Sprite GetTileSprite(int spriteIndex)
        {
            spriteIndex -= 1;
            if (spriteIndex >= _tilesSprites.Count || spriteIndex < 0)
                return _errorSprite;

            Sprite sprite = _tilesSprites[spriteIndex];
            if (sprite == null)
                return _errorSprite;

            return sprite;
        }
    }
}