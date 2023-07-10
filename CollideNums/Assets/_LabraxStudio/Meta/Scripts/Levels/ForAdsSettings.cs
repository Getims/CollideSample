using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta.Levels
{
    [Serializable]
    public class ForAdsSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private bool _levelForAds = false;

        [Title("Camera")]
        [SerializeField, ShowIf(nameof(_levelForAds))]
        private float _cameraSize = 7.15f;

        [SerializeField, ShowIf(nameof(_levelForAds))]
        private bool _moveCamera = false;

        [SerializeField, ShowIf(nameof(_levelForAds))]
        private Vector2 _cameraOffset = new Vector2(0, 0);

        [Title("Tile")]
        [SerializeField, ShowIf(nameof(_levelForAds))]
        private bool _overrideTileMoveDistance = false;

        [SerializeField, ShowIf(nameof(OverrideMoveDistance))]
        private int _tileMoveMaxDistance = 8;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool OverrideMoveDistance => _levelForAds && _overrideTileMoveDistance;

        public bool LevelForAds => _levelForAds;
        public float CameraSize => _cameraSize;

        public bool MoveCamera => _levelForAds && _moveCamera;

        public int TileMoveMaxDistance => _tileMoveMaxDistance;

        public Vector2 CameraOffset => _cameraOffset;
    }
}