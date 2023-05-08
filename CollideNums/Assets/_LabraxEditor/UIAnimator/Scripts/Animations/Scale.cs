using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UiAnimator
{
    [System.Serializable]
    public class Scale : Animation
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Transform _transform;

        [SerializeField]
        [HideIf(nameof(_instant))]
        private bool _useStartScale = false;

        [SerializeField]
        [HideIf(nameof(_instant)), ShowIf(nameof(_useStartScale))]
        private Vector3 _startScale = Vector3.one;

        [SerializeField]
        private Vector3 _targetScale;

        // FIELDS: -------------------------------------------------------------------

        private Tweener _scaleTW;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void Play()
        {
            if (_instant)
            {
                _transform.localScale = _targetScale;
                return;
            }

            _scaleTW.Kill();
            if (_useStartScale)
            {
                _transform.localScale = _startScale;
            }

            _scaleTW = _transform.DOScale(_targetScale, _animationTime)
                .SetEase(_animationEase)
                .SetDelay(_startDelay);
        }

        public override void Stop()
        {
            _scaleTW.Kill();
        }
    }
}