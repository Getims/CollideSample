using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace LabraxStudio.Game.Tiles
{
    public class TilesAnimator
    {
        public TilesAnimator()
        {
            //_coroutineRunner = TilesController.Instance.gameObject;
        }

        // FIELDS: -------------------------------------------------------------------

        private GameObject _coroutineRunner;
        private bool _isPaused = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public async void Play(List<AnimationAction> animations, Action onComplete = null)
        {
            foreach (var animation in animations)
            {
                if (animation == null)
                    continue;

                _isPaused = true;
                animation.Play(Unpause);
                while (_isPaused)
                    await Task.Delay(100);
            }
            
            if(onComplete != null)
                onComplete.Invoke();
        }
        
        public async void Play(AnimationAction animation, Action onComplete = null)
        {
            if (animation != null)
            {
                _isPaused = true;
                animation.Play(Unpause);
                while (_isPaused)
                    await Task.Delay(100); 
            }
            
            if(onComplete != null)
                onComplete.Invoke();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        private void Unpause()
        {
            _isPaused = false;
        }
    }
}