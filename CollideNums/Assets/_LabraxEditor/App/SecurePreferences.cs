using System.Security.Cryptography;
using System.Text;
using LabraxStudio.Managers;
using UnityEngine;

namespace LabraxStudio.App
{
    public static class SecurePreferences
    {
        private const string LOG_KEY = "SecurePreferences";
        private static string _secretKey = "Spiderops";

        public static int GetInt(string _key)
        {
            int _value = PlayerPrefs.GetInt(Md5(_key));
            if (!CheckValue(_key, _value.ToString()))
                _value = 0;

            return _value;
        }

        public static void SetInt(string _key, int _value)
        {
            PlayerPrefs.SetInt(Md5(_key), _value);
            SaveMd5Value(_key, _value.ToString());
        }

        public static long GetLong(string _key)
        {
            string _valueStr = GetString(_key);
            long _value = long.Parse(_valueStr);

            return _value;
        }

        public static void SetLong(string _key, long _value)
        {
            SetString(_key, _value.ToString());
        }

        public static float GetFloat(string _key)
        {
            float _value = PlayerPrefs.GetFloat(Md5(_key));
            if (!CheckValue(_key, _value.ToString()))
                _value = 0;

            return _value;
        }

        public static void SetFloat(string _key, float _value)
        {
            PlayerPrefs.SetFloat(Md5(_key), _value);
            SaveMd5Value(_key, _value.ToString());
        }

        public static bool GetBool(string _key)
        {
            int _intValue = PlayerPrefs.GetInt(Md5(_key));
            if (!CheckValue(_key, _intValue.ToString()))
                _intValue = 0;

            bool _value = _intValue == 1;
            return _value;
        }

        public static void SetBool(string _key, bool _value)
        {
            int _intValue = _value ? 1 : 0;
            PlayerPrefs.SetInt(Md5(_key), _intValue);
            SaveMd5Value(_key, _intValue.ToString());
        }

        public static string GetString(string _key)
        {
            string _value = PlayerPrefs.GetString(Md5(_key));
            if (!CheckValue(_key, _value))
                _value = "";

            return _value;
        }

        public static void SetString(string _key, string _value)
        {
            PlayerPrefs.SetString(Md5(_key), _value);
            SaveMd5Value(_key, _value);
        }

        public static string LoadFileData(string _fileName)
        { /*
            string _fileData = FilesManager.Instance.ReadFile(_fileName);
            if (!CheckValue(_fileName, _fileData))
                _fileData = "";

            Debug.Log( "Load file " + _fileName + ": " + _fileData);

            return _fileData;
            */
            return null;
        }

        public static void SaveFileData(string _fileName, string _data)
        {
            /*
            Debug.Log( "Save file " + _fileName + ": " + _data);
            FilesManager.Instance.SaveFile(_fileName, _data);
            SaveMd5Value(_fileName, _data);
            */
        }

        public static bool HasKey(string _key)
        {
            bool _hasKey = PlayerPrefs.HasKey(Md5(_key));
            return _hasKey;
        }

        public static bool HasFile(string _fileName)
        {
            /*
            bool _hasFile = FilesManager.Instance.IsFileExists(_fileName);
            return _hasFile;
            */
            return false;
        }

        public static void SaveData()
        {
            PlayerPrefs.Save();
        }

        public static void RemoveKey(string _key)
        {
            PlayerPrefs.DeleteKey(Md5(_key));
            PlayerPrefs.DeleteKey(Md5(ConvertKeyForMd5Value(_key)));
        }

        public static void ClearData()
        {
            // FilesManager.Instance.DeleteAllFiles();
            PlayerPrefs.DeleteAll();
        }

        private static void SaveMd5Value(string _key, string _value)
        {
            _key = Md5(ConvertKeyForMd5Value(_key));
            _value = Md5(_value);

            PlayerPrefs.SetString(_key, _value);
        }

        private static bool CheckValue(string _key, string _value)
        {
            //Debug.Log("Check value: key " + _key + " value " + _value);

            bool _isCheck = false;

            _key = Md5(ConvertKeyForMd5Value(_key));
            _value = Md5(_value);

            _isCheck = _value == PlayerPrefs.GetString(_key);

            /*
            if (PlayerPrefs.HasKey(_key))
                Assert.IsTrue(_isCheck, LOG_KEY, () => "Data was hacked!");
            */

            return _isCheck;
        }

        private static string ConvertKeyForMd5Value(string _key)
        {
            _key = _key + "EF5nsh";
            return _key;
        }

        private static string Md5(string _originalStr)
        {
            UTF8Encoding _encoding = new UTF8Encoding();
            _originalStr = _originalStr + _secretKey;
            byte[] _bytes = _encoding.GetBytes(_originalStr);

            MD5CryptoServiceProvider _md5Service = new MD5CryptoServiceProvider();
            byte[] _hashBytes = _md5Service.ComputeHash(_bytes);

            string _hashStr = "";

            for (int i = 0; i < _hashBytes.Length; i++)
            {
                _hashStr += System.Convert.ToString(_hashBytes[i], 16).PadLeft(2, '0');
            }

            _hashStr = _hashStr.PadLeft(32, '0');

            return _hashStr;
        }
    }
}