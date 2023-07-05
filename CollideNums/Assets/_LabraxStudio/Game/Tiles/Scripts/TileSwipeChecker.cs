using System;
using System.Collections;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Meta.GameField;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    internal class TileSwipeChecker
    {
        // FIELDS: -------------------------------------------------------------------

        private bool _isSelected = false;
        private bool _isDragging = false;
        private bool _isDragPause = false;
        private UnityEngine.Camera _camera;
        private SwipeSettings _swipeSettings;
        private Vector2 _mouseDownPos;
        private float _mouseDownTime;
        private Action<Direction, Swipe, float> _onSwipe;
        private Tile _tile;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(Tile tile, UnityEngine.Camera camera, Action<Direction, Swipe, float> onSwipe)
        {
            _tile = tile;
            _camera = camera;
            _swipeSettings = ServicesProvider.GameSettingsService.GetGameSettings().SwipeSettings;
            _onSwipe = onSwipe;
        }

        public void OnSelect()
        {
            if (!ServicesProvider.TouchService.IsTouchEnabled)
                return;

            _isSelected = true;
            _isDragging = true;
            _mouseDownPos = GetMousePosition();
            _mouseDownTime = Time.realtimeSinceStartup;
            ServicesProvider.CoroutineService.RunCoroutine(DragTracker());
        }

        public void OnDeselect()
        {
            _isDragging = false;
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

        public void StopDragging()
        {
            _isDragging = false;
        }

        public void SetPause(bool isPause)
        {
            _isDragPause = isPause;
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
            UIEvents.SendCalculateSwipeSpeed(swipeSpeed);
            return swipeSpeed;
        }

        private Swipe CalculateSwipe(float swipeSpeed)
        {
            if (swipeSpeed < _swipeSettings.BaseSwipeForce)
                return Swipe.Null;

            if (swipeSpeed < _swipeSettings.AccelSwipeForce)
                return Swipe.OneTile;

            return Swipe.Infinite;
        }

        private Swipe CalculateSwipe(Vector2 inputDelta, Direction direction, ref float swipeSpeed)
        {
            float delta = 0;
            if (direction == Direction.Up || direction == Direction.Down)
                delta = Math.Abs(inputDelta.y);
            else
                delta = Math.Abs(inputDelta.x);

            Utils.ReworkPoint("Delta: " + delta);
            if (delta < 0.5f)
                return Swipe.Null;

            if (delta > 1.5f)
            {
                swipeSpeed = _swipeSettings.AccelSwipeForce;
                return Swipe.Infinite;
            }

            swipeSpeed = _swipeSettings.BaseSwipeForce;
            return Swipe.OneTile;
        }

        IEnumerator DragTracker()
        {
            yield return new WaitForSeconds(0.1f);
            int checksCount = _swipeSettings.DragInsensitivity;
            float minSpeed = _swipeSettings.DragMinSpeed;
            int currentCheck = 0;
            float speedSumm = minSpeed+1f;

            while (_isDragging)
            {
                if (!_isDragPause)
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
                        Utils.ReworkPoint("Avg speed: " + avgSpeed);
                        if (avgSpeed <= minSpeed)
                        {
                            OnDragStop();
                        }

                        speedSumm = 0;
                        currentCheck = 0;
                    }
                }

                yield return new WaitForEndOfFrame();
            }
        }

        private void OnDragStop()
        {
            if (!_isDragging)
                return;

            _isSelected = false;
            Vector2 _tilePos = _tile.Position;
            Vector2 _mouseUpPos = GetMousePosition();
            Vector2 inputDelta = _mouseUpPos - _tilePos;
            Direction moveDirection = CalculateDirection(inputDelta);

            float swipeSpeed = 0;
            Swipe swipe = CalculateSwipe(inputDelta, moveDirection, ref swipeSpeed);

            if (swipe == Swipe.Null)
                return;

            if (_onSwipe != null)
                _onSwipe.Invoke(moveDirection, swipe, swipeSpeed);
        }

    }
}