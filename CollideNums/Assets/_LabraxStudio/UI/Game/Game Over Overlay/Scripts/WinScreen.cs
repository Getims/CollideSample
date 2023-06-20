using System.Collections;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Meta;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.GameScene.GameOver
{
    public class WinScreen : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Button _claimButton;

        [SerializeField]
        private RewardPanel _rewardPanel;

        // FIELDS: -------------------------------------------------------------------

        private int _reward;
        private Coroutine _closeAnimationCO;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            _claimButton.onClick.AddListener(OnClaimClicked);
            Setup();
        }

        protected override void OnDestroy()
        {
            _claimButton.onClick.RemoveListener(OnClaimClicked);
            if (_closeAnimationCO != null)
                StopCoroutine(_closeAnimationCO);
            base.OnDestroy();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Setup()
        {
            LevelMeta levelMeta = ServicesAccess.LevelMetaService.GetCurrentLevelMeta();
            _reward = levelMeta.Reward;
            _rewardPanel.SetReward(_reward);
        }

        private void ApplyReward()
        {
            ServicesAccess.PlayerDataService.AddMoney(_reward);
            CommonEvents.SendAllCurrencyChanged();
        }

        private IEnumerator CloseAnimation()
        {
            GameMediator.Instance.StartCoinsFlyAnimation(_rewardPanel.CoinCenter);
            yield return new WaitForSeconds(0.5f);
            ApplyReward();
            ServicesAccess.PlayerDataService.SwitchToNextLevel();
            UIEvents.SendWinScreenClaimClicked();

            Hide();
            DestroySelfDelayed();
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnClaimClicked()
        {
            if (_closeAnimationCO != null)
                StopCoroutine(_closeAnimationCO);

            _closeAnimationCO = StartCoroutine(CloseAnimation());
            /*
            ApplyReward();
            ServicesAccess.PlayerDataService.SwitchToNextLevel();
            UIEvents.SendWinScreenClaimClicked();
            GameMediator.Instance.StartCoinsFlyAnimation(_rewardPanel.CoinCenter);

            Hide();
            DestroySelfDelayed();
            */
        }
    }
}