using System;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    internal class TileSwipeChecker
    {
        // FIELDS: -------------------------------------------------------------------

        private bool _isSelected = false;
        private UnityEngine.Camera _camera;
        private Tile _tile;
        private GameFieldSettings _gameFieldSettings;
        private Vector2 _mouseDownPos;
        private Action<Direction, Swipe> _onSwipe;
        
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(Tile tile, UnityEngine.Camera camera, Action<Direction, Swipe> onSwipe)
        {
            _tile = tile;
            _camera = camera;
            _gameFieldSettings = ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSettings;
            _onSwipe = onSwipe;
        }
        
        public void OnSelect()
        {
            if(!ServicesFabric.TouchService.IsTouchEnabled)
                return;

            _isSelected = true;
            _mouseDownPos = GetMousePosition();
        }

        public void OnDeselect()
        {
            if(!_isSelected)
                return;
            
            _isSelected = false;
            
            Vector2 _mouseUpPos = GetMousePosition();
            //_mouseDownPos = GetObjectPosition();
            Vector2 inputDelta = _mouseUpPos - _mouseDownPos;
            
            Direction _moveDirection = CalculateDirection(inputDelta);
            Swipe _swipe = CalculateSwipe(inputDelta, _moveDirection);
            
            Utils.ReworkPoint(_moveDirection + " " + _swipe);
            if(_swipe== Swipe.Null)
                return;
            
            if(_onSwipe!=null)
                _onSwipe.Invoke(_moveDirection, _swipe);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private Vector2 GetMousePosition()
        {
            var position = Input.mousePosition;
            position.z = 30;
            position = _camera.ScreenToWorldPoint(position);
            return position;
        }

        private Vector2 GetObjectPosition()
        {
            var position = _camera.WorldToScreenPoint(_tile.transform.position);
            position.z = 30;
            position = _camera.ScreenToWorldPoint(position);
            return position;
        }
        
        private Direction CalculateDirection(Vector2 inputDelta)
        {
            Direction _result = Direction.Null;
            if (Math.Abs(inputDelta.x) > Math.Abs(inputDelta.y))
            {
                _result = inputDelta.x > 0 ? Direction.Right : Direction.Left;
            }
            else
            {
                _result = inputDelta.y > 0 ? Direction.Up : Direction.Down;
            }

            return _result;
        }
        
        private Swipe CalculateSwipe(Vector2 inputDelta, Direction direction)
        {
            float delta = 0;
            if (direction == Direction.Up || direction == Direction.Down)
                delta = Math.Abs(inputDelta.y);
            else
                delta = Math.Abs(inputDelta.x);

            if (delta < _gameFieldSettings.ShortSwipeDelta.x)
                return Swipe.Null;

            if (delta <= _gameFieldSettings.ShortSwipeDelta.y)
                return Swipe.Short;

            if (delta >= _gameFieldSettings.LongSwipeDelta.x && delta <= _gameFieldSettings.LongSwipeDelta.y)
                return Swipe.Long;

            return Swipe.Infinite;
        }
    }
}