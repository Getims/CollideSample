using System;
using LabraxStudio.Events;
using LabraxStudio.Game;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    [Serializable]
    public class BoosterTutorialHand
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private UIHandAnimation _handAnimation;

        // FIELDS: -------------------------------------------------------------------

        private BoosterType _boosterType = BoosterType.Null;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(BoosterType boosterType)
        {
            _boosterType = boosterType;
            UIEvents.OnNeedBoosterHand.AddListener(ShowBoosterHand);
        }

        public void OnDestroy()
        {
            RemoveListener();
        }

        public void OnBoosterClick()
        {
            RemoveListener();
            _handAnimation.StopAnimation();
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void RemoveListener() => UIEvents.OnNeedBoosterHand.RemoveListener(ShowBoosterHand);

        private void ShowBoosterHand(BoosterType boosterType)
        {
            if (_boosterType != boosterType)
                return;

            _handAnimation.StartAnimation();
        }
    }
}