using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.Levels
{
    [Serializable]
    public class LevelTaskMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        [ValueDropdown("FriendlyTextureSizes")]
        private int _tileNumber = 0;


        [SerializeField]
        private int _tilesCount;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public int TileNumber => _tileNumber;

        public int TilesCount => _tilesCount;

        // FIELDS: -------------------------------------------------------------------
        
        private static IEnumerable FriendlyTextureSizes = new ValueDropdownList<int>()
        {
            { "2", 1},
            { "4", 2 },
            { "8", 3 },
            { "16", 4 },
            { "32", 5 },
            { "64", 6 },
            { "128", 7 },
            { "256", 8 },
            { "512", 9 },
            { "1K", 10 },
            { "2K", 11 },
            { "4K", 12 },
            { "8K", 13 },
            { "16K", 14 },
            { "32K", 15 },
            { "64K", 16 }
        };
        
      
    }
    
   
}