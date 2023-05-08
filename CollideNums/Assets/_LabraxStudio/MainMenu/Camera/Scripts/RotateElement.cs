using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace LabraxStudio.MainMenu.Camera
{
    public class RotateElement : MonoBehaviour
    {
        [SerializeField] private int _id = 0;
        [SerializeField] private Transform _rotateElement;
        [SerializeField] private Vector3 _angle;
        [SerializeField] private float _rotateTime = 0.2f;
        [SerializeField] private Ease _rotateEase = Ease.InOutQuint;
        [SerializeField] private float _delay = 0;
        [SerializeField] private RotateMode _rotateMode = RotateMode.FastBeyond360;

        [Tooltip("If enable can't move until reset")]
        [SerializeField] private bool _checkState = true;
        
        private bool _isUp = false;
        private Vector3 _startRotation;
        private Tweener _rotateTW;
        private bool _isSetuped = false;
        private float _rotateEffector = 1;

        public int Id => _id;
        public float RotateTime => _rotateTime;
        public float Delay => _delay;

        private void Start()
        {
            if(_isSetuped)
                return;
            
            _startRotation = _rotateElement.localEulerAngles;
            _isSetuped = true;
        }

        public void SetRotateEffector(float value)
        {
            _rotateEffector = value;
        }
        
        public void Rotate(float delay = -1, bool instant = false)
        {
            if (!_isSetuped)
            {
                Start();
            }
            
            if (_checkState)
            {
                if (_isUp)
                    return;
            }

            var rotateDelay = _delay;
            if (delay >= 0)
                rotateDelay = delay;

            var time = _rotateTime;
            if (instant)
            {
                time = 0;
                rotateDelay = 0;
            }
            
            _rotateTW.Kill();

            var newAngle = _angle*_rotateEffector;
            _rotateTW = _rotateElement.DOLocalRotate(newAngle, time, _rotateMode)
                .SetEase(_rotateEase)
                .SetDelay(rotateDelay);
            _isUp = !_isUp;
        }

        public void ResetState(float delay = -1, bool instant = false)
        {
            if (!_isSetuped)
            {
                Start();
            }
            
            if (_checkState)
            {
                if (!_isUp)
                    return;
            }

            var rotateDelay = _delay;
            if (delay >= 0)
                rotateDelay = delay;
            
            var time = _rotateTime;
            if (instant)
            {
                time = 0;
                rotateDelay = 0;
            }

            _isUp = false;
            _rotateTW.Kill();

            var newAngle = _startRotation;
            if (_rotateMode == RotateMode.LocalAxisAdd)
                newAngle = -_angle*_rotateEffector;
            _rotateTW = _rotateElement.DOLocalRotate(newAngle, time, _rotateMode)
                .SetEase(_rotateEase)
                .SetDelay(rotateDelay);
        }

        private void OnDestroy()
        {
            _rotateTW.Kill();
        }
    }
}