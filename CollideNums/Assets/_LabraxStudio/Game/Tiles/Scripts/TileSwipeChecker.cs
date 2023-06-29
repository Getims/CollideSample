using System;
using LabraxStudio.App.Services;
using LabraxStudio.Game.Debug;
using LabraxStudio.Meta.GameField;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    internal class TileSwipeChecker
    {
        // FIELDS: -------------------------------------------------------------------

        private bool _isSelected = false;
        private UnityEngine.Camera _camera;
        private SwipeSettings _swipeSettings;
        private Vector2 _mouseDownPos;
        private float _mouseDownTime;
        private Action<Direction, Swipe, float> _onSwipe;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(Tile tile, UnityEngine.Camera camera, Action<Direction, Swipe, float> onSwipe)
        {
            _camera = camera;
            _swipeSettings = ServicesProvider.GameSettingsService.GetGameSettings().SwipeSettings;
            _onSwipe = onSwipe;
        }

        public void OnSelect()
        {
            if (!ServicesProvider.TouchService.IsTouchEnabled)
                return;

            _isSelected = true;
            _mouseDownPos = GetMousePosition();
            _mouseDownTime = Time.realtimeSinceStartup;
        }

        public void OnDeselect()
        {
            if (!_isSelected)
                return;

            _isSelected = false;

            Vector2 _mouseUpPos = GetMousePosition();
            float _mouseUpTime = Time.realtimeSinceStartup;
            Vector2 inputDelta = _mouseUpPos - _mouseDownPos;
            Direction moveDirection = CalculateDirection(inputDelta);
            
            float swipeTime = _mouseUpTime - _mouseDownTime;
            float swipeSpeed = CalculateSwipeSpeed(inputDelta, moveDirection, swipeTime);
            Swipe swipe = CalculateSwipe(swipeSpeed);

            if (swipe == Swipe.Null)
                return;

            if (_onSwipe != null)
                _onSwipe.Invoke(moveDirection, swipe, swipeSpeed);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private Vector2 GetMousePosition()
        {
            var position = Input.mousePosition;
            position.z = 30;
            position = _camera.ScreenToWorldPoint(position);
            return position;
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
            DebugMenu.Instance.UpdateSpeed(swipeSpeed);
            return swipeSpeed;
        }

        private Swipe CalculateSwipe(float swipeSpeed)
        {
            if(swipeSpeed<_swipeSettings.BaseSwipeForce)
                return Swipe.Null;
            
            if(swipeSpeed<_swipeSettings.AccelSwipeForce)
                return Swipe.OneTile;
            
            return Swipe.Infinite;
        }
    }
}