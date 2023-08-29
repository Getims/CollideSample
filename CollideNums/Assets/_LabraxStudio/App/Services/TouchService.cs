namespace LabraxStudio.App.Services
{
    public class TouchService
    {
        // PROPERTIES: ----------------------------------------------------------------------------
        
        public bool IsTouchEnabled => _isTouchEnabled;
        
        // FIELDS: -------------------------------------------------------------------
        
        private bool _isTouchEnabled = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetTouchState(bool enabled)
        {
            _isTouchEnabled = enabled;
        }
        
    }
}