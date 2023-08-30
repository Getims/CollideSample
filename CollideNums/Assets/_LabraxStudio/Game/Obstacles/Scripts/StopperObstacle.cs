using LabraxStudio.Meta.GameField;
using UnityEngine;

namespace LabraxStudio.Game.Obstacles
{
    public class StopperObstacle : AObstacle
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private SpriteRenderer _stopper;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void SetupObstacle(ObstacleType obstacleType, Vector2Int cell,
            ObstaclesSprites obstaclesSprites, Direction obstacleDirection)
        {
            _cell = cell;
            _obstacleType = obstacleType;
            _stopper.sprite = obstaclesSprites.StopperMain;
        }

        public override void PlayTileCollideEffect()
        {
        }
    }
}