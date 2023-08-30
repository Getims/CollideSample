using LabraxStudio.Meta.GameField;
using UnityEngine;

namespace LabraxStudio.Game.Obstacles
{
    public class MockObstacle : AObstacle
    {
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void SetupObstacle(ObstacleType obstacleType, Vector2Int cell,
            ObstaclesSprites obstaclesSprites, Direction obstacleDirection)
        {
            _cell = cell;
            _obstacleType = obstacleType;
        }

        public override void PlayTileCollideEffect()
        {
        }
    }
}