using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game.Gates;
using LabraxStudio.Sound;
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
            if(_isMerging)
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
            if (!_isMerging)
                _tileEffectsController.PlayCollideEffect(direction);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public void OnSelect()
        {
            if (BoostersController.IsBoosterActive)
            {
                bool lockedByTutorial = !ServicesProvider.TutorialService.CanUseBoosterOnTile(this);
                
                if(lockedByTutorial)
                    return;
                
                GameEvents.SendTileSelectForBooster(this);
                return;
            }

            _swipeChecker.OnSelect();
        }

        public void OnDeselect() => _swipeChecker.OnDeselect();

        private void OnSwipe(Direction direction, Swipe swipe, float swipeSpeed)
        {
            bool lockedByTutorial = !ServicesProvider.TutorialService.CanMoveTile(_cell, direction, swipe);
            if(lockedByTutorial)
                return;
            
            if (swipe == Swipe.Infinite)
                _tileEffectsController.PlayInfiniteMoveEffect();
            else
                _tileEffectsController.StopInfiniteMoveEffect();

            TilesController.MoveTile(this, direction, swipe, swipeSpeed);
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}