using LabraxEditor;
using UnityEngine;

namespace LabraxStudio.Meta.GameField
{
    public class GameFieldSettings : ScriptableMeta
    {
        [SerializeField]
        private float _cellSize = 1;
        
        public float CellSize => _cellSize;
    }
}