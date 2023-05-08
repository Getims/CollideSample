using LabraxStudio.Sound;

namespace LabraxStudio.Managers
{
    public class MainMenuSoundMediator : SharedManager<MainMenuSoundMediator>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        
        // FIELDS: --------------------------------------------------------------------------------

        private SoundManager _soundManager;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() =>
            _soundManager = SoundManager.Instance;

        // PUBLIC METHODS: ------------------------------------------------------------------------

    }
}