using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.GameField
{
    [Serializable]
    public class SwipeSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private bool _useShortSwipes = true;
        
        [SerializeField]
        [ShowIf(nameof(_useShortSwipes))]
        private float _baseSwipeForce = 1.8f;
        
        [SerializeField]
        private float _accelSwipeForce = 5.4f;

        [SerializeField]
        private float _tileSpeed = 10f;

        [SerializeField]
        private float _tileAcceleration = 15f;

        [SerializeField]
        [Min(1)]
        private int _dragInsensitivity = 5;
        
        [SerializeField]
        [Min(0)]
        private float _dragMinSpeed = 1;

        // PROPERTIES: ----------------------------------------------------------------------------
       
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

        public float AccelSwipeForce
        {
            get => _accelSwipeForce;
            set => _accelSwipeForce = value;
        }

        public int DragInsensitivity
        {
            get => _dragInsensitivity;
            set => _dragInsensitivity = value;
        }

        public float DragMinSpeed
        {
            get => _dragMinSpeed;
            set => _dragMinSpeed = value;
        }

        public bool UseShortSwipes => _useShortSwipes;
    }
}