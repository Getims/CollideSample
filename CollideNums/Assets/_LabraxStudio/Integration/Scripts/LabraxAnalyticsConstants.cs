namespace LabraxStudio.AnalyticsIntegration
{
    internal static class LabraxAnalyticsConstants
    {
        internal const string StartGameEventName = "Game Start";
        internal const string FinishGameEventName = "Game End";
        internal const string LevelStartEventName = "Level Start";
        internal const string LevelRestarteEventName = "Level Restart";
        internal const string LevelCompleteEventName = "Level Complete";
        internal const string OneBoosterUseEventName = "Booster Use";
        internal const string Use50PercentsBoostersEventName = "Used 50 Percent Of Boosters";
        internal const string AllBoostersUsedEventName = "Used All Boosters";
        internal const string BuyBoosterEventName = "Booster Buy ";
        internal const string BuySkinEventName = "Skin Buy";
        internal const string UseSkinEventName = "Skin Use";
        internal const string ShopEnterEventName = "Shop Enter";
        internal const string ShopOpenTabEventName = "Shop Open Tab";
        internal const string RewardedVideoEventName = "Rewarded Video";
        internal const string InterstitialEventName = "Interstitial Ad";
        internal const string InAppPurchaseEventName = "In-App Purchase";
        internal const string SosWindowOpenEventName = "Donate Show";
        internal const string SpecialOfferOpenEventName = "Special Offer Open";
        internal const string CurrencyShopOpenFromSosWindowEventName = "Donate Open Shop";
        public static string SuperLevelStartEventName = "Super Level Start";
        public static string SuperLevelRestarteEventName = "Super Level Restart";
        public static string SuperLevelCompleteEventName = "SuperLevel Complete";

        internal const string LevelNumberProperty = "levelNumber";
        internal const string SessionTimeProperty = "sessionDuration";
        internal const string AttemptNumberProperty = "attemptNumber";
        internal const string StarsCountProperty = "starsCount";
        internal const string LevelPlayTimeProperty = "playTime";
        internal const string BoosterNameProperty = "boosterName";
        internal const string SkinsCountProperty = "skinsCount";
        internal const string SkinNameProperty = "skinName";
        internal const string EntersCountProperty = "entersCount";
        internal const string RewardTypeProperty = "rewardType";
        internal const string InterstitialPlacementProperty = "placement";
        internal const string CashPointsProperty = "cashPoints";
        internal const string ShopTabNameProperty = "tabName";
        internal const string InAppProductNameProperty = "productID";

        //Super
        internal const string AbTestPropertyKey = "Versions_ABtest";
        internal const string AbTestPropertyName = "abTest";
        internal const string AbConfigPropertyName = "abConfig";
        internal const string ConfigSourcePropertyName = "sourceConfig";
        internal const string SessionNumberProperty = "sessionNumber";
        internal const string СashPointsTotalProperty = "cashPointsTotal";
        internal const string СoinsCountProperty = "coinsCount";
        internal const string GemsCountProperty = "gemsCount";
        internal const string EnergyCountProperty = "energyCount";

        internal const string DeviceUniqueIdentifier = "Device Uniquer Identifier";

        internal const string AbTestConfigDefaultName = "none";
        internal const string AbTestConfigDefaultServerName = "Control";
        internal const string ConfigSourceBuild = "build";
        internal const string ConfigSourceRemote = "remote";
        internal const string AbTestConfig1Name = "Config1";
        internal const string AbTestConfig2Name = "Config2";
    }
}