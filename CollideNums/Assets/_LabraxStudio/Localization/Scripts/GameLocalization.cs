using I2.Loc;

namespace LabraxStudio.Localization
{
    public static class GameLocalization
    {
        // FIELDS: -------------------------------------------------------------------
        
        private static LanguageSourceData _source;

        // PUBLIC METHODS: -----------------------------------------------------------------------
        
        public static void Initialize(LanguageSource source) => _source = source.SourceData;
        public static void Initialize(LanguageSourceData source) => _source = source;

        public static string Translate(string term)
        {
            string translation = _source.GetTranslation(term);
            if(translation.Equals(string.Empty))
                translation = _source.GetTranslation(term, "English");
            return translation;
        }
    }
}
