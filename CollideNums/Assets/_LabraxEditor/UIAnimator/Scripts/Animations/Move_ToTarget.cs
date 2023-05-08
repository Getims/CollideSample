using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UiAnimator
{
    [System.Serializable]
    public class Move_ToTarget : Animation
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] 
        private Transform _transform;
        
        [SerializeField] 
        private bool _useLinker;
        
        [SerializeField]
        [HideIf(nameof(_useLinker))]
        private Transform _targetTransform;
        
        [SerializeField]
        [ShowIf(nameof(_useLinker))]
        private TransformLinker _targetLink;

        // FIELDS: -------------------------------------------------------------------
        
        private Tweener _moveTW;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void Play()
        {
            _moveTW.Kill();
            Transform target = null;

            if (_useLinker)
                target = _targetLink.GetLink();
            else
                target = _targetTransform;
              
            if(target == null)
                return;
            
            if (_instant)
            {
                _transform.position = target.position;
                return;
            }

            _moveTW = _transform.DOMove(target.position, _animationTime)
                .SetEase(_animationEase)
                .SetDelay(StartDelay);
        }

        public override void Stop()
        {
            _moveTW.Kill();
        }

    }
}
