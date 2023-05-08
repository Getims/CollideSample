using System;
using UnityEngine;

namespace LabraxStudio.UiAnimator
{
    [Serializable]
    public class AnimationAction : Animation
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] 
        ActionLinker _actionLinker;

        // FIELDS: -------------------------------------------------------------------
        
        private new const bool _hideAnimationValues = true;
        private Action _action;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public override void Play()
        {
            if (_actionLinker != null)
            {
                _action = _actionLinker.GetLink();
                Utils.PerformWithDelay(_actionLinker, _startDelay, StartAction);
            }
        }

        public override void Stop()
        {
            if (_actionLinker != null)
                _actionLinker.StopAllCoroutines();
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void StartAction()
        {
            _action.Invoke();
        }
    }
}