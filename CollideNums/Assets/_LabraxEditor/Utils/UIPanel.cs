using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UI
{
    public class UIPanel : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [FoldoutGroup(Settings)]
        [SerializeField, Min(0)]
        private float _fadeTime = 0.35f;

        [FoldoutGroup(Settings)]
        [SerializeField, Required]
        [InlineButton(nameof(GetCanvasGroup), label: "Try Set")]
        protected CanvasGroup _targetCG;

        [FoldoutGroup(Settings)]
        [SerializeField]
        private bool _ignoreScaleTime;

        [FoldoutGroup(Settings)]
        [SerializeField]
        private bool _useCanvas;

        [FoldoutGroup(Settings)]
        [SerializeField]
        private bool _hideOnAwake;
        
        [FoldoutGroup(Settings)]
        [SerializeField]
        [ShowIf(nameof(_useCanvas))]
        private Canvas _canvas;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public float FadeTime => _fadeTime;

        // FIELDS: --------------------------------------------------------------------------------
        
        private const string Settings = "UI Panel Settings";
        
        public event Action HideEvent;

        private Tweener _fadeTN;
        private bool _instant;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected virtual void Awake()
        {
            if (_hideOnAwake)
                _targetCG.alpha = 0;
        }

        protected virtual void OnDestroy()
        {
            _fadeTN.Kill();
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public virtual void Show()
        {
            _instant = false;
            
            SetCanvasState(true);
            VisibilityState(true);
        }

        public virtual void Hide()
        {
            _instant = false;
            
            VisibilityState(false);
        }

        public void Hide(float delay)
        {
            _instant = false;
            
            VisibilityState(false, delay);
        }

        public virtual void Show(bool instant)
        {
            _instant = instant;
            
            SetCanvasState(true);
            VisibilityState(true);
        }

        public virtual void Hide(bool instant)
        {
            _instant = instant;
            
            VisibilityState(false);
        }

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected void DestroySelf()
        {
            try
            {
                if(gameObject!=null)
                    Destroy(gameObject);
            }
            catch (Exception e)
            {
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetCanvasState(bool isEnabled)
        {
            if (!_useCanvas)
                return;

            _canvas.enabled = isEnabled;
        }

        private void VisibilityState(bool show, float delay = 0)
        {
            float duration = _fadeTime;

            if (_instant)
                duration = 0;

            if (_targetCG == null)
                return;

            float value = show ? 1 : 0;

            _fadeTN.Kill();
            _fadeTN = _targetCG
                .DOFade(value, duration)
                .SetUpdate(_ignoreScaleTime)
                .SetDelay(delay)
                .OnComplete(() =>
                {
                    if (show)
                        return;
                    
                    HideEvent?.Invoke();
                    SetCanvasState(false);
                });

            _targetCG.interactable = show;
            _targetCG.blocksRaycasts = show;
        }

        private void GetCanvasGroup() =>
            TryGetComponent(out _targetCG);
        
    }
}