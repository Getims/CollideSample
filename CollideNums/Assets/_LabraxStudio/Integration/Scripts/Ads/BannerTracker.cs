using LabraxStudio.Events;

namespace LabraxStudio.UI
{
    public class BannerTracker
    {
        // FIELDS: -------------------------------------------------------------------
        private bool _isShowing = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------
        public void ShowBanner(float height = 0)
        {
            _isShowing = true;
            UIEvents.SendShowBanner(height);
        }

        public void HideBanner()
        {
            _isShowing = false;
            UIEvents.SendHideBanner();
        }

        public void CheckState()
        {
            if (_isShowing)
                UIEvents.SendShowBanner(0);
            else
                UIEvents.SendHideBanner();
        }
    }
}