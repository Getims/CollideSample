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
            ServicesProvider.GameFlowService.ObstaclesController.PlayObstacleEffect(_obstaclePosition);
            switch (_obstacleType)
            {
                case ObstacleType.Null:
                case ObstacleType.Stopper:
                    onComplete.Invoke();
                    break;
                case ObstacleType.Saw:
                    _tile.DestroyByObstacle(_obstacleType);
                    ServicesProvider.CoroutineService.RunCoroutine(Restart(0.35f));
                    break;
                case ObstacleType.Hole:
                    MoveTile();
                    _tile.DestroyByObstacle(_obstacleType);
                    ServicesProvider.CoroutineService.RunCoroutine(Restart(0.45f));
                    
                    return;
                case ObstacleType.Push:
                    ServicesProvider.CoroutineService.RunCoroutine(Push(0.02f));
                    break;
            }

            onComplete.Invoke();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnMoveComplete()
        {
            //_destroyAction?.Invoke(_tile);
            _onActionComplete?.Invoke();
        }

        private IEnumerator Restart(float delay)
        {
            yield return new WaitForSeconds(delay);
            _onActionComplete?.Invoke();
            GameEvents.SendTileDestroyedByObstacle();
        }
        
        private IEnumerator Push(float delay)
        {
            yield return new WaitForSeconds(delay);
            ServicesProvider.GameFlowService.TilesController.MoveTile(_tile, CalculatePushDirection(),
                Swipe.Infinite);
        }

        private void MoveTile()
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