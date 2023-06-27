using System.Collections.Generic;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Game;
using LabraxStudio.Meta.Levels;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Tasks
{
    public class TasksPanel : UIPanel
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private List<TasksContainer> _tasksContainers = new List<TasksContainer>();

        // FIELDS: -------------------------------------------------------------------

        private TasksContainer _currentContainer;
        private bool _hasTasks = false;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            GameEvents.OnGenerateLevel.AddListener(OnLevelGenerate);
            GameEvents.OnGameOver.AddListener(OnGameOver);
            GameEvents.OnGameFail.AddListener(OnGameFail);
            GameEvents.OnLevelTaskProgress.AddListener(OnLevelTaskProgress);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameEvents.OnGenerateLevel.RemoveListener(OnLevelGenerate);
            GameEvents.OnGameOver.RemoveListener(OnGameOver);
            GameEvents.OnLevelTaskProgress.RemoveListener(OnLevelTaskProgress);
        }

        private void Start()
        {
            if (GameFlowManager.IsLevelGenerated)
                OnLevelGenerate();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private bool SetupTasks()
        {
            LevelMeta currentLevel = ServicesProvider.LevelMetaService.GetCurrentLevelMeta();
            TaskSettings taskSettings = currentLevel.TaskSettings;

            if (taskSettings == null)
                return false;

            int tasksCount = taskSettings.LevelTasks.Count;
            _currentContainer = null;
            SelectTaskContainer(tasksCount);

            if (_currentContainer == null)
                return false;

            _currentContainer.SetActive(true);
            _currentContainer.Setup(taskSettings.LevelTasks);
            return true;
        }

        private void SelectTaskContainer(int tasksCount)
        {
            for (int i = 0; i < _tasksContainers.Count; i++)
            {
                if (i + 1 != tasksCount)
                    _tasksContainers[i].SetActive(false);
                else
                    _currentContainer = _tasksContainers[i];
            }
        }

        private void OnLevelTaskProgress(int tileNumber)
        {
            if (!_hasTasks)
                return;

            _currentContainer.AddTaskProgress(tileNumber);
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
            if (!_hasTasks)
                return;

            if (failReason != FailReason.NotCompleteAllTasks)
                return;

            _currentContainer.CheckForIncorrectState();
        }

        private void OnLevelGenerate()
        {
            _hasTasks = SetupTasks();
            if (_hasTasks)
                Show();
            else
                Hide();
        }
    }
}