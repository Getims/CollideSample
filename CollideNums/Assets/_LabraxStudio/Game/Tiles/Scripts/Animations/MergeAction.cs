using System;
using DG.Tweening;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Sound;
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
            GameEvents.SendPreMergeTile(this);
            _onComplete = onComplete;
            Vector3 newPosition = _mergeTo.Position;

            Ease ease = Ease.OutSine;

            float time = 0.125f;
            _mergeFrom.transform.DOMove(newPosition, time)
                .SetEase(ease)
                .OnComplete(OnMoveComplete);
            
            GameSoundMediator.Instance.PlayTilesMergeSFX();
            Vector3 punch = new Vector3(0, 0.15f, 0);

            _mergeTo.PlayMergeEffect();
            _mergeTo.transform.DOPunchScale(Vector3.one * 0.1f, 0.25f, 1, 0)
                .SetDelay(time*0.8f);
            _mergeTo.transform.DOPunchPosition(punch, 0.25f, 1, 0f)
                .SetDelay(time*0.8f)
                .OnComplete(OnComplete);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnMoveComplete()
        {
            TilesController.DestroyTile(_mergeFrom);
            TilesController.CheckTileValue(_mergeTo);
            /*
            Vector3 punch = new Vector3(0, 0.3f, 0);

            _mergeTo.transform.DOPunchScale(Vector3.one * 0.1f, 0.5f, 1, 0);
            _mergeTo.transform.DOPunchPosition(punch, 0.5f, 1, 0f)
                .OnComplete(OnComplete);
                */
        }

        private void OnComplete()
        {
            if (_onComplete != null)
                _onComplete.Invoke();
        }
    }
}