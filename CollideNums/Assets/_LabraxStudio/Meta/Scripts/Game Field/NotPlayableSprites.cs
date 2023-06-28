using System;
using System.Collections.Generic;
using LabraxStudio.Meta.GameField.Rules;
using UnityEngine;

namespace LabraxStudio.Meta.GameField
{
    [Serializable]
    public class NotPlayableSprites
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Sprite _background;

        [SerializeField]
        private Sprite _horizontalGateExtension;

        [SerializeField]
        private List<SpriteAndRule> _spriteRules;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public Sprite GetSpriteByRule(int rule)
        {
            foreach (var spriteRule in _spriteRules)
            {
                if(spriteRule.Rules.HasRule(rule))
                    return spriteRule.Sprite;
            }

            return null;
        }
        
        public Sprite Background => _background;
        public Sprite HorizontalGateExtension => _horizontalGateExtension;
    }
}