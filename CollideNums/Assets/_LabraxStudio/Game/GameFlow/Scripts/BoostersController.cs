namespace LabraxStudio.Game
{
    public class BoostersController
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsBoosterActive => _isBoosterActive;

        // FIELDS: -------------------------------------------------------------------

        private bool _isBoosterActive = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetBoosterState(bool isActive)
        {
            _isBoosterActive = isActive;
        }
    }
}