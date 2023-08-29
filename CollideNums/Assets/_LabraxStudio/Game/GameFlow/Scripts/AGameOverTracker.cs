namespace LabraxStudio.Game
{
    public abstract class AGameOverTracker
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsFail = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public abstract void CheckForFail();
        public abstract void CheckForWin();
        public abstract void ResetFailFlag();
    }
}