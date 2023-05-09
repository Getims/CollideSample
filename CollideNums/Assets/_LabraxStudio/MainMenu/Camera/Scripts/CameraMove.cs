using System.Collections;
using Lean.Touch;
using UnityEngine;

namespace LabraxStudio.MainMenu.Camera
{
    public class CameraMove : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private LeanDragCamera _leanDragCamera;

        // FIELDS: -------------------------------------------------------------------

        private CameraController _cameraController;
        private bool _isSetuped = false;
        private Coroutine _sensCO;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(CameraController cameraController)
        {
            _cameraController = cameraController;
            CalculateMinMaxBorders();
            _isSetuped = true;
        }

        public void CheckState()
        {
            if (_cameraController.State.CanMove)
            {
                SetDefaultSensitivity();
                _leanDragCamera.enabled = true;
            }
            else
            {
                _leanDragCamera.enabled = false;
                SetBlockedSetDefaultSensitivity();
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void CalculateMinMaxBorders()
        {
        }

        private void SetBlockedSetDefaultSensitivity()
        {
            if (_sensCO != null)
                StopCoroutine(_sensCO);
            _leanDragCamera.Sensitivity = 0;
        }

        private void SetDefaultSensitivity()
        {
            if (_sensCO != null)
                StopCoroutine(_sensCO);

            _sensCO = StartCoroutine(EnableSensitivity());
        }

        private IEnumerator EnableSensitivity()
        {
            yield return new WaitForSeconds(0.1f);
            _leanDragCamera.Sensitivity = 1;
        }
    }
}