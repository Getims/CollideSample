using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UiAnimator
{
    [System.Serializable]
    public abstract class Animation
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        protected bool _instant = false;

        [SerializeField, Min(0), LabelWidth(125)]
        [HideIf(nameof(_instant))]
        protected float _startDelay;

        [SerializeField, Min(0), LabelWidth(125)]
        [HideIf(nameof(_hideValues))]
        protected float _animationTime;

        [SerializeField, LabelWidth(125)]
        [HideIf(nameof(_hideValues))]
        protected Ease _animationEase;

        // PROPERTIES: ----------------------------------------------------------------------------

        public float StartDelay => _startDelay;
        public float AnimationTime => _animationTime;
        protected bool _hideValues => _instant || _hideAnimationValues;

        // PROTECTED METHODS: -----------------------------------------------------------------------

        protected const bool _hideAnimationValues = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public abstract void Play();
        public abstract void Stop();
    }
}