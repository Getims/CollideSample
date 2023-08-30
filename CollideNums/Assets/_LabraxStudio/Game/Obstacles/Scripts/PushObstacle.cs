using LabraxStudio.Meta.GameField;
using LabraxStudio.UiAnimator;
using UnityEngine;

namespace LabraxStudio.Game.Obstacles
{
    public class PushObstacle : AObstacle
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private SpriteRenderer _pushMain;

        [SerializeField]
        private SpriteRenderer _pushMetal;

        [SerializeField]
        private SpriteRenderer _pushButton;

        [SerializeField]
        private Transform _rotateContainer;
        
        [SerializeField]
        private UIAnimator _pushAnimator;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void SetupObstacle(ObstacleType obstacleType, Vector2Int cell,
            ObstaclesSprites obstaclesSprites, Direction obstacleDirection)
        {
            _cell = cell;
            _obstacleType = obstacleType;
            SetupSprites(obstaclesSprites);
            SetupDirection(obstacleDirection);
        }

        public override void PlayTileCollideEffect()
        {
            _pushAnimator.Play();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetupSprites(ObstaclesSprites obstaclesSprites)
        {
            _pushMain.sprite = obstaclesSprites.PushMain;
            _pushMetal.sprite = obstaclesSprites.PushMetal;
            _pushButton.sprite = obstaclesSprites.PushButton;
        }

        private void SetupDirection(Direction obstacleDirection)
        { 
            int angle = 0;

            switch (obstacleDirection)
            {
                case Direction.Left:
                    angle = 90;
                    break;
                case Direction.Right:
                    angle = -90;
                    break;
                case Direction.Up:
                    angle = 0;
                    break;
                case Direction.Down:
                    angle = 180;
                    break;
                default:
                    angle = 0;
                    break;
            }

            _rotateContainer.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}