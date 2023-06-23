using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UI.Common.IconsSpawner
{
    [System.Serializable]
    public class FlySettings
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [Title("Move")]
        [SerializeField, Min(0)]
        private float _moveDelay = 0;

        [SerializeField, Min(0)]
        private float _moveTime = 0;

        [SerializeField]
        private Ease _moveEase;

        [SerializeField]
        private float _extraMoveTimeRandom = 0;

        [Title("Scale")]
        [SerializeField]
        private bool _changeScale = false;

        [SerializeField, Min(0)]
        [ShowIf(nameof(_changeScale))]
        private float _startScale = 0.5f;

        [ShowIf(nameof(_changeScale))]
        [SerializeField, Min(0)]
        private float _endScale = 0.5f;

        [SerializeField, Min(0)]
        [ShowIf(nameof(_changeScale))]
        private float _scaleTime = 0f;

        [SerializeField, Min(0)]
        [ShowIf(nameof(_changeScale))]
        private Ease _scaleEase;

        [SerializeField, Min(0)]
        [ShowIf(nameof(_changeScale))]
        private float _scaleDelay = 0.5f;

        [SerializeField, Min(0)]
        [ShowIf(nameof(_changeScale))]
        private float _scaleFromZeroTime = 0;

        [Title("Fade")]
        [SerializeField]
        private bool _useFade = false;

        [SerializeField, Min(0)]
        [ShowIf(nameof(_useFade))]
        private float _showTime;

        [SerializeField, Min(0)]
        [ShowIf(nameof(_useFade))]
        private float _showDelay;

        [SerializeField, Min(0)]
        [ShowIf(nameof(_useFade))]
        private float _hideTime;

        [SerializeField, Min(0)]
        [ShowIf(nameof(_useFade))]
        private float _hideDelay;

        // PROPERTIES: ----------------------------------------------------------------------------

        public float MoveDelay => _moveDelay;

        public float MoveTime => _moveTime;

        public Ease MoveEase => _moveEase;

        public bool ChangeScale => _changeScale;

        public float StartScale => _startScale;

        public float EndScale => _endScale;

        public bool UseFade => _useFade;

        public float ShowTime => _showTime;

        public float ShowDelay => _showDelay;

        public float HideTime => _hideTime;

        public float HideDelay => _hideDelay;

        public float ScaleTime => _scaleTime;

        public Ease ScaleEase => _scaleEase;

        public float ScaleDelay
        {
            get => _scaleDelay;
            set => _scaleDelay = value;
        }

        public float ScaleFromZeroTime => _scaleFromZeroTime;

        public float ExtraMoveTimeRandom => _extraMoveTimeRandom;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public float GetAnimationTime()
        {
            float time = _moveTime + _moveDelay + _extraMoveTimeRandom;

            if (_changeScale)
            {
                if (time < _scaleDelay + _scaleTime)
                    time = _scaleDelay + _scaleTime;
            }

            if (_useFade)
            {
                if (time < _hideDelay + _hideTime)
                    time = _hideDelay + _hideTime;
            }

            return time;
        }
    }
}