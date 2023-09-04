namespace LabraxStudio.Game
{
    public abstract class AGameOverTracker
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsFail => _isFail;

        // FIELDS: -------------------------------------------------------------------
        
        protected bool _isFail = false;
        protected bool _isWin = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public abstract void CheckForFail();
        public abstract void CheckForWin();
        
        public void ResetFailFlag()
        {
            _isFail = false;
        }

        public void ResetWinFlag()
        {
            _isWin = false;
        }
    }
}