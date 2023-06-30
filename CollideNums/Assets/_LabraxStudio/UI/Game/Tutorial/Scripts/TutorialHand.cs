using System;
using LabraxStudio.App.Services;
using LabraxStudio.Game;
using LabraxStudio.Game.Tiles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    public class TutorialHand : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private HandMover _shortMove;

        [SerializeField]
        private HandMover _longMove;

        // FIELDS: -------------------------------------------------------------------

        private HandMover _currentMover;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopMovers();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------
        
        public void PlaySwipeAnimation(Vector2Int tilePosition, Direction swipeDirection, Swipe swipeType)
        {
            StopMovers();
            Vector3 startPosition = CalculateStartPosition(tilePosition);
            _currentMover = _shortMove;
            if (swipeType == Swipe.Infinite)
                _currentMover = _longMove;

            _currentMover.StartMove(startPosition, swipeDirection, _targetCG);
        }
        
        [Button]
        public void StopSwipeAnimation()
        {
            StopMovers();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        [Button]
        private void DebugMove(Vector3 tilePosition, Direction swipeDirection, Swipe swipeType)
        {
            StopMovers();
            Vector3 startPosition = tilePosition;
            _currentMover = _shortMove;
            if (swipeType == Swipe.Infinite)
                _currentMover = _longMove;

            _currentMover.StartMove(startPosition, swipeDirection, _targetCG);
        }
        
        private void StopMovers()
        {
            _shortMove.StopMove();
            _longMove.StopMove();
        }

        private Vector3 CalculateStartPosition(Vector2Int tilePosition)
        {
            Tile tile = ServicesProvider.GameFlowService.TilesController.GetTile(tilePosition);
            if (tile == null)
                return Vector3.zero;

            Vector3 worldPosition = tile.transform.position;
            Camera mainCamera = Camera.main;
            return mainCamera != null ? mainCamera.WorldToScreenPoint(worldPosition) : Vector3.zero;
        }
    }
}