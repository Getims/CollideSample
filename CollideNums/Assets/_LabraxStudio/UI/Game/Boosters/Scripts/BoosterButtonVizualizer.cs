using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.GameScene.Boosters
{
    [Serializable]
    public class BoosterButtonVisualizer
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Image _boosterIcon;
        
        [SerializeField]
        private GameObject _currencyContainer;

        [SerializeField]
        private Image _adIcon;
        
        [SerializeField]
        private TextMeshProUGUI _currencyTMP;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetIcon(Sprite icon)
        {
            _boosterIcon.sprite = icon;
        }
        
        public void SetCurrency(int count)
        {
            _currencyTMP.text = count.ToString();
        }

        public void SetState(bool isAdState)
        {
            _adIcon.enabled = isAdState;
            _currencyContainer.SetActive(!isAdState);
        }
    }
}