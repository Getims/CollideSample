using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.GameScene.GameOver
{
    public class RewardPanel : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private TextMeshProUGUI _rewardTMP;

        [SerializeField]
        private Image _coinIcon;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        [Button]
        public void SetReward(int reward)
        {
            _rewardTMP.text = reward.ToString();
            SetupPosition(reward);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetupPosition(int reward)
        {
            int offset = 0;
            if (reward < 10)
                offset = -35;
            else if (reward < 100)
                offset = -70;
            else
                offset = -110;

            var iconPosition = _coinIcon.rectTransform.anchoredPosition;
            iconPosition.x = offset;
            _coinIcon.rectTransform.anchoredPosition = iconPosition;
        }
    }
}