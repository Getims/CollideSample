using System.Collections;
using Coffee.UIExtensions;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Meta.Levels;
using LabraxStudio.Sound;
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

        [SerializeField]
        private UIParticle _confettiPS;

        [SerializeField]
        private Image _raycastBlocker;

        // FIELDS: -------------------------------------------------------------------

        private int _reward;
        private Coroutine _closeAnimationCO;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            _claimButton.onClick.AddListener(OnClaimClicked);
            Setup();
            Show();
        }

        protected override void OnDestroy()
        {
            _claimButton.onClick.RemoveListener(OnClaimClicked);
            CancelInvoke(nameof(AfterShow));
            if (_closeAnimationCO != null)
                StopCoroutine(_closeAnimationCO);
            base.OnDestroy();
        }

        public override void Show()
        {
            UISoundMediator.Instance.PlayVictoryMenuOpenedSFX();
            _raycastBlocker.enabled = false;
            base.Show();
            Invoke(nameof(AfterShow), FadeTime * 0.8f);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Setup()
        {
            LevelMeta levelMeta = ServicesProvider.LevelMetaService.GetCurrentLevelMeta();
            _reward = levelMeta.Reward;
            _rewardPanel.SetReward(_reward);
        }

        private void ApplyReward()
        {
            ServicesProvider.PlayerDataService.AddMoney(_reward);
            CommonEvents.SendAllCurrencyChanged();
        }

        private IEnumerator CloseAnimation()
        {
            UISoundMediator.Instance.PlayCoinsFlySFX();
            GameMediator.Instance.StartCoinsFlyAnimation(_rewardPanel.CoinCenter);
            yield return new WaitForSeconds(0.55f);
            CommonEvents.SendAllCurrencyChanged();
            yield return new WaitForSeconds(0.8f);
            //ServicesProvider.PlayerDataService.SwitchToNextLevel();
            UIEvents.SendWinScreenClaimClicked();

            Hide();
            DestroySelfDelayed();
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnClaimClicked()
        {
            _raycastBlocker.enabled = true;
            if (_closeAnimationCO != null)
                StopCoroutine(_closeAnimationCO);

            _closeAnimationCO = StartCoroutine(CloseAnimation());
        }

        private void AfterShow()
        {
            ServicesProvider.PlayerDataService.AddMoney(_reward);
            ServicesProvider.PlayerDataService.SwitchToNextLevel();
            _confettiPS?.Play();
        }
    }
}