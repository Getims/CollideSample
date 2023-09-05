using LabraxStudio.Meta.GameField;
using LabraxStudio.Sound;
using LabraxStudio.UiAnimator;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Game.Obstacles
{
    public class PushObstacle : AObstacle
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title("Sprites")]
        [SerializeField]
        private SpriteRenderer _pushUpMain;

        [SerializeField]
        private SpriteRenderer _pushUpMetal;

        [SerializeField]
        private SpriteRenderer _pushUpButton;

        [SerializeField]
        private SpriteRenderer _pushSideMain;

        [SerializeField]
        private SpriteRenderer _pushSideMetal;

        [SerializeField]
        private SpriteRenderer _pushSideButton;

        [SerializeField]
        private SpriteRenderer _pushDownMain;

        [SerializeField]
        private SpriteRenderer _pushDownMetal;

        [SerializeField]
        private SpriteRenderer _pushDownButton;

        [Title("Containers")]
        [SerializeField]
        private GameObject _pushUpContainer;

        [SerializeField]
        private GameObject _pushDownContainer;

        [SerializeField]
        private GameObject _pushSideContainer;

        [Title("Animators")]
        [SerializeField]
        private UIAnimator _pushUpAnimator;

        [SerializeField]
        private UIAnimator _pushDownAnimator;

        [SerializeField]
        private UIAnimator _pushSideAnimator;

        // FIELDS: -------------------------------------------------------------------

        private UIAnimator _pushAnimator;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void SetupObstacle(ObstacleType obstacleType, Vector2Int cell,
            ObstaclesSprites obstaclesSprites, Direction obstacleDirection)
        {
            _cell = cell;
            _obstacleType = obstacleType;
            SetupSprites(obstaclesSprites, obstacleDirection);
            SetupDirection(obstacleDirection);
        }

        public override void PlayTileCollideEffect()
        {
            GameSoundMediator.Instance.PlayTileCollidePusherSFX();
            _pushAnimator.Play();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetupSprites(ObstaclesSprites obstaclesSprites, Direction obstacleDirection)
        {
            _pushUpMain.sprite = obstaclesSprites.PushUpMain;
            _pushUpMetal.sprite = obstaclesSprites.PushMetal;
            _pushUpButton.sprite = obstaclesSprites.PushUpButton;

            _pushSideMain.sprite = obstaclesSprites.PushHorizontalMain;
            _pushSideMetal.sprite = obstaclesSprites.PushHorizontalMetal;
            _pushSideButton.sprite = obstaclesSprites.PushHorizontalButton;

            _pushDownMain.sprite = obstaclesSprites.PushDownMain;
            _pushDownMetal.sprite = obstaclesSprites.PushMetal;
            _pushDownButton.sprite = obstaclesSprites.PushDownButton;
        }

        private void SetupDirection(Direction obstacleDirection)
        {
            _pushSideContainer.SetActive(false);
            _pushUpContainer.SetActive(false);
            _pushDownContainer.SetActive(false);

            switch (obstacleDirection)
            {
                case Direction.Left:
                    _pushAnimator = _pushSideAnimator;
                    _pushSideContainer.SetActive(true);
                    break;

                case Direction.Right:
                    _pushSideContainer.SetActive(true);
                    _pushAnimator = _pushSideAnimator;

                    var newScale = _pushSideContainer.transform.localScale;
                    newScale.y *= -1;
                    _pushSideContainer.transform.localScale = newScale;
                    break;

                case Direction.Up:
                    _pushUpContainer.SetActive(true);
                    _pushAnimator = _pushUpAnimator;
                    break;

                case Direction.Down:
                    _pushDownContainer.SetActive(true);
                    _pushAnimator = _pushDownAnimator;
                    break;

                default:
                    _pushAnimator = _pushUpAnimator;
                    break;
            }
        }
    }
}