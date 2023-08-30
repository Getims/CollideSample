using System.Collections.Generic;
using UnityEngine;
using LabraxStudio.Meta.GameField;
using LabraxStudio.UiAnimator;
using Sirenix.OdinInspector;

namespace LabraxStudio.Game.Obstacles
{
    public class SawObstacle : AObstacle
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private SpriteRenderer _sawMain;

        [SerializeField]
        private SpriteRenderer _sawPin;

        [SerializeField]
        private SpriteRenderer _sawShadow;

        [SerializeField]
        private UIAnimator _sawAnimator;

        [SerializeField]
        private List<ParticleSystem> _particleSystems;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void SetupObstacle(ObstacleType obstacleType, Vector2Int cell,
            ObstaclesSprites obstaclesSprites, Direction gateDirection)
        {
            _cell = cell;
            _obstacleType = obstacleType;
            SetupSprites(obstaclesSprites);
            _sawAnimator.Play();
        }

        [Button]
        public override void PlayTileCollideEffect()
        {
            foreach (var system in _particleSystems)
                system.Play();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetupSprites(ObstaclesSprites obstaclesSprites)
        {
            _sawMain.sprite = obstaclesSprites.SawMain;
            _sawPin.sprite = obstaclesSprites.SawPin;
            _sawShadow.sprite = obstaclesSprites.SawShadow;
        }
    }
}