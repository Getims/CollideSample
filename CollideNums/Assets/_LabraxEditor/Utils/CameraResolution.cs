using System.Collections;
using System.Collections.Generic;
using LabraxStudio.Base;
using UnityEngine;

namespace LabraxStudio
{
    public class CameraResolution : MonoBehaviour
    {
        [SerializeField] private float _hFOV = 36;
        [SerializeField] private float _tabletHFOV = 36;
        [SerializeField] List<Camera> _cameras;

        void Start()
        {
            FixFov();
        }

        [Sirenix.OdinInspector.Button]
        public void FixFov()
        {
            bool isPhone = DeviceTypeChecker.GetDeviceType() == ENUM_Device_Type.Phone;

            foreach (var cam in _cameras)
            {
                var hFOVrad = (isPhone ? _hFOV : _tabletHFOV) * Mathf.Deg2Rad;
                var camH = Mathf.Tan(hFOVrad * .5f) / cam.aspect;
                var vFOVrad = Mathf.Atan(camH) * 2;
                cam.fieldOfView = vFOVrad * Mathf.Rad2Deg;
            }
        }
    }
}
