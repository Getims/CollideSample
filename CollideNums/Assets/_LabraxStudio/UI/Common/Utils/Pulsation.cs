using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UI.Common
{
    public class Pulsation : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Transform _element;

        [SerializeField]
        private float _pulseTime = 0.5f;

        [SerializeField]
        private float _pulsePower = 0.1f;

        [SerializeField]
        private bool _isLooped = false;

        [SerializeField]
        private Vector3 _startScale = Vector3.one;
        
        // PROPERTIES: ----------------------------------------------------------------------------
        
        public bool IsPulsing => _isPulsing;
        
        // FIELDS: -------------------------------------------------------------------

        private Tweener _pulseTW;
        private bool _isPulsing = false;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy()
        {
            if (_pulseTW != null)
                _pulseTW.Kill();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void StartDelayedPulse()
        {
            StopPulse();
            _isPulsing = true;
            _pulseTW = _element.DOPunchScale(Vector3.one * _pulsePower, _pulseTime, 1, 1)
                .SetDelay(0.1f)
                .OnComplete(
                    () =>
                    {
                        _isPulsing = false;
                        if (_isLooped)
                            StartPulse();
                    });
        }

        public void StartPulse()
        {
            StopPulse();
            _isPulsing = true;
            _pulseTW = _element.DOPunchScale(Vector3.one * _pulsePower, _pulseTime, 1, 1).OnComplete(
                () =>
                {
                    _isPulsing = false;
                    if (_isLooped)
                        StartPulse();
                });
        }

        public void StopPulse()
        {
            _isPulsing = false;
            if (_pulseTW != null)
                _pulseTW.Kill();
            _element.localScale = _startScale;
        }
    }
}
