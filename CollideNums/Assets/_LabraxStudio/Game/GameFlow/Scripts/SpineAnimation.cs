using Spine.Unity;
using UnityEngine;

namespace LabraxStudio.Game
{
    public class SpineAnimation: MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField]
        protected SkeletonAnimation _skeleton;
        
        [SerializeField]
        protected bool _isLooped = false;
       
        [SerializeField]
        protected string _mainAnimationName;

        [SerializeField]
        protected string _idleAnimationName;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public virtual void PlayAnimation()
        {
            _skeleton.AnimationState.SetAnimation(0, _mainAnimationName, _isLooped);
        }

        public virtual void StopAnimation()
        {
            _skeleton.AnimationState.SetAnimation(0, _idleAnimationName, false);
        }
    }
}