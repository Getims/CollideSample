using System;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    [Serializable]
    internal class TileEffectsController
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private TileEffect _mergeEffect;

        [SerializeField]
        private MovementEffect _movementEffect;

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

        public void StopMoveEffect()
        {
            _movementEffect.StopAnimation();
        }
    }
}