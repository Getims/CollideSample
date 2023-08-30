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

        // FIELDS: -------------------------------------------------------------------

        private Tweener _fallTW;

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
            _fallTW = tileSprite.DOColor(newColor, _holeFallEffect.GetAnimatorWorkTime())
                .SetEase(Ease.InSine)
                .OnComplete(onComplete.Invoke);
        }
    }
}