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
        private TutorialTextController _tutorialTextController;

        [SerializeField]
        private TutorialHand _tutorialHand;
        
        // FIELDS: -------------------------------------------------------------------

        private readonly TutorialController _tutorialController = new TutorialController();
        private bool _hasTutorial;
        private LevelRules _currentRules;
        
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
            _tutorialController.OnDestroy();
            base.OnDestroy();
            GameEvents.OnGenerateLevel.RemoveListener(OnLevelGenerate);
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
        }

        private void Start()
        {
            if (GameFlowManager.IsLevelGenerated)
                OnLevelGenerate();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void Show()
        {
            base.Show();
            StartTutorial();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private bool SetupTutorial()
        {
            LevelsListMeta levelsListMeta = ServicesProvider.LevelMetaService.SelectedLevelsList;
            TutorialSettingsMeta tutorialSettings = levelsListMeta.TutorialSettingsMeta;
            ServicesProvider.TutorialService.Initialize(null);
            
            if (tutorialSettings == null)
                return false;
            
            int currentLevel = ServicesProvider.PlayerDataService.CurrentLevel;
            _currentRules = tutorialSettings.GetRules(currentLevel);

            if (_currentRules == null || _currentRules.RulesCount == 0)
                return false;

            SetupTitle();
            
            _tutorialController.Initialize(_currentRules, _tutorialTextController, _tutorialHand, OnTutorialComplete);
            ServicesProvider.TutorialService.Initialize(_tutorialController);
            
            return true;
        }

        private void SetupTitle()
        {
            _tutorialTextController.SetupTitle(_currentRules);
        }

        private void StartTutorial() => _tutorialController.StartTutorial();
        
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

        private void OnTutorialComplete()
        {
            Hide();
            //DestroySelfDelayed();
        }

    }
}