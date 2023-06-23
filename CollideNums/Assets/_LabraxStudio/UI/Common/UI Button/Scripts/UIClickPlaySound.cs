using LabraxStudio.Sound;
using UnityEngine;

namespace LabraxStudio.UI.Common
{
    public class UIClickPlaySound : MonoBehaviour
    {
        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public void OnClick() =>
            GlobalSoundMediator.Instance.PlayUIClick();
    }
}
