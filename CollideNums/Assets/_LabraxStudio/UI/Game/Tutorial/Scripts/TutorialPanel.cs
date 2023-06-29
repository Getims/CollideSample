using LabraxStudio.Events;
using LabraxStudio.Game;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    public class TutorialPanel : UIPanel
    {
        // FIELDS: -------------------------------------------------------------------
        
        private bool _hasTutorial;
        
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
            return false;
            //throw new System.NotImplementedException();
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