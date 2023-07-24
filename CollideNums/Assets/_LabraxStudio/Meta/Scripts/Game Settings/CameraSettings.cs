using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta
{
    [Serializable]
    public class CameraSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private float _cameraSize = 7.15f;

        [SerializeField]
        private bool _moveCamera = false;

        [SerializeField]
        [ShowIf(nameof(_moveCamera))]
        private Ease _moveEase = Ease.OutCubic;

        [SerializeField]
        [ShowIf(nameof(_moveCamera))]
        private float _moveTime = 0.3f;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool MoveCamera => _moveCamera;
        public float CameraSize => _cameraSize;
        public Ease MoveEase => _moveEase;
        public float MoveTime => _moveTime;
    }
}