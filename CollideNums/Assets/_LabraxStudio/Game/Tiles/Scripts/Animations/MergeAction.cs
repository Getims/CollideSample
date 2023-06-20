using System;
using DG.Tweening;
using LabraxStudio.App.Services;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class MergeAction : AnimationAction
    {
        // CONSTRUCTORS: -------------------------------------------------------------------------------

        public MergeAction(Tile mergeFrom, Tile mergeTo)
        {
            _mergeFrom = mergeFrom;
            _mergeTo = mergeTo;
        }

        // PROPERTIES: ----------------------------------------------------------------------------

        public Tile MergeFrom => _mergeFrom;
        public Tile MergeTo => _mergeTo;
        private TilesController TilesController => ServicesProvider.GameFlowService.TilesController;

        // FIELDS: -------------------------------------------------------------------

        private Tile _mergeFrom;
        private Tile _mergeTo;
        private Action _onComplete;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void Play(Action onComplete)
        {
            _onComplete = onComplete;
            Vector3 newPosition = _mergeTo.Position;

            Ease ease = Ease.OutSine;

            float time = 0.125f;
            _mergeFrom.transform.DOMove(newPosition, time)
                .SetEase(ease)
                .OnComplete(OnComplete);
        }

        private void OnComplete()
        {
            TilesController.DestroyTile(_mergeFrom);
            TilesController.CheckTileValue(_mergeTo);

            if (_onComplete != null)
                _onComplete.Invoke();
        }
    }
}