using System;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    [Serializable]
    internal class TileEffectsController
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private MovementEffect _movementEffect;

        [SerializeField]
        private GameObject _infiniteMoveParticles;

        [SerializeField]
        private TileEffect _mergeEffect;

        [SerializeField]
        private TileEffect _passGateEffect;
        
        [SerializeField]
        private GameObject _passGateParticles;

        [SerializeField]
        private CollideEffect _collideParticles;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void PlayMoveEffect(Direction direction)
        {
            _movementEffect.SetDirection(direction);
            _movementEffect.PlayAnimation();
        }

        public void PlayMergeEffect()
        {
            _mergeEffect.PlayAnimation();
        }

        public void PlayPassGateEffect()
        {
            _passGateParticles.SetActive(true);
           // _passGateEffect.PlayAnimation();
        }

        public void StopMoveEffect()
        {
            _movementEffect.StopAnimation();
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