using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Tasks
{
    [Serializable]
    public class TaskCounter
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private TextMeshProUGUI _tilesCounter;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private float _fadeTime = 0.2f;

        // FIELDS: -------------------------------------------------------------------

        private bool _isEnabled = true;
        private Tweener _fadeTW;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetState(bool enabled)
        {
            if (_isEnabled != enabled)
            {
                _fadeTW?.Kill();
                _isEnabled = enabled;
                _canvasGroup.DOFade(enabled ? 1 : 0, _fadeTime);
            }

            if (!enabled)
                OnDestroy();
        }

        public void UpdateCounter(int tilesCount)
        {
            _tilesCounter.text = tilesCount.ToString();
        }

        public void OnDestroy()
        {
            _fadeTW?.Kill();
        }
    }
}