using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.Base
{
    public class CustomCanvasScaler : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField]
        private bool _revert;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void Start() =>
            UpdateCanvas();

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        [Button]
        private void UpdateCanvas()
        {
            int leftValue = _revert ? 1 : 0;
            int rightValue = _revert ? 0 : 1;

            float matchWidth = DeviceTypeChecker.GetDeviceType() == ENUM_Device_Type.Phone ? leftValue : rightValue;
            GetComponent<CanvasScaler>().matchWidthOrHeight = matchWidth;
        }
    }
}