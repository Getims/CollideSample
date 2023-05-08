using System;
using DG.Tweening;
using LabraxStudio.Events;
using LabraxStudio.Managers;
using LabraxStudio.UI;
using UnityEngine;

namespace LabraxStudio.MainMenu.Camera
{
    public class CameraController : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Header("References")]
        [SerializeField]
        private UnityEngine.Camera _camera;

        [SerializeField]
        private CameraMove _cameraMove;

        [SerializeField]
        private CameraZoom _cameraZoom;

        [SerializeField]
        private MovingElement _cameraMoveOutScript;

        [SerializeField]
        private RotateElement _cameraRotateScript;

        [SerializeField]
        private UIPanel _uiFog;

        [Header("Automove settings")]
        [SerializeField]
        private Vector3 _hexOffset = new Vector3(-5.5f, 0, -18);

        [SerializeField]
        private float _moveTime = 0.25f;

        [SerializeField]
        private Ease _moveEase = Ease.OutSine;

        // PROPERTIES: ----------------------------------------------------------------------------

        public UnityEngine.Camera Camera => _camera;
        public Vector3 HexOffset => _hexOffset;
        public CameraState State => _cameraState;

        // FIELDS: -------------------------------------------------------------------

        private Tweener _moveTW;
        private CameraState _cameraState = new CameraState();
        private Action _onMoveComplete;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            UIEvents.OnUISelect.AddListener(LockByUI);
            UIEvents.OnUIDeselect.AddListener(UnlockByUI);
        }

        private void OnDestroy()
        {
            _moveTW.Kill();

            UIEvents.OnUISelect.RemoveListener(LockByUI);
            UIEvents.OnUIDeselect.RemoveListener(UnlockByUI);
        }
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(bool lastSceneIsGame, Action onMoveComplete)
        {
            int openedLevels = LevelManager.UnlockedLevelsCount;
            _onMoveComplete = onMoveComplete;
            _cameraRotateScript.SetRotateEffector(openedLevels > 5 ? 1 : 0.5f);

            _cameraState.Setup(this);
            _cameraMove.Setup(this);
            _cameraZoom.Setup(this);

            float screenFactor = 1;

            if (ScreenManager.Instance.IsPhone)
            {
                screenFactor =
                    1 - (ScreenManager.Instance.CurrentAspectRatio - ScreenManager.Instance.BaseAspectRatio);

                _cameraZoom.SetSize(screenFactor);
                _cameraMoveOutScript.SetMoveEffector(screenFactor);
            }
            else
                _cameraZoom.SetSize(screenFactor);

            if (!lastSceneIsGame)
            {
                //Utils.ReworkPoint("Last scene not game");
                OnCameraMoveToMap();
                return;
            }

            //Utils.ReworkPoint("Prepare camera move");
            LockCamera();
            _cameraMoveOutScript.Move(0, true);
            _cameraRotateScript.Rotate(0, true);
            _uiFog.Show(true);


            Utils.PerformWithDelay(this, 0.75f, () =>
            {
                _cameraMoveOutScript.ResetState();
                _cameraRotateScript.ResetState();
                _uiFog.Hide();

                Invoke(nameof(OnCameraMoveToMap), _cameraMoveOutScript.MoveTime);
            });
        }

        public void MoveCameraToPosition(Transform currentHex, Transform lastHex, Vector3 offset)
        {
            var position = _camera.transform.position;
            if (lastHex != null)
            {
                var tempPosition = lastHex.position + _hexOffset;
                tempPosition.y = position.y;
                position = tempPosition;
            }

            var newPosition = currentHex.position + offset;
            newPosition += _hexOffset;
            newPosition.y = position.y;
            _cameraState.SetMoveTrigger(false);

            if (newPosition.x > position.x)
            {
                if (Math.Abs(position.x - newPosition.x) < 5.5)
                    newPosition.x = position.x + (newPosition.x - position.x) * 0.55f;
            }
            else if (newPosition.x < position.x)
            {
                if (Math.Abs(position.x - newPosition.x) < 5.5)
                    newPosition.x = position.x - (position.x - newPosition.x) * 0.55f;
            }

            if (newPosition.z > position.z)
            {
                if (Math.Abs(position.z - newPosition.z) < 8)
                    newPosition.z = position.z + (newPosition.z - position.z) * 0.55f;
            }
            else if (newPosition.z < position.z)
            {
                if (Math.Abs(position.z - newPosition.z) < 8)
                    newPosition.z = position.z - (position.z - newPosition.z) * 0.55f;
            }

            _moveTW.Kill();
            _moveTW = _camera.transform.DOMove(newPosition, _moveTime)
                .SetEase(_moveEase)
                .OnComplete(() => { _cameraState.SetMoveTrigger(true); });
        }

        public void MoveCameraToPosition(Transform currentHex, Vector2 offset)
        {
            var position = _camera.transform.position;
            var newPosition = currentHex.position + _hexOffset;
            newPosition.x += offset.x;
            newPosition.y = position.y;
            newPosition.z += offset.y;

            _cameraState.SetMoveTrigger(false);

            _moveTW.Kill();
            _moveTW = _camera.transform.DOMove(newPosition, _moveTime)
                .SetEase(_moveEase)
                .OnComplete(() => { _cameraState.SetMoveTrigger(true); });
        }

        public void OnStateUpdate()
        {
            _cameraMove.CheckState();
            _cameraZoom.CheckState();
        }

        public void SetUITrigger(bool isCoveredByUI) =>
            _cameraState.SetUITrigger(isCoveredByUI);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void MoveOutOfScreen()
        {
            LockCamera();
            _cameraMoveOutScript.Move();
            _cameraRotateScript.Rotate();
            _uiFog.Show();
        }

        private void LockCamera() =>
            _cameraState.SetLockTrigger(true);

        private void UnlockCamera() =>
            _cameraState.SetLockTrigger(false);

        private void LockByUI() => SetUITrigger(true);

        private void UnlockByUI() => SetUITrigger(false);

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public void OnCameraMoveToMap()
        {
            UnlockCamera();
            if (_onMoveComplete != null)
                _onMoveComplete.Invoke();
        }

        private void OnPlayButtonClicked() =>
            MoveOutOfScreen();
    }
}