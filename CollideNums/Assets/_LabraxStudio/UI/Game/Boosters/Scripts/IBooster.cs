namespace LabraxStudio.UI.GameScene.Boosters
{
    internal interface IBooster
    {
        bool CanUseBooster();
        void UseBooster();
    }
}