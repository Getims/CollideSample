using System;
using System.Collections;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Meta.GameField;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class MoveAction : AnimationAction
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public MoveAction(Tile tile, Vector2Int moveTo, Swipe swipe, Direction direction)
        {
            _tile = tile;
            _moveTo = moveTo;
            _swipe = swipe;
            _direction = direction;
            _gameFieldSettings = ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSettings;
        }
        
        // PROPERTIES: ----------------------------------------------------------------------------
        
        public Vector2Int MoveTo => _moveTo;
        private TilesController TilesController => ServicesProvider.GameFlowService.TilesController;
        
        // FIELDS: -------------------------------------------------------------------

        private readonly Tile _tile;
        private readonly Vector2Int _moveTo;
        private readonly Swipe _swipe;
        private Direction _direction;
        private readonly GameFieldSettings _gameFieldSettings;
        private Action _onMoveComplete;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void Play(Action onComplete)
        {
            Vector2 matrixToPosition =
                GameTypesConverter.MatrixPositionToGamePosition(_moveTo, _gameFieldSettings.CellSize);
            Vector3 newPosition = _tile.Position;
            newPosition.x = matrixToPosition.x;
            newPosition.y = matrixToPosition.y;

            _onMoveComplete = onComplete;
            Ease ease = Ease.Linear;

            float time = CalculateTime(_gameFieldSettings.TileSpeed, _swipe);

            if (_swipe != Swipe.Infinite)
            {
                _tile.transform.DOMove(newPosition, time)
                    .SetEase(ease)
                    .OnComplete(OnMoveComplete);
            }
            else
            {
                TilesController.StartCoroutine(Moving(_tile.transform,
                    _tile.Position,
                    newPosition));
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private float CalculateTime(float tileSpeed, Swipe swipe)
        {
            if (Math.Abs(tileSpeed) < 0.01f)
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

        private IEnumerator Moving(Transform tile, Vector3 startPosition, Vector3 endPosition)
        {
            float testTime = 0;
            float timeStep = 0.01f;

            var moveDelta = endPosition - startPosition;
            float maxCoord = Mathf.Max(Mathf.Abs(moveDelta.x), Mathf.Abs(moveDelta.y));

            float startSpeed = _gameFieldSettings.TileSpeed;

            float timeToMaxCoord = maxCoord / startSpeed;
            float cyclesPerTile = 1 / startSpeed / timeStep;
            float acceleration = _gameFieldSettings.TileAcceleration * timeStep / cyclesPerTile;

            float currentTime = 0;
            float currentAcceleration = 0;

            while (currentTime < timeToMaxCoord)
            {
                currentTime += timeStep + currentAcceleration;
                currentAcceleration += acceleration;
                tile.position = Vector3.Lerp(startPosition, endPosition, currentTime / timeToMaxCoord);

                testTime += timeStep;
                if(currentTime < timeToMaxCoord)
                    yield return new WaitForSeconds(timeStep);
            }

            //WUtils.ReworkPoint("TestTime: " + testTime);
            _tile.PlayCollideEffect(_direction);
            OnMoveComplete();
        }

        private void OnMoveComplete()
        {
            _onMoveComplete?.Invoke();
        }
    }
}