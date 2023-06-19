using LabraxStudio.App;

namespace LabraxStudio.UI.GameScene.Boosters
{
    internal class LevelRestartBooster: IBooster
    {
        public void UseBooster()
        {
            GameManager.ReloadScene();
        }
    }
}