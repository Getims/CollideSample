using LabraxStudio.App.Services;
using LabraxStudio.Events;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class Tile : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private TileEffectsController _tileEffectsController;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Vector2Int Cell => _cell;
        public int Value => _value;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public bool IsMerging => _isMerging;
        public bool MovedToGate => _movedToGate;

        private TilesController TilesController => ServicesProvider.GameFlowService.TilesController;
        private BoostersController BoostersController => ServicesProvider.GameFlowService.BoostersController;

        // FIELDS: -------------------------------------------------------------------

        private Vector2Int _cell = Vector2Int.zero;
        private int _value;
        private TileSwipeChecker _swipeChecker = new TileSwipeChecker();
        private bool _isMerging = false;
        private bool _movedToGate = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(string tileName)
        {
            gameObject.name = tileName;
            _swipeChecker.Initialize(this, UnityEngine.Camera.main, OnSwipe);
        }

        public void SetCell(Vector2Int cell)
        {
            _cell = cell;
        }

        public void SetValue(int value, Sprite sprite)
        {
            _value = value;
            _spriteRenderer.sprite = sprite;
        }

        public void SetMergeFlag(bool isMerging)
        {
            _isMerging = isMerging;
            if (_isMerging)
                _swipeChecker.StopDragging();
        }

        public void SetMoveFlag(bool isMoving) => _swipeChecker.SetPause(isMoving);

        public void SetGateFlag()
        {
            _movedToGate = true;
            _swipeChecker.StopDragging();
        }

        public void PlayMergeEffect() => _tileEffectsController.PlayMergeEffect();

        public void PlayCollideEffect(Direction direction)
        {
            if (_isMerging)
                return;

            _tileEffectsController.PlayCollideEffect(direction);
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
        
        public void DestroyByObstacle(ObstacleType obstacleType, Direction direction = Direction.Null)
        {
            switch (obstacleType)
            {
                case ObstacleType.Null:
                case ObstacleType.Push:
                case ObstacleType.Stopper:
                    break;
                case ObstacleType.Saw:
                    //Destroy(gameObject);
                    _tileEffectsController.PlaySawEffect(direction, DestroySelf);
                    break;
                case ObstacleType.Hole:
                    _tileEffectsController.PlayHoleFallEffect(_spriteRenderer, DestroySelf);
                    _tileEffectsController.StopInfiniteMoveEffect();
                    break;
            }
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public void OnSelect()
        {
            if (BoostersController.IsBoosterActive)
            {
                bool lockedByTutorial = !ServicesProvider.TutorialService.CanUseBoosterOnTile(this);

                if (lockedByTutorial)
                    return;

                GameEvents.SendTileSelectForBooster(this);
                return;
            }

            _swipeChecker.OnSelect();
        }

        public void OnDeselect() => _swipeChecker.OnDeselect();

        public void OnSwipe(Direction direction, Swipe swipe)
        {
            bool lockedByTutorial = !ServicesProvider.TutorialService.CanMoveTile(_cell, direction, swipe);
            if (lockedByTutorial)
                return;

            if (swipe == Swipe.Infinite)
                _tileEffectsController.PlayInfiniteMoveEffect();
            else
                _tileEffectsController.StopInfiniteMoveEffect();

            TilesController.MoveTile(this, direction, swipe);
        }

    }
}