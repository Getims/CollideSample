using LabraxStudio.Meta.GameField;
using LabraxStudio.Sound;
using UnityEngine;

namespace LabraxStudio.Game.Obstacles
{
    public class HoleObstacle : AObstacle
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private SpriteRenderer _holeMain;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void SetupObstacle(ObstacleType obstacleType, Vector2Int cell,
            ObstaclesSprites obstaclesSprites, Direction obstacleDirection)
        {
            _cell = cell;
            _obstacleType = obstacleType;
            _holeMain.sprite = obstaclesSprites.HoleMain;
        }

        public override void PlayTileCollideEffect()
        {
            GameSoundMediator.Instance.PlayTileCollideHoleSFX();
        }
    }
}