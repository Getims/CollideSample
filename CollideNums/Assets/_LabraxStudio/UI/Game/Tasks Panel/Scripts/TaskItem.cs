using System;
using UnityEngine;

namespace LabraxStudio.UI.GameScene.Tasks
{
    [Serializable]
    internal class TaskItem
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField]
        private TaskElement _taskElement;

        // PROPERTIES: ----------------------------------------------------------------------------

        public int TileNumber => _tileNumber;
        public int CurrentCount => _currentCount;

        // FIELDS: -------------------------------------------------------------------

        private int _tileNumber = 0;
        private int _currentCount = 0;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Setup(int tileNumber, int needCount, Sprite tileIcon)
        {
            _tileNumber = tileNumber;
            _currentCount = needCount;
            _taskElement.SetSprite(tileIcon);
            _taskElement.SetCount(_currentCount);
        }

        public void SetCurrentCount(int count)
        {
            _currentCount = count;
            _taskElement.SetCount(_currentCount);
        }

        public void SetIncorrectState() => _taskElement.SetIncorrectState();
    }
}