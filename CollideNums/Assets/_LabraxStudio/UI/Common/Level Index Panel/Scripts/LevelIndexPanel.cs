using LabraxStudio.App.Services;
using LabraxStudio.Events;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace LabraxStudio.UI.Common
{
    public class LevelIndexPanel : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private TextMeshProUGUI _levelIndexTMP;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();

            GameEvents.OnGenerateLevel.AddListener(OnGenerateLevel);
            GameEvents.OnGameOver.AddListener(OnGameOver);
        }

        private void Start() =>
            UpdateInfo();

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameEvents.OnGenerateLevel.RemoveListener(OnGenerateLevel);
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void UpdateInfo()
        {
            int currentLevel = ServicesAccess.PlayerDataService.CurrentLevel;
            _levelIndexTMP.text = $"Level {currentLevel + 1}";
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------
        
        private void OnGenerateLevel()
        {
            UpdateInfo();
            Show();
        }
        
        private void OnGameOver(bool arg0)
        {
            Hide();
        }
    }
}