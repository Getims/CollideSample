using DG.Tweening;
using LabraxStudio.UI;
using UnityEngine;

namespace LabraxStudio.Creatives
{
    public class TrackingHand : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private float _scalePower = 0.1f;
        [SerializeField] private float _punchDuration = 0.15f;
        [SerializeField] private UIPanel _uiPanel;

        // GAME ENGINE METHODS: -------------------------------------------------------------------
        
        private void Update()
        {
            transform.position = Input.mousePosition;
            if(Input.GetMouseButtonDown(0))
                OnClick();
            if (Input.GetMouseButtonDown(1))
                SwitchHandState();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void OnClick()
        {
            transform.DOPunchScale(-Vector3.one * _scalePower, _punchDuration, 0, 0);
        }

        private void SwitchHandState()
        {
            _uiPanel.Hide();
        }
    }
}
