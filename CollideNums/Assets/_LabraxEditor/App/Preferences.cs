using System;
using UnityEngine;
using LabraxStudio.Managers;

namespace LabraxStudio.App
{
    public class Preferences : SharedManager<Preferences>, IPreferences
    {
        public int GetInt(string _key)
        {
            int _value = PlayerPrefs.GetInt(_key, 0);
            return _value;
        }

        public void SetInt(string _key, int _value)
        {
            PlayerPrefs.SetInt(_key, _value);
        }

        public long GetLong(string _key)
        {
            string _valueStr = GetString(_key);
            long _value = 0;
            if (_valueStr != String.Empty)
            {
                _value = long.Parse(_valueStr);
            }

            return _value;
        }

        public void SetLong(string _key, long _value)
        {
            SetString(_key, _value.ToString());
        }

        public float GetFloat(string _key)
        {
            float _value = PlayerPrefs.GetFloat(_key, 0);
            return _value;
        }

        public void SetFloat(string _key, float _value)
        {
            PlayerPrefs.SetFloat(_key, _value);
        }

        public bool GetBool(string _key)
        {
            bool _value = PlayerPrefs.GetInt(_key, 0) == 1;
            return _value;
        }

        public void SetBool(string _key, bool _value)
        {
            PlayerPrefs.SetInt(_key, _value ? 1 : 0);
        }

        public string GetString(string _key)
        {
            string _value = PlayerPrefs.GetString(_key, "");
            return _value;
        }

        public void SetString(string _key, string _value)
        {
            PlayerPrefs.SetString(_key, _value);
        }

        public string LoadFileData(string _fileName)
        {
            /*
            string _fileData = FilesManager.Instance.ReadFile(_fileName);
            return _fileData;
            */
            return null;
        }

        public void SaveFileData(string _fileName, string _data)
        {
            /*
            FilesManager.Instance.SaveFile(_fileName, _data);
            */
        }

        public bool HasKey(string _key)
        {
            bool _hasKey = PlayerPrefs.HasKey(_key);
            return _hasKey;
        }

        public bool HasFile(string _fileName)
        {
            /*
            bool _hasFile = FilesManager.Instance.IsFileExists(_fileName);
            return _hasFile;
            */
            return false;
        }

        public void SaveData()
        {
            PlayerPrefs.Save();
        }

        internal void SetString(object mEDIA_MANAGER_KEY, string v)
        {
            throw new NotImplementedException();
        }

        public void RemoveKey(string _key)
        {
            PlayerPrefs.DeleteKey(_key);
        }

        public void ClearData()
        {
            // FilesManager.Instance.DeleteAllFiles();
            PlayerPrefs.DeleteAll();
        }
    }
}