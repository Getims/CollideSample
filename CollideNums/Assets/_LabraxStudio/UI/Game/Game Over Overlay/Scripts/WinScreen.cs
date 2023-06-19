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

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnClaimClicked()
        {
            ApplyReward();
            ServicesAccess.PlayerDataService.SwitchToNextLevel();
            UIEvents.SendWinScreenClaimClicked();
            
            Hide();
            DestroySelfDelayed();
        }
    }
}