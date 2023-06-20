using LabraxStudio.Events;

namespace LabraxStudio.UI.GameScene.Boosters
{
    internal class LevelRestartBooster: IBooster
    {
        public void UseBooster()
        {
            GameEvents.SendLevelRestartBoosterUse();
        }
    }
}