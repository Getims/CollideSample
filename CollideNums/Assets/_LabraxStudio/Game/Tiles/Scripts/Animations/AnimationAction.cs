using System;

namespace LabraxStudio.Game.Tiles
{
    public abstract class AnimationAction
    {
        public abstract void Play(Action onComplete);
    }
}