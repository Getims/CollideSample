using System.Collections.Generic;

namespace LabraxStudio.Game.Tiles
{
    public class TilesAnimator
    {
        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Play(List<AnimationAction> animations)
        {
            foreach (var animation in animations)
            {
                animation.Play();
            }
        }
    }
}