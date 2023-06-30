using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using LabraxStudio.Events;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.GameScene.Tasks
{
    public class TaskWindow : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Button _backButton;
        
        [SerializeField]
        private Button _okButton;

        [SerializeField]
        private UIPanel _windowContainer;

        [SerializeField]
        private Transform _taskPanelContainer;

        [SerializeField]
        private Transform _taskPanelTarget;

        [SerializeField]
        private float _moveTime;

        [SerializeField]
        private List<Image> _shadows;

        // FIELDS: -------------------------------------------------------------------

        private Tweener _moveTweener;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            _okButton.onClick.AddListener(OnOkClicked);
            _backButton.onClick.AddListener(OnOkClicked);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _moveTweener?.Kill();
        }

        private void Start()
        {
            Show();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async void PlayHideAnimation()
        {
            _windowContainer.Hide();
            _okButton.interactable = false;
            _backButton.interactable = false;

            Vector3 taskPosition = _taskPanelTarget.position;
            _moveTweener = _taskPanelContainer.DOMove(taskPosition, _moveTime);

            foreach (var shadow in _shadows)
                shadow.enabled = false;

            int wait = (int) (_moveTime * 1000-50);
            await Task.Delay(wait);

            UIEvents.SendTaskWindowClosed();
            await Task.Delay(250);

            Hide(true);
            DestroySelfDelayed();
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnOkClicked() => PlayHideAnimation();
    }
}