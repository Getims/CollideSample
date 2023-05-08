using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;

namespace LabraxStudio.UI
{
    public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [Title(Settings)]
        [SerializeField]
        private bool _canThrowEvent;

        [SerializeField]
        private bool _useTransform;

        [SerializeField]
        private bool _checkButtonInteractable;

        [SerializeField, Required]
        [ShowIf(nameof(_checkButtonInteractable))]
        private Button _button;

        [SerializeField]
        private float _scaleTime = 0.15f;

        [SerializeField]
        private Vector2 _scale = new Vector2(0.85f, 0.85f);

        [SerializeField]
        private bool _useAdditionalTransforms;

        [SerializeField] 
        [Tooltip("Autoplay of animation on every click")]
        private bool _usePointersEvents = true;


        [Title("References")]
        [HideIf("_useTransform")]
        [InfoBox("Отсуствует ссылка на 'Rect Transform'.", InfoMessageType.Error, "@_rectTransform == null")]
        [SerializeField] private RectTransform _rectTransform;

        [ShowIf("_useTransform")]
        [InfoBox("Отсуствует ссылка на 'Transform'.", InfoMessageType.Error, "@_transform == null")]
        [SerializeField] private Transform _transform;

        [SerializeField]
        [ShowIf("_useAdditionalTransforms")]
        private List<Transform> _additionalTransforms;

        [ShowIf("_canThrowEvent")]
        [SerializeField] private float _eventDelay = 0.05f;

        [ShowIf("_canThrowEvent")]
        [SerializeField] private UnityEvent _onClickEvent;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public bool IsEnabled { get; set; } = true;

        // FIELDS: --------------------------------------------------------------------------------
        
        private const string Settings = "Settings";

        private Tweener _scaleTwnr;
        private Vector3 _startScale;
        private Vector3 _finalScale;
        private bool _isEventThrowing;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void Start() =>
            _startScale = _useTransform ? _transform.localScale : _rectTransform.localScale;

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void OnButtonPressed()
        {
            if (_canThrowEvent && !_isEventThrowing)
            {
                _isEventThrowing = true;
                Invoke(nameof(ThrowEvent), _eventDelay);
            }
        }

        public void PlayClickAnimation()
        {
            _scaleTwnr.Complete();
            _finalScale = _startScale * _scale;
            _finalScale.z = _finalScale.x;

            if (_useTransform)
                _scaleTwnr = _transform.DOScale(_finalScale, _scaleTime)
                    .SetUpdate(true)
                    .OnComplete(() =>
                        _scaleTwnr = _transform.DOScale(_startScale, _scaleTime)
                            .SetUpdate(true));
            else
                _scaleTwnr = _rectTransform.DOScale(_finalScale, _scaleTime)
                    .SetUpdate(true)
                    .OnComplete(() => 
                        _scaleTwnr = _rectTransform.DOScale(_startScale, _scaleTime)
                            .SetUpdate(true));
        }

        public void OnButtonDown()
        {
            if (!IsEnabled) return;

            _scaleTwnr.Complete();
            _finalScale = _startScale * _scale;
            _finalScale.z = _finalScale.x;

            if (_useTransform)
                _scaleTwnr = _transform.DOScale(_finalScale, _scaleTime).SetUpdate(true);
            else
                _scaleTwnr = _rectTransform.DOScale(_finalScale, _scaleTime).SetUpdate(true);
        }

        public void OnButtonUp()
        {
            if (!IsEnabled) return;

            _scaleTwnr.Complete();

            if (_useTransform)
                _scaleTwnr = _transform.DOScale(_startScale, _scaleTime).SetUpdate(true);
            else
                _scaleTwnr = _rectTransform.DOScale(_startScale, _scaleTime).SetUpdate(true);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void ThrowEvent()
        {
            _isEventThrowing = false;
            _onClickEvent.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_usePointersEvents)
                return;

            if (_checkButtonInteractable && !_button.interactable)
                return;
            
            OnButtonDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_usePointersEvents)
                return;
            
            if (_checkButtonInteractable && !_button.interactable)
                return;
            
            OnButtonUp();
        }
    }
}