using System;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Game.Gates;
using LabraxStudio.Meta.GameField;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class MoveInGateAction : AnimationAction
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public MoveInGateAction(Tile tile, Direction direction, Action<Tile> destroyAction, Vector2Int gateCell)
        {
            _tile = tile;
            _direction = direction;
            _destroyAction = destroyAction;
            _gateCell = gateCell;
            _gameFieldSettings = ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSettings;
        }

        // FIELDS: -------------------------------------------------------------------

        private Tile _tile;
        private Direction _direction;
        private Action<Tile> _destroyAction;
        private Action _onMoveComplete;
        private GameFieldSettings _gameFieldSettings;
        private Vector2Int _gateCell;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void Play(Action onComplete)
        {
            Vector2 gateVector = GameTypesConverter.DirectionToVector2Int(_direction) * 5;
            Vector3 newPosition = _tile.Position;
            newPosition.x += gateVector.x;
            newPosition.y -= gateVector.y;

            _onMoveComplete = onComplete;
            Ease ease = Ease.OutCubic;

            float time = CalculateTime(_gameFieldSettings.TileSpeed);

            Gate gate = ServicesProvider.GameFlowService.GatesController.GetGate(_gateCell);
            if(gate!=null)
                gate.PlayPassGateEffect();
            
            _tile.transform.DOMove(newPosition, time)
                .SetEase(ease)
                .OnComplete(OnMoveComplete);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private float CalculateTime(float tileSpeed)
        {
            if (tileSpeed == 0)
                return 0;

            float time = 0;
            int n = 3;

            time = n / tileSpeed;
            return time;
        }

        private void OnMoveComplete()
        {
            _destroyAction?.Invoke(_tile);
            _onMoveComplete?.Invoke();
        }
    }
}