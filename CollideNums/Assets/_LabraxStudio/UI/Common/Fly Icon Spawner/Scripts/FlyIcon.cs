using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace LabraxStudio.UI.Common.IconsSpawner
{
    public class FlyIcon : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Image _iconImg;

        [SerializeField]
        private RectTransform _iconRT;

        [SerializeField]
        private CanvasGroup _iconCG;

        // FIELDS: -------------------------------------------------------------------

        private Tweener _moveTwnr;
        private Tweener _scaleTwnr;
        private Tweener _scaleFromZeroTwnr;
        private Tweener _showTwnr;
        private Tweener _hideTwnr;
        private float _time;
        private float _endScale;
        private FlySettings _flySettings = new FlySettings();
        private Action _onComplete;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy()
        {
            KillTweeners();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(Sprite sprite, FlySettings flySettings, Action onComplete = null)
        {
            KillTweeners();
            _iconImg.sprite = sprite;
            _flySettings = flySettings;
            _onComplete = onComplete;

            if (_flySettings.UseFade)
            {
                if (_flySettings.ShowTime > 0)
                    _iconCG.alpha = 0;
            }
            else
            {
                _iconCG.alpha = 1;
            }

            if (_flySettings.ChangeScale)
            {
                if (_flySettings.ScaleFromZeroTime > 0)
                    _iconRT.localScale = Vector3.zero;
                else
                    _iconRT.localScale = _flySettings.StartScale * Vector3.one;
            }
            else
            {
                _iconRT.localScale = Vector3.one;
            }

            _iconRT.anchoredPosition = new Vector2(0, 0);
        }

        public void PlayAnimation(Vector3 startPosition, Vector3 targetPosition)
        {
            Move(startPosition, targetPosition);

            if (_flySettings.UseFade)
                Fade();

            if (_flySettings.ChangeScale)
                Scale();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Move(Vector3 startPosition, Vector3 targetPosition)
        {
            _moveTwnr.Kill();
            _iconRT.position = startPosition;
            float moveTime = _flySettings.MoveTime;
            moveTime += Random.Range(0, _flySettings.ExtraMoveTimeRandom);

            _moveTwnr = _iconRT.DOMove(targetPosition, _flySettings.MoveTime)
                .SetEase(_flySettings.MoveEase)
                .SetDelay(_flySettings.MoveDelay)
                .OnComplete(() => _onComplete?.Invoke());
        }

        private void Scale()
        {
            _scaleTwnr.Kill();
            _scaleFromZeroTwnr.Kill();

            if (_flySettings.ScaleFromZeroTime > 0)
            {
                _scaleFromZeroTwnr = _iconRT
                    .DOScale(Vector3.one * _flySettings.StartScale, _flySettings.ScaleFromZeroTime)
                    .SetEase(_flySettings.ScaleEase);
            }

            _scaleTwnr = _iconRT.DOScale(Vector3.one * _flySettings.EndScale, _flySettings.ScaleTime)
                .SetEase(_flySettings.ScaleEase)
                .SetDelay(_flySettings.ScaleDelay);
        }

        private void Fade()
        {
            _showTwnr.Kill();
            _hideTwnr.Kill();

            _showTwnr = _iconCG.DOFade(1, _flySettings.ShowTime)
                .SetDelay(_flySettings.ShowDelay);
            _hideTwnr = _iconCG.DOFade(0, _flySettings.HideTime)
                .SetDelay(_flySettings.HideDelay);
        }

        private void KillTweeners()
        {
            _moveTwnr.Kill();
            _scaleTwnr.Kill();
            _scaleFromZeroTwnr.Kill();
            _showTwnr.Kill();
            _hideTwnr.Kill();
        }
    }
}