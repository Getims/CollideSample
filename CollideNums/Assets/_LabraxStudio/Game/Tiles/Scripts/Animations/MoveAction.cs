using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class MoveAction : AnimationAction
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public MoveAction(Tile tile, Vector2Int moveTo, Swipe swipe, float swipeSpeed = 1)
        {
            _tile = tile;
            _moveTo = moveTo;
            _swipe = swipe;
            _swipeSpeed = swipeSpeed;
            _gameFieldSettings = ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSettings;
        }

        // FIELDS: -------------------------------------------------------------------

        private Tile _tile;
        private Vector2Int _moveTo;
        private Swipe _swipe;
        private float _swipeSpeed = 1;
        private GameFieldSettings _gameFieldSettings;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetSwipeSpeed(float swipeSpeed)
        {
            _swipeSpeed = swipeSpeed;
        }

        public override void Play()
        {
            Vector2 matrixToPosition =
                GameTypesConverter.MatrixPositionToGamePosition(_moveTo, _gameFieldSettings.CellSize);
            Vector3 newPosition = _tile.Position;
            newPosition.x = matrixToPosition.x;
            newPosition.y = matrixToPosition.y;

            Ease ease = _gameFieldSettings.ShortMoveEase;
            if (_swipe == Swipe.Infinite)
                ease = _gameFieldSettings.LongMoveEase;

            //float time = CalculateTime(_gameFieldSettings.OneTileMoveTime, _tile.Position - newPosition);
            float time = CalculateTime(_gameFieldSettings.OneTileMoveTime, _swipe, _tile.Position - newPosition);
            
            //Moving(_tile.transform, _tile.Position, newPosition);
            
            //return;
            _tile.transform.DOMove(newPosition, time)
                .SetEase(ease);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private float CalculateTime(float oneTileMoveTime, Swipe swipe, Vector3 moveDelta)
        {
            float n = 0;
            float time = 0;
            switch (swipe)
            {
                case Swipe.Null:
                    return 0;
                case Swipe.OneTile:
                    n = 1;
                    time = oneTileMoveTime;
                    break;
                case Swipe.TwoTiles:
                    n = 2;
                    time = (oneTileMoveTime + (n - 1) * _gameFieldSettings.MoveSlowing * oneTileMoveTime) / n;
                    break;
                case Swipe.Infinite:
                    n = 3;
                    time = (oneTileMoveTime + (n - 1) * _gameFieldSettings.MoveSlowing * oneTileMoveTime) / 8;
                    break;
            }

            float x = Math.Abs(moveDelta.x);
            float y = Math.Abs(moveDelta.y);
            
            if (x > 0)
                n = x;

            if (y > 0)
                n = y;

            if (n > 8)
                n = 8;

            time = time * n;
            Utils.ReworkPoint("Time: " + time);
            return time;
        }

        private float CalculateTime(float oneTileMoveTime, Vector3 moveDelta)
        {
            float x = Math.Abs(moveDelta.x);
            float y = Math.Abs(moveDelta.y);
            float n = 0;

            if (x > 0)
                n = x;

            if (y > 0)
                n = y;

            float time = oneTileMoveTime + (n - 1) * oneTileMoveTime / (n - n * _gameFieldSettings.MoveSlowing);
            Utils.ReworkPoint("Time: " + time);
            return time;
        }

        private async void Moving(Transform tile, Vector3 startPosition, Vector3 endPosition)
        {
            float speed = 0.01f;
            float step = 0.015f;
            float t = 0;

            while (t<1)
            {
                t += speed;
                tile.position = Vector3.Lerp(startPosition, endPosition, t);
                speed += step;
                await Task.Delay(1);
            }
        }
    }
}