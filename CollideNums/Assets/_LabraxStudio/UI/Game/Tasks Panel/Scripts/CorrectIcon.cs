using DG.Tweening;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Tasks
{
    public class CorrectIcon : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField]
        private float _startScale = 1.5f;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private float _scaleTime = 0.2f;

        // FIELDS: -------------------------------------------------------------------
        private Tweener _scaleTW;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _scaleTW?.Kill();
        }

        public override void Show()
        {
            base.Show();
            _container.localScale = Vector3.one * _startScale;
            _scaleTW?.Kill();
            _container.DOScale(Vector3.one, _scaleTime)
                .SetDelay(0.01f);
        }

        public override void Hide()
        {
            base.Hide();
            _scaleTW?.Kill();
        }
    }
}