namespace LabraxStudio.App
{
    public interface IPreferences
    {
        int GetInt(string _key);
        void SetInt(string _key, int _value);

        long GetLong(string _key);
        void SetLong(string _key, long _value);

        float GetFloat(string _key);
        void SetFloat(string _key, float _value);

        bool GetBool(string _key);
        void SetBool(string _key, bool _value);

        string GetString(string _key);
        void SetString(string _key, string _value);

        string LoadFileData(string _fileName);
        void SaveFileData(string _fileName, string _data);

        bool HasKey(string _key);
        bool HasFile(string _fileName);

        void SaveData();
        void RemoveKey(string _key);
        void ClearData();
    }
}