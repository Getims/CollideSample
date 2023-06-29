using System;
using UnityEngine;

namespace LabraxStudio.Meta.GameField
{
    [Serializable]
    public class SwipeSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField]
        private float _baseSwipeForce = 1.8f;
        
        [SerializeField]
        private float _accelSwipeForce = 5.4f;

        [SerializeField]
        private float _tileSpeed = 10f;

        [SerializeField]
        private float _tileAcceleration = 15f;

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
    }
}