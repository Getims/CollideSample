using System;
using LabraxStudio.App.Services;
using LabraxStudio.Game;
using LabraxStudio.Game.Tiles;
using LabraxStudio.UI.Common;
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

        [SerializeField]
        private Pulsation _handClick;
        
        // FIELDS: -------------------------------------------------------------------

        private HandMover _currentMover;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopAnimations();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void PlayClickAnimation(Vector2Int tilePosition)
        {
            StopAnimations();
            Vector3 startPosition = CalculateStartPosition(tilePosition);
            transform.position = startPosition;
            Show(true);
            _handClick.StartPulse();
        }
        
        public void PlaySwipeAnimation(Vector2Int tilePosition, Direction swipeDirection, Swipe swipeType)
        {
            StopAnimations();
            Vector3 startPosition = CalculateStartPosition(tilePosition);
            _currentMover = _shortMove;
            if (swipeType == Swipe.Infinite)
                _currentMover = _longMove;

            _currentMover.StartMove(startPosition, swipeDirection, _targetCG);
        }
        
        public void StopAnimations()
        {
            _handClick.StopPulse();
            _shortMove.StopMove();
            _longMove.StopMove();
            Hide(true);
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void DebugMove(Vector3 tilePosition, Direction swipeDirection, Swipe swipeType)
        {
            StopAnimations();
            Vector3 startPosition = tilePosition;
            _currentMover = _shortMove;
            if (swipeType == Swipe.Infinite)
                _currentMover = _longMove;

            _currentMover.StartMove(startPosition, swipeDirection, _targetCG);
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