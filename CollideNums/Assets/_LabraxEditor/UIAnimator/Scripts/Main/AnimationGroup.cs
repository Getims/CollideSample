using System.Collections.Generic;
using System.Globalization;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UiAnimator
{
    [System.Serializable]
    public class AnimationGroup
    {
        // MEMBERS: -------------------------------------------------------------------------------
        [SerializeField] 
        private string _groupName = "AnimationGroup";

        [SerializeField, HideInInspector] 
        private float _playStartTime = 0;

        [SerializeField, HideInInspector] 
        private float _playEndTime = 0;

        [SerializeField, Min(0)] 
        private float _playDelay = 0;

        [SerializeReference] 
#if UNITY_EDITOR
        [ListDrawerSettings(Expanded = true), OnStateUpdate(nameof(CalculateTime))]
#endif
        List<Animation> _animations = new List<Animation>();

        // PROPERTIES: ----------------------------------------------------------------------------

        public float PlayDelay => _playDelay;

        public float PlayEndTime => _playEndTime;

        
        // FIELDS: -------------------------------------------------------------------

        #region FIELDS

        private string ElementName =>
            $"'{_groupName}', playtime:  {_startTime.ToString("G",new CultureInfo("en-US"))}s - " +
            $"{_playEndTime.ToString("G",new CultureInfo("en-US"))}s";

        private float _startTime => _playStartTime + _playDelay;
        
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Play()
        {
            foreach (var animation in _animations)
                animation.Play();
        }

        public void Stop()
        {
            foreach (var animation in _animations)
                animation.Stop();
        }

        #endregion

        //-------------------------------------------------------------------------------

        #region EDITOR_METHODS

#if UNITY_EDITOR
        public void SetStartTime(float startTime)
        {
            _playStartTime = startTime;
        }

        private void CalculateTime()
        {
            float maxDelay = 0;
            float maxPlay = 0;

            foreach (var animation in _animations)
            {
                maxPlay = animation.StartDelay + animation.AnimationTime;
                if (maxPlay > maxDelay)
                    maxDelay = maxPlay;
            }

            _playEndTime = _playStartTime + _playDelay + maxDelay;
        }
#endif

        #endregion
    }
}