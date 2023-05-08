﻿using System.Text;
using UnityEngine;

namespace LabraxStudio
{
    public static class Log
    {
        private const string StatColor = "<color=#FF6B37>";
        private const string ValuesColor = "<color=#60FF79>";
        private const string AttributeColor = "<color=#3BAAFF>";

        private const string RedColor = "<color=#FF3243>";
        private const string GreenColor = "<color=#60FF79>";
        private const string CyanColor = "<color=#3BAAFF>";
        private const string OrangeColor = "<color=#FF6B37>";

        private const string RedFat = "<r><b>";
        private const string RedFatEndPrefix = "</b></>";
        private const string GreenFat = "<g><b>";
        private const string GreenFatEndPrefix = "</b></>";
        private const string CyanFat = "<c><b>";
        private const string CyanFatEndPrefix = "</b></>";
        private const string OrangeFat = "<o><b>";
        private const string OrangeFatEndPrefix = "</b></>";

        private const string ColorEndPrefix = "</color>";

        public static string HandleLog(string log)
        {
            HandleMessage(log, out string logResult);
            return AddWhite(logResult);
        }

        public static string HandleLog(string tag, string log)
        {
            string message = $"[<st><b>{tag}</b></>] {log}";
            HandleMessage(message, out string logResult);
            return AddWhite(logResult);
        }

        public static void Print(string log)
        {
            HandleMessage(log, out string text);
            PrintMessage(text);
        }

        public static void Error(string log)
        {
            HandleMessage(log, out string text);
            PrintMessage(text, true);
        }

        public static void Error(string log, Object context)
        {
            HandleMessage(log, out string text);
            PrintMessage(text, context, true);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static void HandleMessage(string log, out string result)
        {
            StringBuilder text = new(log);

            text.Replace("<rb>", RedFat);
            text.Replace("</rb>", RedFatEndPrefix);
            text.Replace("<gb>", GreenFat);
            text.Replace("</gb>", GreenFatEndPrefix);
            text.Replace("<cb>", CyanFat);
            text.Replace("</cb>", CyanFatEndPrefix);
            text.Replace("<ob>", OrangeFat);
            text.Replace("</ob>", OrangeFatEndPrefix);

            text.Replace("<r>", RedColor);
            text.Replace("<g>", GreenColor);
            text.Replace("<c>", CyanColor);
            text.Replace("<o>", OrangeColor);

            text.Replace("<st>", StatColor);
            text.Replace("<val>", ValuesColor);
            text.Replace("<att>", AttributeColor);

            text.Replace("</>", ColorEndPrefix);
            text.Replace("</r>", ColorEndPrefix);
            text.Replace("</g>", ColorEndPrefix);
            text.Replace("</c>", ColorEndPrefix);
            text.Replace("</o>", ColorEndPrefix);
            result = text.ToString();
        }

        private static string AddWhite(string log)
        {
            return $"<color=white>{log}</color>";
        }
        
        private static void PrintMessage(string text, bool isError = false)
        {
            if (isError)
                Debug.LogError($"<color=white>{text}</color>");
            else
                Debug.Log($"<color=white>{text}</color>");
        }

        private static void PrintMessage(string text, Object context, bool isError = false)
        {
            if (isError)
                Debug.LogError($"<color=white>{text}</color>", context);
            else
                Debug.Log($"<color=white>{text}</color>", context);
        }
    }
}