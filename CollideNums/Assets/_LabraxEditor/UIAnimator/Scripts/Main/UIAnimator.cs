using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UiAnimator
{
    public class UIAnimator : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
#if UNITY_EDITOR
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "@ElementName", Expanded = true)]
        [OnStateUpdate(nameof(UpdateTime))]
#endif
        List<AnimationGroup> _animations = new List<AnimationGroup>();

        [SerializeField]
        private bool _isLooped = false;

        // FIELDS: -------------------------------------------------------------------

        private Coroutine _playCO;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnDestroy()
        {
            StopPlay();
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        [Button]
        public void Play()
        {
            if (_playCO != null)
                StopCoroutine(_playCO);
            if (isActiveAndEnabled)
                _playCO = StartCoroutine(PlayCO());
        }

        [Button]
        public void Stop()
        {
            StopPlay();
        }

        public float GetAnimatorWorkTime()
        {
            float result = 0;
            foreach (var animationGroup in _animations)
            {
                if (animationGroup != null)
                    result = animationGroup.PlayEndTime;
            }

            return result;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private IEnumerator PlayCO()
        {
            float lastGroupTime = 0;
            foreach (var animationGroup in _animations)
            {
                yield return new WaitForSeconds(animationGroup.PlayDelay);
                animationGroup.Play();
                lastGroupTime = animationGroup.PlayEndTime - animationGroup.PlayDelay;
            }

            if (_isLooped)
            {
                yield return new WaitForSeconds(lastGroupTime);
                _playCO = StartCoroutine(PlayCO());
            }
        }

        private void StopPlay()
        {
            bool saveLoop = _isLooped;
            _isLooped = false;
            if (_playCO != null)
                StopCoroutine(_playCO);

            foreach (var animationGroup in _animations)
                animationGroup.Stop();
            _isLooped = saveLoop;
        }

        #region EDITOR_METHODS

#if UNITY_EDITOR
        private void UpdateTime()
        {
            float _startTime = 0;
            foreach (var animationGroup in _animations)
            {
                animationGroup.SetStartTime(_startTime);
                _startTime += animationGroup.PlayDelay;
            }
        }
#endif

        #endregion
    }
}