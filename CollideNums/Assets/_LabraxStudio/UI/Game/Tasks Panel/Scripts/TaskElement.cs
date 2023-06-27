using LabraxStudio.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.GameScene.Tasks
{
    public class TaskElement : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Image _tileIcon;
        
        [SerializeField]
        private Pulsation _tilePulsation;

        [SerializeField]
        private CorrectIcon _correctIcon;

        [SerializeField]
        private IncorrectIcon _incorrectIcon;

        [SerializeField]
        private TaskCounter _taskCounter;

        // FIELDS: -------------------------------------------------------------------

        private int _lastCount = -1;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy()
        {
            _taskCounter.OnDestroy();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetSprite(Sprite tileSprite)
        {
            _tileIcon.sprite = tileSprite;
            _lastCount = -1;
        }

        public void SetCount(int count)
        {
            if (_lastCount == count)
                return;

            bool isCorrect = count == 0;
            bool isIncorrect = count < 0;
            bool showCounter = count > 0;

            _taskCounter.SetState(showCounter);
            _taskCounter.UpdateCounter(count);

            if(_lastCount!=-1)
                _tilePulsation.StartPulse();
            
            if (isCorrect)
                _correctIcon.Show();
            else
                _correctIcon.Hide();

            if (isIncorrect)
                _incorrectIcon.Show();
            else
                _incorrectIcon.Hide();

            _lastCount = count;
        }

        public void SetIncorrectState()
        {
            _taskCounter.SetState(false);
            _correctIcon.Hide();
            _incorrectIcon.Show();
        }
    }
}