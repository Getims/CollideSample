using System;
using System.Collections;
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
                    _tile.DestroySelf();
                    ServicesProvider.GameFlowService.ObstaclesController.PlayObstacleEffect(_obstaclePosition);
                    ServicesProvider.CoroutineService.RunCoroutine(Restart(0.35f));
                    return;
                case ObstacleType.Hole:
                    break;
                case ObstacleType.Push:
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
    }
}