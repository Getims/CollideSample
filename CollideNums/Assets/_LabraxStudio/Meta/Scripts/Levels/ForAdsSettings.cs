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

        [Title("Tile")]
        [SerializeField, ShowIf(nameof(_levelForAds))]
        private bool _overrideTileMoveDistance = false;

        [SerializeField, ShowIf(nameof(OverrideMoveDistance))]
        private int _tileMoveMaxDistance = 8;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool OverrideMoveDistance => _levelForAds && _overrideTileMoveDistance;
        public bool LevelForAds => _levelForAds;
        public float CameraSize => _cameraSize;
        public int TileMoveMaxDistance => _tileMoveMaxDistance;

    }
}