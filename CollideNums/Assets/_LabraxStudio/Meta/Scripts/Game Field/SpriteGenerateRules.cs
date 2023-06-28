using System;
using System.Collections.Generic;
using LabraxEditor;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace LabraxStudio.Meta.GameField.Rules
{
    [Serializable]
    public class SpriteGenerateRules: ScriptableMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [OdinSerialize]
        [SerializeField, ListDrawerSettings(NumberOfItemsPerPage = 10)]
        private List<SpriteRule> _rules = new List<SpriteRule>();

        // PUBLIC METHODS: -----------------------------------------------------------------------
        
        public bool HasRule(int rule)
        {
            foreach (var tileRule in _rules)
            {
                if (rule == tileRule.Rule)
                    return true;
            }
            
            return false;
        }
    }
    
}