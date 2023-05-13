using System;
using System.Collections;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Meta;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class MoveAction : AnimationAction
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public MoveAction(Tile tile, Vector2Int moveTo, Swipe swipe)
        {
            _tile = tile;
            _moveTo = moveTo;
            _swipe = swipe;
            _gameFieldSettings = ServicesFabric.GameSettingsService.GetGameSettings().GameFieldSettings;
        }

        // FIELDS: -------------------------------------------------------------------

        private Tile _tile;
        private Vector2Int _moveTo;
        private Swipe _swipe;
        private GameFieldSettings _gameFieldSettings;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void Play()
        {
            Vector2 matrixToPosition =
                GameTypesConverter.MatrixPositionToGamePosition(_moveTo, _gameFieldSettings.CellSize);
            Vector3 newPosition = _tile.Position;
            newPosition.x = matrixToPosition.x;
            newPosition.y = matrixToPosition.y;

            Ease ease = _gameFieldSettings.ShortMoveEase;

            float time = CalculateTime(_gameFieldSettings.TileSpeed, _swipe);

            if (_swipe != Swipe.Infinite)
                _tile.transform.DOMove(newPosition, time).SetEase(ease);
            else
                TilesController.Instance.StartCoroutine(Moving(_tile.transform, _tile.Position, newPosition));
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private float CalculateTime(float tileSpeed, Swipe swipe)
        {
            if (tileSpeed == 0)
                return 0;

            float time = 0;
            int n = 0;

            switch (swipe)
            {
                case Swipe.Null:
                    return 0;
                case Swipe.OneTile:
                    n = 1;
                    break;
                case Swipe.TwoTiles:
                    n = 2;
                    break;
                case Swipe.Infinite:
                    n = 3;
                    break;
            }

            time = n / tileSpeed;
            Utils.ReworkPoint("Time: " + time);
            return time;
        }

        /*
        private async void Moving(Transform tile, Vector3 startPosition, Vector3 endPosition)
        {
            var moveDelta = endPosition - startPosition;
            float n = 0;
            float x = Math.Abs(moveDelta.x);
            float y = Math.Abs(moveDelta.y);
            
            if (x > 0)
                n = x;

            if (y > 0)
                n = y;
            
            var testTime = Time.realtimeSinceStartup;
            float speed = 0.1f;
            float step = 0;//0.015f;
            float t = 0;

            while (t<1)
            {
                t += speed;
                tile.position = Vector3.Lerp(startPosition, endPosition, t);
                speed += step;
                await Task.Delay(1);
            }

            var testEnd = Time.realtimeSinceStartup;
            
            Utils.ReworkPoint("TestTime: " + (testEnd-testTime));
        }*/

        private IEnumerator Moving(Transform tile, Vector3 startPosition, Vector3 endPosition)
        {
            float testTime = 0;
            float timeStep = 0.01f;

            var moveDelta = endPosition - startPosition;
            float maxCoord = Mathf.Max(Mathf.Abs(moveDelta.x), Mathf.Abs(moveDelta.y));
            
            float startSpeed = _gameFieldSettings.TileSpeed;
            
            float timeToMaxCoord = maxCoord / startSpeed;
            
            float acceleration = _gameFieldSettings.TileAcceleration;

            float currentTime = 0;
            float currentAcceleration = 0;

            while (currentTime < timeToMaxCoord)
            {
                currentTime += Time.deltaTime + currentAcceleration;
                
                currentAcceleration += acceleration*timeStep*timeStep/2;
                
                tile.position = Vector3.Lerp(startPosition, endPosition, currentTime / timeToMaxCoord);

                testTime += Time.deltaTime;
                
                yield return new WaitForSeconds(timeStep);
            }

            Utils.ReworkPoint("TestTime: " + testTime);
        }

        /*
           private IEnumerator Moving(Transform tile, Vector3 startPosition, Vector3 endPosition)
        {
            float testTime = 0;
            
            var moveDelta = endPosition - startPosition;
            float maxCoord = Mathf.Max(Mathf.Abs(moveDelta.x), Mathf.Abs(moveDelta.y));
            float startSpeed = _gameFieldSettings.TileSpeed;
            float timeToMaxCoord = maxCoord / startSpeed;
            float acceleration = _gameFieldSettings.TileAcceleration;

            float step = 0.01f;
            startSpeed = step;
            float currentTime = 0;
            float currentSpeed = 0;

            while (currentTime < timeToMaxCoord)
            {
                currentTime += step + currentSpeed;
                currentSpeed += startSpeed*acceleration * Time.deltaTime;
                tile.position = Vector3.Lerp(startPosition, endPosition, currentTime / timeToMaxCoord);

                testTime += step;
                // Ждем шаг времени
                yield return new WaitForSeconds(step);
            }

            Utils.ReworkPoint("TestTime: " + testTime);
        }
         */
    }
}