using LabraxStudio.AnalyticsIntegration.Ads;
using LabraxStudio.AnalyticsIntegration.IAP;

namespace LabraxStudio.AnalyticsIntegration
{
    public static class AdsEnumsConverter
    {
        public static string RewardToGADesignName(AdReward reward)
        {
            switch (reward)
            {
                case AdReward.Booster:
                    return "GetBooster";
            }

            return string.Empty;
        }

        public static string IapProductNameToMixpanelName(ProductName productName)
        {
            switch (productName)
            {
                case ProductName.NoAds:
                    return "NoadsOnly";
            }

            return string.Empty;
        }
    }
}