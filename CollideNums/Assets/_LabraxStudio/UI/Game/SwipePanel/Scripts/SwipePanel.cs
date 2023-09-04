using System;
using System.Collections;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game;
using LabraxStudio.Game.Tiles;
using LabraxStudio.Meta.GameField;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Swipes
{
    public class SwipePanel : UIPanel
    {
        // FIELDS: -------------------------------------------------------------------

        private bool _isSelected = false;
        private bool _isDragging = false;
        private Camera _camera;
        private SwipeSettings _swipeSettings;
        private Vector2 _mouseDownPos;
        private float _mouseDownTime;
        private TilesController _tilesController;
        private Coroutine _drugCO;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            GameEvents.OnGenerateLevel.AddListener(OnLevelGenerate);
            GameEvents.OnGameOver.AddListener(OnGameOver);
            GameEvents.OnBoosterStateChange.AddListener(OnBoosterStateChange);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameEvents.OnGenerateLevel.RemoveListener(OnLevelGenerate);
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
            GameEvents.OnBoosterStateChange.RemoveListener(OnBoosterStateChange);
            if(_drugCO!=null)
                StopCoroutine(_drugCO);
        }

        private void Start()
        {
            if (GameFlowManager.IsLevelGenerated)
                OnLevelGenerate();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void OnSelect()
        {
            if (!ServicesProvider.TouchService.IsTouchEnabled)
                return;

            _isSelected = true;
            _isDragging = true;
            _mouseDownPos = GetMousePosition();
            _mouseDownTime = Time.realtimeSinceStartup;
            
            if(_drugCO!=null)
                StopCoroutine(_drugCO);
            _drugCO = StartCoroutine(DragTracker());
        }

        public void OnDeselect()
        {
            _isDragging = false;
            if (!_isSelected)
                return;

            _isSelected = false;

            Vector2 mouseUpPos = GetMousePosition();
            float mouseUpTime = Time.realtimeSinceStartup;
            Vector2 inputDelta = ConvertPositionToWorld(mouseUpPos) - ConvertPositionToWorld(_mouseDownPos);
            Direction moveDirection = CalculateDirection(inputDelta);

            float swipeTime = mouseUpTime - _mouseDownTime;
            float swipeSpeed = CalculateSwipeSpeed(inputDelta, moveDirection, swipeTime);
            Swipe swipe = CalculateSwipe(swipeSpeed);

            if (swipe == Swipe.Null)
                return;

            OnSwipe(moveDirection, swipe);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Initialize()
        {
            _camera = Camera.main;
            _swipeSettings = ServicesProvider.GameSettingsService.GetGameSettings().SwipeSettings;
            _tilesController = ServicesProvider.GameFlowService.TilesController;
            Show();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private Vector2 GetMousePosition()
        {
            var position = Input.mousePosition;
            //position.z = 30;
            //position = _camera.ScreenToWorldPoint(position);
            return position;
        }

        private Vector2 ConvertPositionToWorld(Vector2 position)
        {
            Vector3 newPosition = new Vector3(position.x, position.y, 30);
            newPosition = _camera.ScreenToWorldPoint(position);
            return newPosition;
        }

        IEnumerator DragTracker()
        {
            yield return new WaitForSeconds(0.1f);
            int checksCount = _swipeSettings.DragInsensitivity;
            float minSpeed = _swipeSettings.DragMinSpeed;
            int currentCheck = 0;
            float speedSumm = 0f;

            while (_isDragging)
            {
                if (currentCheck < checksCount)
                {
                    float speedX = Input.GetAxis("Mouse X");
                    float speedY = Input.GetAxis("Mouse Y");
                    float avgSpeed = (speedX + speedY) * 0.5f;
                    speedSumm += avgSpeed;
                    currentCheck++;
                }
                else
                {
                    float avgSpeed = speedSumm / checksCount;
                    avgSpeed = Math.Abs(avgSpeed * 100);
                    if (avgSpeed >= minSpeed)
                        OnDragStop();

                    speedSumm = 0;
                    currentCheck = 0;
                }

                yield return new WaitForEndOfFrame();
            }
        }

        private Direction CalculateDirection(Vector2 inputDelta)
        {
            Direction _result = Direction.Null;
            if (Math.Abs(inputDelta.x) > Math.Abs(inputDelta.y))
                _result = inputDelta.x > 0 ? Direction.Right : Direction.Left;
            else
                _result = inputDelta.y > 0 ? Direction.Up : Direction.Down;

            return _result;
        }

        private float CalculateSwipeSpeed(Vector2 inputDelta, Direction direction, float swipeTime)
        {
            float delta = 0;
            if (direction == Direction.Up || direction == Direction.Down)
                delta = Math.Abs(inputDelta.y);
            else
                delta = Math.Abs(inputDelta.x);

            float swipeSpeed = delta / swipeTime;
            UIEvents.SendCalculateSwipeSpeed(swipeSpeed);
            return swipeSpeed;
        }

        private Swipe CalculateSwipe(float swipeSpeed)
        {
            if (_swipeSettings.UseShortSwipes)
            {
                if (swipeSpeed < _swipeSettings.BaseSwipeForce)
                    return Swipe.Null;

                if (swipeSpeed < _swipeSettings.AccelSwipeForce)
                    return Swipe.OneTile;
            }
            else
            {
                if (swipeSpeed < _swipeSettings.AccelSwipeForce)
                    return Swipe.Null;
            }

            return Swipe.Infinite;
        }

        private Swipe CalculateSwipe(Vector2 inputDelta, Direction direction)
        {
            float delta = 0;
            if (direction == Direction.Up || direction == Direction.Down)
                delta = Math.Abs(inputDelta.y);
            else
                delta = Math.Abs(inputDelta.x);

            if (_swipeSettings.UseShortSwipes)
            {
                if (delta < 0.3f)
                    return Swipe.Null;

                if (delta > 1.0f)
                    return Swipe.Infinite;

                return Swipe.OneTile;
            }

            if (delta > 0.3f)
                return Swipe.Infinite;

            return Swipe.Null;
        }

        private void OnDragStop()
        {
            if (!_isDragging)
                return;

            _isSelected = false;
            Vector2 mouseUpPos = GetMousePosition();
            Vector2 inputDelta = ConvertPositionToWorld(mouseUpPos) - ConvertPositionToWorld(_mouseDownPos);
            Direction moveDirection = CalculateDirection(inputDelta);
            Swipe swipe = CalculateSwipe(inputDelta, moveDirection);

            _mouseDownPos = mouseUpPos;
            if (swipe == Swipe.Null)
                return;

            OnSwipe(moveDirection, swipe);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnGameOver(bool isWin)
        {
            if (isWin)
            {
                Hide();
                DestroySelfDelayed();
            }
        }

        private void OnLevelGenerate() => Initialize();

        private void OnSwipe(Direction direction, Swipe swipe)
        {
            if (_tilesController.IsAnyTileMove)
                return;

            TrackedTile trackedTile = _tilesController.GetTrackedTile();
            if (trackedTile == null)
                return;

            Tile tile = trackedTile.Tile;
            if (tile == null)
                return;

            tile.OnSwipe(direction, swipe);
        }

        private void OnBoosterStateChange(bool isActive)
        {
            if (isActive)
                Hide(true);
            else
                Show(true);
        }
    }
}