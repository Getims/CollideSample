namespace LabraxStudio.AnalyticsIntegration
{
    internal static class LabraxAnalyticsConstants
    {
        // Events names
        internal const string START_GAME_EVENT_NAME = "Game Start";
        internal const string FINISH_GAME_EVENT_NAME = "Game End";

        internal const string LEVEL_START_EVENT_NAME = "Level Start";
        internal const string LEVEL_RESTART_EVENT_NAME = "Level Restart";
        internal const string LEVEL_COMPLETE_EVENT_NAME = "Level Complete";

        internal const string BOOSTER_USE_EVENT_NAME = "Booster Use";
        internal const string BUY_BOOSTER_EVENT_NAME = "Booster Buy ";

        internal const string REWARDED_VIDEO_EVENT_NAME = "Rewarded Video";
        internal const string INTERSTITIAL_EVENT_NAME = "Interstitial Ad";
        internal const string IN_APP_PURCHASE_EVENT_NAME = "In-App Purchase";

        // Properties

        internal const string LEVEL_NUMBER_PROPERTY = "levelNumber";
        internal const string SESSION_DURATION_PROPERTY = "sessionDuration";
        internal const string LEVEL_PLAY_TIME_PROPERTY = "playTime";
        internal const string BOOSTER_NAME_PROPERTY = "boosterName";

        internal const string REWARD_TYPE_PROPERTY = "rewardType";
        internal const string INTERSTITIAL_PLACEMENT_PROPERTY = "placement";
        internal const string CASH_POINTS_PROPERTY = "cashPoints";

        // Values

        internal const string AB_NONE_VALUE = "none";
        internal const string CONFIG_SOURCE_BUILD_VALUE = "build";
        internal const string CONFIG_SOURCE_REMOTE_VALUE = "remote";

        //Super

        internal const string AB_TEST_PROPERTY = "abTest";
        internal const string AB_CONFIG_PROPERTY = "abConfig";
        internal const string CONFIG_SOURCE_PROPERTY = "sourceConfig";
        internal const string SESSION_NUMBER_PROPERTY = "sessionNumber";
        internal const string СASH_POINTS_TOTAL_PROPERTY = "cashPointsTotal";
        internal const string COINS_COUNT_PROPERTY = "coinsCount";
        internal const string DEVICE_UNIQUE_IDENTIFIER = "Device Uniquer Identifier";
    }
}