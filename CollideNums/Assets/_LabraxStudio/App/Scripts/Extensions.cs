using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace LabraxStudio
{
    public static class Extensions
    {
        public static void VisibilityState(this GameObject gameObject, bool show) =>
            gameObject.SetActive(show);
        
        public static string GetNiceName(this string text) =>
            Regex.Replace(text, "([a-z0-9])([A-Z0-9])", "$1 $2");

        public static void ConvertToMinutes(this float time, out string result)
        {
            time = Mathf.Max(time, 0);
            
            int min = Mathf.FloorToInt(time % 3600 / 60f);
            int sec = Mathf.FloorToInt(time % 60);
            
            result = $"{min:D2}:{sec:D2}";
        }

        public static void ConvertToHours(this float time, out string result)
        {
            time = Mathf.Max(time, 0);
            
            int hours = Mathf.FloorToInt(time / 3600f);
            int min = Mathf.FloorToInt(time % 3600 / 60f);
            int sec = Mathf.FloorToInt(time % 60);
            
            result = $"{hours:D2}:{min:D2}:{sec:D2}";
        }

        public static void ConvertToDateTimeNow(this int time, out string result)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);
            DateTime dateTime = dateTimeOffset.LocalDateTime;
            result = dateTime.ToString();
        }
        
        public static void ConvertToDateTimeUtc(this int time, out string result)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);
            DateTime dateTime = dateTimeOffset.UtcDateTime;
            result = dateTime.ToString();
        }
    }
}