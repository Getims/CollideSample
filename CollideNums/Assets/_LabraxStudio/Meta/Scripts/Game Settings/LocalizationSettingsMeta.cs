using I2.Loc;
using LabraxEditor;

namespace LabraxStudio.Meta
{
    public class LocalizationSettingsMeta : ScriptableSettings,  ILanguageSource
    {
        public  LanguageSourceData SourceData
        {
            get { return mSource; }
            set { mSource = value; }
        }

        public LanguageSourceData mSource = new LanguageSourceData();
    }
}
