using System;
using LabraxStudio.Game;
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
        private CanvasGroup _currencyContainer;

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

        public void SetState(BoosterCost costType)
        {
            _adIcon.enabled = costType == BoosterCost.RV;
            _currencyContainer.gameObject.SetActive(costType == BoosterCost.Money);
        }

        public void SetInteractable(bool isInteractable)
        {
            Color newColor = Color.white;
            newColor.a = isInteractable ? 1 : 0.55f;
            _boosterIcon.color = newColor;
            _currencyContainer.alpha = isInteractable ? 1 : 0.55f;
        }
    }
}