namespace LabraxStudio.Sound
{
    internal class GSound
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public GSound(string id, long time = 0)
        {
            _id = id;
            _lastPlayTime = time;
        }

        // PROPERTIES: ----------------------------------------------------------------------------

        public string Id => _id;
        public long LastPlayTime => _lastPlayTime;

        // FIELDS: --------------------------------------------------------------------------------

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