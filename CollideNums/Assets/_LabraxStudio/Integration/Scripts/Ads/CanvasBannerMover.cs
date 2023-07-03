using System.Collections.Generic;
using LabraxStudio.Events;
using UnityEngine;

namespace LabraxStudio.UI
{
    public class CanvasBannerMover : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField]
        private List<RectTransform> _canvases;

        [SerializeField, Range(0, 1)]
        private float _bannerOffsetPercent = 0.06f;

        [SerializeField, Range(0, 1)]
        private float _baseOffsetPercent;

        [SerializeField]
        private float _baseHeight = 1920;

        // FIELDS: --------------------------------------------------------------------------------

        private float _bannerOffset;
        private float _baseOffset;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            UIEvents.OnBannerShow.AddListener(OnBannerShow);
            UIEvents.OnBannerHide.AddListener(OnBannerHide);
            
            Setup();
        }

        private void OnDestroy()
        {
            UIEvents.OnBannerShow.RemoveListener(OnBannerShow);
            UIEvents.OnBannerHide.RemoveListener(OnBannerHide);
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void Setup()
        {
            _bannerOffset = _baseHeight * _bannerOffsetPercent; // * screenScale;
            _baseOffset = _baseHeight * _baseOffsetPercent; // * screenScale;
        }

        private void MoveCanvases(bool moveUp)
        {
            foreach (RectTransform canvas in _canvases)
                MoveCanvas(canvas, moveUp ? _bannerOffset : _baseOffset);
        }

        private void MoveCanvas(RectTransform rect, float distance)
        {
            if (rect == null)
                return;
                
            rect.SetBottom(distance);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------
        
        private void OnBannerShow(float height)
        {
            if (_bannerOffset < height)
                _bannerOffset = height;

            MoveCanvases(moveUp: true);
        }

        private void OnBannerHide() =>
            MoveCanvases(moveUp: false);
    }
}