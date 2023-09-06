using System;
using System.Collections;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Meta.GameField;
using LabraxStudio.Sound;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class MoveAction : AnimationAction
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public MoveAction(Tile tile, Vector2Int moveTo, Swipe swipe, Direction direction, bool collideWithGate,
            ObstacleType obstacle, Vector2Int obstaclePosition)
        {
            _tile = tile;
            _moveTo = moveTo;
            _swipe = swipe;
            _direction = direction;
            _collideWithGate = collideWithGate;
            _obstacle = obstacle;
            _obstaclePosition = obstaclePosition;

            _gameFieldSettings = ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSettings;
            _swipeSettings = ServicesProvider.GameSettingsService.GetGameSettings().SwipeSettings;
        }

        // PROPERTIES: ----------------------------------------------------------------------------

        public Vector2Int MoveTo => _moveTo;
        private TilesController TilesController => ServicesProvider.GameFlowService.TilesController;
        public Tile Tile => _tile;
        public ObstacleType Obstacle => _obstacle;
        public Vector2Int ObstaclePosition => _obstaclePosition;

        // FIELDS: -------------------------------------------------------------------

        private readonly Tile _tile;
        private readonly Vector2Int _moveTo;
        private readonly Swipe _swipe;
        private readonly Direction _direction;
        private readonly bool _collideWithGate;
        private readonly ObstacleType _obstacle;
        private readonly GameFieldSettings _gameFieldSettings;
        private readonly SwipeSettings _swipeSettings;
        private readonly Vector2Int _obstaclePosition;
        private Action _onMoveComplete;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void Play(Action onComplete)
        {
            if (_tile == null)
                return;

            Vector2 matrixToPosition =
                GameTypesConverter.MatrixPositionToGamePosition(_moveTo, _gameFieldSettings.CellSize);
            Vector3 newPosition = _tile.Position;
            newPosition.x = matrixToPosition.x;
            newPosition.y = matrixToPosition.y;

            bool wasMoved = _tile.Position != newPosition;
            _onMoveComplete = onComplete;
            Ease ease = Ease.Linear;

            if (!wasMoved)
            {
                OnMoveComplete();
                return;
            }

            _tile.SetMoveFlag(true);
            float time = CalculateTime(_swipeSettings.TileSpeed, _swipe);

            if (_swipe != Swipe.Infinite)
            {
                GameSoundMediator.Instance.PlayTileMoveSFX();
                _tile.transform.DOMove(newPosition, time)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        if (_collideWithGate)
                            GameSoundMediator.Instance.PlayTileCollideGateSFX();
                        OnMoveComplete();
                    });
            }
            else
            {
                GameSoundMediator.Instance.PlayTileSuperMoveSFX();
                TilesController.StartCoroutine(Moving(_tile.transform,
                    _tile.Position,
                    newPosition));
            }
        }

        public float GetTime() => CalculateTime(_swipeSettings.TileSpeed, _swipe);

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
            float timeStep = 0.01f;

            var moveDelta = endPosition - startPosition;
            float maxCoord = Mathf.Max(Mathf.Abs(moveDelta.x), Mathf.Abs(moveDelta.y));

            float startSpeed = _swipeSettings.TileSpeed;

            float timeToMaxCoord = maxCoord / startSpeed;
            float cyclesPerTile = 1 / startSpeed / timeStep;
            float acceleration = _swipeSettings.TileAcceleration * timeStep / cyclesPerTile;

            float currentTime = 0;
            float currentAcceleration = 0;

            while (currentTime < timeToMaxCoord)
            {
                currentTime += timeStep + currentAcceleration;
                currentAcceleration += acceleration;
                if (tile == null)
                    break;
                tile.position = Vector3.Lerp(startPosition, endPosition, currentTime / timeToMaxCoord);

                if (currentTime < timeToMaxCoord)
                    yield return new WaitForSeconds(timeStep);
            }

            if (tile != null && _obstacle == ObstacleType.Null)
            {
                _tile.PlayCollideEffect(_direction);

                if (_collideWithGate)
                    GameSoundMediator.Instance.PlayTileCollideGateSFX();
                else
                {
                    if (!_tile.IsMerging && !_tile.MovedToGate)
                        GameSoundMediator.Instance.PlayTileCollideSFX();
                }
            }

            OnMoveComplete();
        }

        private void OnMoveComplete()
        {
            _tile.SetMoveFlag(false);
            _onMoveComplete?.Invoke();
        }
    }
}