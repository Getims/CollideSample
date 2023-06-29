using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game;
using LabraxStudio.Meta.Levels;
using LabraxStudio.Meta.Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    public class TutorialPanel : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Image _tutorialTitleText;
        
        // FIELDS: -------------------------------------------------------------------
        
        private bool _hasTutorial;
        private LevelRules _currentRules;
        private int _currentStep = 0;
        
        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            GameEvents.OnGenerateLevel.AddListener(OnLevelGenerate);
            GameEvents.OnGameOver.AddListener(OnGameOver);
            GameEvents.OnGameFail.AddListener(OnGameFail);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameEvents.OnGenerateLevel.RemoveListener(OnLevelGenerate);
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
        }

        private void Start()
        {
            if (GameFlowManager.IsLevelGenerated)
                OnLevelGenerate();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private bool SetupTutorial()
        {
            LevelsListMeta levelsListMeta = ServicesProvider.LevelMetaService.SelectedLevelsList;
            TutorialSettingsMeta tutorialSettings = levelsListMeta.TutorialSettingsMeta;
            
            if (tutorialSettings == null)
                return false;
            
            int currentLevel = ServicesProvider.PlayerDataService.CurrentLevel;
            _currentRules = tutorialSettings.GetRules(currentLevel);

            if (_currentRules == null || _currentRules.RulesCount == 0)
                return false;

            _currentStep = 0;
            SetupTitle();
            
            return true;
        }

        private void SetupTitle()
        {
            _tutorialTitleText.sprite = _currentRules.TutorialTitleSprite;
        }

        private void StartTutorial()
        {
            
        }
        
        // EVENTS RECEIVERS: ----------------------------------------------------------------------
        
        private void OnGameOver(bool isWin)
        {
            if (isWin)
            {
                Hide();
                DestroySelfDelayed();
            }
        }

        private void OnGameFail(FailReason failReason)
        {
            if (!_hasTutorial)
                return;

            /*
            if (failReason != FailReason.NotCompleteAllTasks)
                return;
                */
        }

        private void OnLevelGenerate()
        {
            _hasTutorial = SetupTutorial();
            if (_hasTutorial)
                Show();
            else
                Hide();
        }
    }
}