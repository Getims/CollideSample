using LabraxStudio.Meta.GameField;
using UnityEngine;

namespace LabraxStudio.Game.Obstacles
{
    public abstract class AObstacle : MonoBehaviour
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public Vector2Int Cell => _cell;
        public ObstacleType ObstacleType => _obstacleType;

        // FIELDS: -------------------------------------------------------------------

        protected Vector2Int _cell = Vector2Int.zero;
        protected ObstacleType _obstacleType;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void DestroySelf()
        {
            if (gameObject != null)
                Destroy(gameObject);
        }

        public abstract void SetupObstacle(ObstacleType obstacleType, Vector2Int cell,
            ObstaclesSprites obstaclesSprites, Direction gateDirection);

        public abstract void PlayTileCollideEffect();
    }
}