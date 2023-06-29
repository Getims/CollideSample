using System.Collections.Generic;
using LabraxStudio.App.Services;
using LabraxStudio.Meta.Levels;
using LabraxStudio.Sound;
using LabraxStudio.UI.Common;
using LabraxStudio.UiAnimator;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.GameScene.Tasks
{
    public class TasksContainer : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private List<TaskItem> _taskItems = new List<TaskItem>();

        [SerializeField]
        private UIAnimator _animation;

        [SerializeField]
        private Image _background;

        [SerializeField]
        private Sprite _normalBackground;

        [SerializeField]
        private Sprite _badBackground;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(List<LevelTaskMeta> levelTasks)
        {
            _background.sprite = _normalBackground;
            List<Sprite> tasksSprites =
                ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSprites.TasksSprites;

            int tasksCount = levelTasks.Count;
            for (int i = 0; i < tasksCount; i++)
            {
                int tileNumber = levelTasks[i].TileNumber;
                _taskItems[i].Setup(tileNumber, levelTasks[i].TilesCount, GetSprite(tileNumber, tasksSprites));
            }
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void AddTaskProgress(int tileNumber)
        {
            TaskItem taskItem = _taskItems.Find(ti => ti.TileNumber == tileNumber);
            if (taskItem == null)
                return;

            int currentCount = taskItem.CurrentCount;
            currentCount--;
            taskItem.SetCurrentCount(currentCount);
            if (taskItem.CurrentCount == 0)
            {
                _animation.Play();
                UISoundMediator.Instance.PlayTaskCompleteSFX();
            }
        }

        public void CheckForIncorrectState()
        {
            bool switchBack = false;
            foreach (var taskItem in _taskItems)
            {
                if (taskItem.CurrentCount != 0)
                {
                    taskItem.SetIncorrectState();
                    switchBack = true;
                }
            }

            if (switchBack)
            {
                UISoundMediator.Instance.PlayTaskFailSFX();
                _background.sprite = _badBackground;
            }
        }
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private Sprite GetSprite(int tileNumber, List<Sprite> tasksSprites)
        {
            if (tileNumber - 1 >= tasksSprites.Count)
                return ServicesProvider.GameSettingsService.GetGameSettings().GameFieldSprites.ErrorSprite;

            return tasksSprites[tileNumber - 1];
        }
    }
}