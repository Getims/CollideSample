using System;
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
    }
}