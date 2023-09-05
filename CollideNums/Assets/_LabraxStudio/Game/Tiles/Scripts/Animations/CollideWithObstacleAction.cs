using System;
using System.Collections;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class CollideWithObstacleAction : AnimationAction
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public CollideWithObstacleAction(Tile tile, Direction direction, Vector2Int obstaclePosition,
            ObstacleType obstacleType)
        {
            _tile = tile;
            _direction = direction;
            _obstaclePosition = obstaclePosition;
            _obstacleType = obstacleType;
        }

        // FIELDS: -------------------------------------------------------------------
        private Tile _tile;
        private Direction _direction;
        private Vector2Int _obstaclePosition;
        private ObstacleType _obstacleType;
        private Action _onActionComplete;
        private Tweener _extraMoveTW;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void Play(Action onComplete)
        {
            _onActionComplete = onComplete;
            switch (_obstacleType)
            {
                case ObstacleType.Null:
                case ObstacleType.Stopper:
                    onComplete.Invoke();
                    break;
                case ObstacleType.Saw:
                    ServicesProvider.CoroutineService.RunCoroutine(Sawing());
                    break;
                case ObstacleType.Hole:
                    MoveToHole();
                    _tile.DestroyByObstacle(_obstacleType);   
                    ServicesProvider.GameFlowService.ObstaclesController.PlayObstacleEffect(_obstaclePosition);
                    ServicesProvider.CoroutineService.RunCoroutine(Restart(0.45f));
                    break;
                case ObstacleType.Push:
                    ServicesProvider.GameFlowService.ObstaclesController.PlayObstacleEffect(_obstaclePosition);
                    ServicesProvider.CoroutineService.RunCoroutine(Push(0.02f));
                    break;
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnMoveComplete()
        {
            _onActionComplete?.Invoke();
        }

        private IEnumerator Restart(float delay)
        {
            yield return new WaitForSeconds(delay);
            OnMoveComplete();
            GameEvents.SendTileDestroyedByObstacle();
        }

        private IEnumerator Sawing()
        {
            Vector3 extraMove = Vector3.zero;
            switch (_direction)
            {
                case Direction.Left:
                    extraMove.x = -1f;
                    break;
                case Direction.Right:
                    extraMove.x = 1f;
                    break;
                case Direction.Up:
                    extraMove.y = 1f;
                    break;
                case Direction.Down:
                    extraMove.y = -1f;
                    break;
            }

            ServicesProvider.GameFlowService.ObstaclesController.PlayObstacleEffect(_obstaclePosition);
            _tile.DestroyByObstacle(_obstacleType, _direction);
            Vector3 newPosition = _tile.Position + extraMove;
            _extraMoveTW.Kill();
            _extraMoveTW = _tile.transform.DOMove(newPosition, 0.2f)
                .SetEase(Ease.OutQuad);

            yield return new WaitForSeconds(.25f);
            OnMoveComplete();
            GameEvents.SendTileDestroyedByObstacle();
        }

        private IEnumerator Push(float delay)
        {
            yield return new WaitForSeconds(delay);
            ServicesProvider.GameFlowService.TilesController.MoveTile(_tile, CalculatePushDirection(),
                Swipe.Infinite);
            OnMoveComplete();
        }

        private void MoveToHole()
        {
            if (_tile == null)
                return;

            Vector3 extraMove = Vector3.zero;
            switch (_direction)
            {
                case Direction.Left:
                    extraMove.x = -0.2f;
                    extraMove.y = -0.1f;
                    break;
                case Direction.Right:
                    extraMove.x = 0.2f;
                    extraMove.y = -0.1f;
                    break;
                case Direction.Up:
                    extraMove.y = -0.25f;
                    break;
                case Direction.Down:
                    extraMove.y = -0.25f;
                    break;
                default:
                    return;
            }

            Vector3 newPosition = _tile.Position + extraMove;
            _extraMoveTW.Kill();
            _extraMoveTW = _tile.transform.DOMove(newPosition, 0.25f)
                .SetEase(Ease.Linear);
        }

        private Direction CalculatePushDirection()
        {
            Vector2Int tilePosition = _tile.Cell;
            if (tilePosition.x < _obstaclePosition.x)
                return Direction.Left;
            if (tilePosition.x > _obstaclePosition.x)
                return Direction.Right;
            if (tilePosition.y < _obstaclePosition.y)
                return Direction.Up;
            if (tilePosition.y > _obstaclePosition.y)
                return Direction.Down;

            return Direction.Null;
        }
    }
}