using System;
using System.Collections;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.UiAnimator;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    [Serializable]
    internal class TileEffectsController
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private GameObject _infiniteMoveParticles;

        [SerializeField]
        private TileEffect _mergeEffect;

        [SerializeField]
        private CollideEffect _collideParticles;

        [SerializeField]
        private UIAnimator _holeFallEffect;

        [SerializeField]
        private SpriteMask _sawMask;

        [SerializeField]
        private SpriteRenderer _highLight;

        // FIELDS: -------------------------------------------------------------------

        private Tweener _obstacleTW;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void PlayMergeEffect()
        {
            _mergeEffect.PlayAnimation();
        }

        public void PlayInfiniteMoveEffect()
        {
            _infiniteMoveParticles.SetActive(true);
        }

        public void StopInfiniteMoveEffect()
        {
            _infiniteMoveParticles.SetActive(false);
        }

        public void PlayCollideEffect(Direction direction)
        {
            _collideParticles.SetDirection(direction);
            _collideParticles.Play();
        }

        public void PlayHoleFallEffect(SpriteRenderer tileSprite, Action onComplete)
        {
            _holeFallEffect.Play();
            Color newColor = Color.white;
            newColor.a = .2f;
            _obstacleTW = tileSprite.DOColor(newColor, _holeFallEffect.GetAnimatorWorkTime())
                .SetEase(Ease.InSine)
                .OnComplete(onComplete.Invoke);
        }

        public void PlaySawEffect(Direction direction, Action onComplete)
        {
            Vector3 sawMaskPosition = Vector3.zero;
            switch (direction)
            {
                case Direction.Left:
                    sawMaskPosition.x = -1f;
                    break;
                case Direction.Right:
                    sawMaskPosition.x = 1f;
                    break;
                case Direction.Up:
                    sawMaskPosition.y = 1f;
                    break;
                case Direction.Down:
                    sawMaskPosition.y = -1.2f;
                    break;
            }

            var maskTransform = _sawMask.transform;
            Vector3 localPosition = maskTransform.localPosition;
            Vector3 startPosition = localPosition;
            localPosition += sawMaskPosition;
            maskTransform.localPosition = localPosition;

            _sawMask.enabled = true;
            _obstacleTW = maskTransform.DOLocalMove(startPosition, 0.2f)
                .OnComplete(onComplete.Invoke);
        }

        public void SetHighlight(bool enabled) => _highLight.enabled = enabled;
    }
}