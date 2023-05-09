using System.Collections.Generic;
using I2.Loc;
using LabraxStudio.Meta;

namespace LabraxStudio.Localization
{
    public static class MainMenuMap
    {
        // FIELDS: -------------------------------------------------------------------

        private static LanguageSourceData _source;
        private static List<LanguageSourceData> _sources = new List<LanguageSourceData>();

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public static void Initialize(LanguageSource source) => _source = source.SourceData;
        public static void Initialize(LanguageSourceData source) => _source = source;

        public static void Initialize(List<LocalizationSettingsMeta> localizationMetas)
        {
            _sources = new List<LanguageSourceData>();
            foreach (var localizationMeta in localizationMetas)
            {
                if (localizationMeta != null && localizationMeta.SourceData != null)
                    _sources.Add(localizationMeta.SourceData);
            }
        }

        public static string Translate(string term)
        {
            return GetTranslation(term);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        private static string GetTranslation(string term)
        {
            foreach (var source in _sources)
            {
                var translation = source.GetTranslation(term);

                if (translation.Equals(string.Empty))
                    translation = source.GetTranslation(term, "English");

                if (!translation.Equals(string.Empty))
                    return translation;
            }

            return string.Empty;
        }
    }
}