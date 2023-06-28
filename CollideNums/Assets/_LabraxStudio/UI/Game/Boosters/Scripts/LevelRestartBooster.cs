using LabraxStudio.Events;

namespace LabraxStudio.UI.GameScene.Boosters
{
    internal class LevelRestartBooster: IBooster
    {
        public bool CanUseBooster()
        {
            return true;
        }

        public void UseBooster()
        {
            GameEvents.SendLevelRestartBoosterUse();
        }
    }
}