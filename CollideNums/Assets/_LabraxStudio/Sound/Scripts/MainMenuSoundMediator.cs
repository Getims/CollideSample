using LabraxStudio.Managers;

namespace LabraxStudio.Sound
{
    public class MainMenuSoundMediator : SharedManager<MainMenuSoundMediator>
    {
        // FIELDS: --------------------------------------------------------------------------------

        private SoundManager _soundManager;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() =>
            _soundManager = SoundManager.Instance;

        // PUBLIC METHODS: ------------------------------------------------------------------------
    }
}