using System;
using LabraxStudio.Managers;
using UnityEngine;

namespace LabraxStudio.App
{
    public class ScreenManager : SharedManager<ScreenManager>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private int _baseWidth = 1080;

        [SerializeField]
        private int _baseHeight = 1920;

        [SerializeField]
        private int _currentWidth = 1080;

        [SerializeField]
        private int _currentHeight = 1920;

        // PROPERTIES: ----------------------------------------------------------------------------

        public int BaseWidth => _baseWidth;
        public int BaseHeight => _baseHeight;
        public int CurrentWidth => _currentWidth;
        public int CurrentHeight => _currentHeight;
        public bool IsPhone => _isPhone;
        public float WidthPercent => _widthPercent;
        public float HeightPercent => _heightPercent;
        public float BaseAspectRatio => _baseAspectRatio;
        public float CurrentAspectRatio => _currentAspectRatio;

        // FIELDS: -------------------------------------------------------------------

        private bool _isPhone = true;
        private float _baseAspectRatio = 1;
        private float _currentAspectRatio = 1;
        private float _widthPercent = 1;
        private float _heightPercent = 1;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize()
        {
#if UNITY_EDITOR
            _isPhone = true;
#elif (UNITY_ANDROID || UNITY_IOS)
            _isPhone = DeviceTypeChecker.GetDeviceType() == ENUM_Device_Type.Phone;
#endif

            if (_isPhone)
            {
                if (SystemInfo.deviceName.Contains("Galaxy Fold"))
                    _isPhone = false;
            }

            _currentWidth = Screen.width;
            _currentHeight = Screen.height;
            CalculatePercents();
            CalculateAspectRatio();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CalculatePercents()
        {
            _widthPercent = 1.0f * _currentWidth / _baseWidth;
            _widthPercent = (float) Math.Round(_widthPercent, 2);
            _heightPercent = 1.0f * _currentHeight / _baseHeight;
            _heightPercent = (float) Math.Round(_heightPercent, 2);
        }

        private void CalculateAspectRatio()
        {
            _baseAspectRatio = 1.0f * _baseWidth / _baseHeight;
            _currentAspectRatio = 1.0f * _currentWidth / _currentHeight;
        }
    }
}