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

        public override void Play(Action onComplete)
        {
            Vector2 matrixToPosition =
                GameTypesConverter.MatrixPositionToGamePosition(_moveTo, _gameFieldSettings.CellSize);
            Vector3 newPosition = _tile.Position;
            newPosition.x = matrixToPosition.x;
            newPosition.y = matrixToPosition.y;

            Ease ease = Ease.Linear;

            float time = CalculateTime(_gameFieldSettings.TileSpeed, _swipe);

            if (_swipe != Swipe.Infinite)
            {
                _tile.transform.DOMove(newPosition, time)
                    .SetEase(ease)
                    .OnComplete(() => onComplete.Invoke());
            }
            else
            {
                TilesController.Instance.StartCoroutine(Moving(_tile.transform, 
                    _tile.Position, 
                    newPosition, 
                    onComplete));
            }
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
            return time;
        }

        private IEnumerator Moving(Transform tile, Vector3 startPosition, Vector3 endPosition, Action onComplete)
        {
            float testTime = 0;
            float timeStep = 0.01f;

            var moveDelta = endPosition - startPosition;
            float maxCoord = Mathf.Max(Mathf.Abs(moveDelta.x), Mathf.Abs(moveDelta.y));
            
            float startSpeed = _gameFieldSettings.TileSpeed;
            
            float timeToMaxCoord = maxCoord / startSpeed;
            float cylcesPerTile = 1 / startSpeed / timeStep;
            float acceleration = _gameFieldSettings.TileAcceleration * timeStep / cylcesPerTile;

            float currentTime = 0;
            float currentAcceleration = 0;

            while (currentTime < timeToMaxCoord)
            {
                currentTime += timeStep + currentAcceleration;
                currentAcceleration += acceleration;
                tile.position = Vector3.Lerp(startPosition, endPosition, currentTime / timeToMaxCoord);

                testTime += timeStep;
                
                yield return new WaitForSeconds(timeStep);
            }

            //WUtils.ReworkPoint("TestTime: " + testTime);
            onComplete.Invoke();
        }

    }
}