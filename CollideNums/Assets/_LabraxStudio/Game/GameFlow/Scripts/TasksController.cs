using System.Collections.Generic;
using LabraxStudio.App.Services;
using LabraxStudio.Events;
using LabraxStudio.Meta.Levels;
using LabraxStudio.Sound;

namespace LabraxStudio.Game
{
    public class TasksController
    {
        public TasksController()
        {
            GameEvents.OnMoveTileInGate.AddListener(OnTileMovedToGate);
        }

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool HasTasks => _tasksProgress.Count > 0;
        public bool IsAllTasksComplete => CheckTasksComplete();

        // FIELDS: -------------------------------------------------------------------

        private TaskSettings _taskSettings;

        private List<int> _tasksProgress;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Initialize(TaskSettings taskSettings)
        {
            _taskSettings = taskSettings;
            _tasksProgress = new List<int>();

            int tasksCount = _taskSettings.LevelTasks.Count;
            for (int i = 0; i < tasksCount; i++)
                _tasksProgress.Add(0);
        }

        public void OnDestroy()
        {
            GameEvents.OnMoveTileInGate.RemoveListener(OnTileMovedToGate);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private bool CheckTaskProgress(int tileNumber)
        {
            int taskIndex = -1;
            LevelTaskMeta taskMeta = FindTask(tileNumber, ref taskIndex);

            if (taskIndex == -1)
                return false;

            if (_tasksProgress[taskIndex] == taskMeta.TilesCount)
                return false;

            _tasksProgress[taskIndex] += 1;
            SendTaskProgress(tileNumber);

            if (_tasksProgress[taskIndex] == taskMeta.TilesCount)
                SendTaskComplete(tileNumber);

            if (IsAllTasksComplete)
            {
                GameEvents.SendAllTasksComplete();
                ServicesProvider.GameFlowService.GameOverTracker.CheckForWin();
            }

            return true;
        }

        private void SendTaskProgress(int tileNumber)
        {
            GameEvents.SendLevelTaskProgress(tileNumber);
        }

        private void SendTaskComplete(int tileNumber)
        {
            GameEvents.SendLevelTaskComplete(tileNumber);
        }

        private LevelTaskMeta FindTask(int tileNumber, ref int taskIndex)
        {
            int i = -1;
            foreach (var levelTask in _taskSettings.LevelTasks)
            {
                i++;
                if (levelTask.TileNumber != tileNumber)
                    continue;

                taskIndex = i;
                return levelTask;
            }

            return null;
        }

        private bool CheckTasksComplete()
        {
            int i = -1;
            foreach (var levelTask in _taskSettings.LevelTasks)
            {
                i++;
                if (levelTask.TilesCount != _tasksProgress[i])
                    return false;
            }

            return true;
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public void OnTileMovedToGate(int tileNumber)
        {
            bool hasTaskProgress = CheckTaskProgress(tileNumber);

            if (hasTaskProgress)
                GameSoundMediator.Instance.PlayTaskGatePassSFX();
            else
                GameSoundMediator.Instance.PlayTilesGatePassSFX();
        }
    }
}