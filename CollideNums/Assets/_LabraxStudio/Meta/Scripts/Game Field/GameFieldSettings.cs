using LabraxEditor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.GameField
{
    public class GameFieldSettings : ScriptableMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private float _cellSize = 1;

        [Title("Swipe settings")]
        [SerializeField]
        private float _baseSwipeForce = 1.8f;

        [Title("Animation settings")]
        [SerializeField]
        private float _tileSpeed = 10f;

        [SerializeField]
        private float _tileAcceleration = 15f;

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
    }
}