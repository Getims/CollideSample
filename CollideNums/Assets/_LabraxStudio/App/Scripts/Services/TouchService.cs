namespace LabraxStudio.App.Services
{
    public class TouchService
    {
        // PROPERTIES: ----------------------------------------------------------------------------
        
        public bool IsTouchEnebled => _isTouchEnebled;
        
        // FIELDS: -------------------------------------------------------------------
        
        private bool _isTouchEnebled = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetTouchState(bool enabled)
        {
            _isTouchEnebled = enabled;
        }
    }
}