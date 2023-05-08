using UnityEngine;

namespace LabraxStudio.Base
{
    public enum ENUM_Device_Type
    {
        Tablet,
        Phone
    }

    public static class DeviceTypeChecker
    {
        public static bool isTablet;

        static float DeviceDiagonalSizeInInches()
        {
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;
            float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

            return diagonalInches;
        }

        public static ENUM_Device_Type GetDeviceType()
        {
#if UNITY_IOS
            float aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
            bool isTablet = (DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f);

            if (isTablet)
            {
                return ENUM_Device_Type.Tablet;
            }
            else
            {
                return ENUM_Device_Type.Phone;
            }
#elif UNITY_ANDROID

            float aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
            bool isTablet = (DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f);

            if (isTablet)
            {
                return ENUM_Device_Type.Tablet;
            }
            else
            {
                return ENUM_Device_Type.Phone;
            }
#endif
#pragma warning disable 0162
            return ENUM_Device_Type.Phone;
#pragma warning restore 0162
        }
    }
}