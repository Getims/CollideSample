using LabraxStudio.Base;

namespace LabraxStudio.Sound
{
    internal class GSound
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public GSound(GameplaySounds soundTyme, long time = 0)
        {
            _soundType = soundTyme;
            _lastPlayTime = time;
        }

        public GSound(string id, long time = 0)
        {
            _id = id;
            _lastPlayTime = time;
        }

        // PROPERTIES: ----------------------------------------------------------------------------

        public GameplaySounds SoundType => _soundType;
        public string Id => _id;
        public long LastPlayTime => _lastPlayTime;

        // FIELDS: --------------------------------------------------------------------------------

        private readonly GameplaySounds _soundType;
        private readonly string _id;
        private long _lastPlayTime;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetLatPlayTime(long time)
        {
            time = System.Math.Max(time, 0);
            _lastPlayTime = time;
        }
    }
}