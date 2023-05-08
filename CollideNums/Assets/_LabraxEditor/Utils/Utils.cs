using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;
using UnityEditor;
using System.IO;

public static class Utils
{
    // random seed fro suffle func
    static System.Random rand = new System.Random();

    public static void GoToScene(string name)
    {
        if (name == "Globals") return;
        var previousScene = SceneManager.GetActiveScene();
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.SetActiveScene(scene);
            if (previousScene.name != "Globals")
            {
                SceneManager.UnloadSceneAsync(previousScene.name);
                Resources.UnloadUnusedAssets();
            }
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    public static void adjustImageSize(GameObject image, GameObject Canvas)
    {
        if (image != null && Canvas != null)
        {
            float scaleBGvalueW = 1f / (image.GetComponent<RectTransform>().rect.width / Canvas.GetComponent<RectTransform>().rect.width);
            float scaleBGvalueH = 1f / (image.GetComponent<RectTransform>().rect.height / Canvas.GetComponent<RectTransform>().rect.height);
            Vector3 currentScaleValue = image.GetComponent<UnityEngine.UI.Image>().transform.localScale;
            currentScaleValue.x = scaleBGvalueW;
            currentScaleValue.y = scaleBGvalueH;
            if (currentScaleValue.x > currentScaleValue.y)
            {
                image.GetComponent<UnityEngine.UI.Image>().transform.localScale = currentScaleValue;
            }
            else
            {
                currentScaleValue.x = currentScaleValue.y;
                image.GetComponent<UnityEngine.UI.Image>().transform.localScale = currentScaleValue;
            }
        }
    }

    public static List<Type> GetTypesInNamespace(string nameSpace)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type[] allTypes = assembly.GetTypes();
        var namespaceTypes = new List<Type>();
        for (int i = 0; i < allTypes.Length; i++)
        {
            if (allTypes[i].Namespace == nameSpace)
            {
                namespaceTypes.Add(allTypes[i]);
            }
        }
        return namespaceTypes;
    }
    public static void Shuffle<T>(T[] arr)
    {
        for (int i = arr.Length - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            T tmp = arr[j];
            arr[j] = arr[i];
            arr[i] = tmp;
        }
    }
    public static void Shuffle<T>(List<T> arr)
    {
        for (int i = arr.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            T tmp = arr[j];
            arr[j] = arr[i];
            arr[i] = tmp;
        }
    }
    public static void RightShit<T>(List<T> arr, int shiftValue)
    {
        List<T> temp = new List<T>(arr);
        for (int i = 0; i < temp.Count; i++)
        {
            arr[(i + shiftValue) % temp.Count] = temp[i];
        }
    }
    public static void ScaleAdaptive(Transform transform)
    {
        Debug.Log(Screen.height + " x " + Screen.width);
        float ratio = (float)Screen.height / Screen.width;
        if (ratio == Mathf.Clamp(ratio, 0.57f, 0.65f)) transform.localScale = new Vector3(0.9f, 0.9f);
        if (ratio == Mathf.Clamp(ratio, 0.65f, 0.73f)) transform.localScale = new Vector3(0.8f, 0.8f);
        if (ratio == Mathf.Clamp(ratio, 0.73f, 0.8f)) transform.localScale = new Vector3(0.75f, 0.75f);
        if (ratio == Mathf.Clamp(ratio, 0.8f, 0.9f)) transform.localScale = new Vector3(0.7f, 0.7f);
    }

    public static string GetMoneyStringValue(int _value)
    {
        if (_value < 1000)
            return _value.ToString();

        float price = 1.0f * _value / 1000;
        price = (float)(Mathf.Round(price * 10)) / 10f; ;
        return price.ToString() + "K";
    }

    public static Color ColorHexToRGB(string hexColor)
    {
        Color colorRGB = Color.clear;
        ColorUtility.TryParseHtmlString(hexColor, out colorRGB);
        return colorRGB;
    }

    public struct UNIXTime
    {
        public static int Now { get => CurrentUnixTimeSeconds(); }
        public static UNIXTime Today { get => new UNIXTime(CurrentUnixTimeSeconds()); }
        public int second { get => dateTime.Second; }
        public int minute { get => dateTime.Minute; }
        public int hour { get => dateTime.Hour; }
        public int day { get => dateTime.Day; }
        public int month { get => dateTime.Month; }
        public int year { get => dateTime.Year; }
        int unixSeconds;
        DateTime dateTime;
        DateTimeOffset dateTimeOffset;
        /* --- constructors ----------------------------------------------------------------------------- */
        public UNIXTime(int unixSeconds = 0)
        {
            this.unixSeconds = unixSeconds;
            dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(this.unixSeconds).ToLocalTime();
            dateTime = dateTimeOffset.DateTime;
        }
        /* --- public methods ----------------------------------------------------------------------------- */
        public static int DaysToSeconds(int d) { return d * 24 * 60 * 60; }
        public static int HoursToSeconds(int h) { return h * 60 * 60; }
        public static int MinutesToSeconds(int m) { return m * 60; }
        public int ToInt()
        {
            return unixSeconds;
        }
        public new string ToString()
        {
            return dateTime.ToString();
        }
        public int AddDays(int days)
        {
            unixSeconds += DaysToSeconds(days);
            return unixSeconds;
        }
        /* --- private methods ----------------------------------------------------------------------------- */
        static int CurrentUnixTimeSeconds()
        {
            return (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }

    static public string FormatTime(int value = 0)
    {
        if (value > 0) return value < 10 ? "0" + value.ToString() : value.ToString();
        return "00";
    }

    static public bool Clamp(float value, float min, float max)
    {
        return value == Mathf.Clamp(value, min, max);
    }

    static public bool Clamp(int value, int min, int max)
    {
        return value == Mathf.Clamp(value, min, max);
    }

    public static int RandomValue(int value1, int value2)
    {
        if (UnityEngine.Random.value > 0.5f)
            return value1;
        else
            return value2;
    }

    public static int RandomValue(int[] values)
    {
        return values[UnityEngine.Random.Range(0, values.Length)];
    }
    
    public static float RandomValue(float[] values)
    {
        return values[UnityEngine.Random.Range(0, values.Length)];
    }

    public static float RandomValue(float value1, float value2)
    {
        if (UnityEngine.Random.value > 0.5f)
            return value1;
        else
            return value2;
    }

    public static bool RandomBool()
    {
        if (UnityEngine.Random.value > 0.5f)
            return true;
        else
            return false;
    }

    public static void DebugPoint()
    {
        Debug.LogError("<color=red>Debug point</color>"); ;
    }

    public static void DebugPoint(object log)
    {
        Debug.LogError("<color=red>Debug point:</color> " + log.ToString()); ;
    }

    public static void ReworkPoint()
    {
        Debug.Log("<color=yellow>Rework point</color>"); ;
    }
    
    public static void ReworkPoint(object log)
    {
        Debug.Log("<color=yellow>Rework point:</color> " + log.ToString()); ;
    }
    
    public static void WarningPoint(object log)
    {
        Debug.Log("<color=yellow>Warning point:</color> " + log.ToString()); ;
    }
    
    static public void Log(string objectName, params object[] args)
    {
        Debug.Log(objectName + ": " + args[0]);
    }

    static public void PerformWithDelay(MonoBehaviour obj, float delay, Action func)
    {
        if (func == null)
            return;

        obj.StartCoroutine(Perform(delay, func));
    }
    static IEnumerator Perform(float seconds, Action func)
    {
        yield return new WaitForSeconds(seconds);
        func();
    }
    static public List<GameObject> AllChilds(GameObject root)
    {
        List<GameObject> result = new List<GameObject>();
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(result, VARIABLE.gameObject);
            }
        }
        return result;
    }

    static public void Searcher(List<GameObject> list, GameObject root)
    {
        list.Add(root);
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(list, VARIABLE.gameObject);
            }
        }
    }

    public static string GetUniqueID() =>
        Guid.NewGuid().ToString().Remove(0, 20);
    public static string GetUniqueID(int length) =>
        Guid.NewGuid().ToString().Remove(0, length);

    public static List<T> GetAllScriptableObjectsOfType<T>() where T : ScriptableObject
    {
        List<T> res = new List<T>();

#if UNITY_EDITOR
        string typeName = typeof(T).FullName;

        string[] guids = AssetDatabase.FindAssets("t:" + typeName, new string[] { "Assets" });
        foreach (string guid in guids)
            res.Add(AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)));
#endif
        return res;
    }
    public static void SetSceneDirty()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
#endif
    }
    public static void SetDirtyObj(UnityEngine.Object _obj)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying && !EditorUtility.IsDirty(_obj))
            EditorUtility.SetDirty(_obj);
#endif
    }
    public static List<T> LoadAllPrefabsOfType<T>(string path) where T : MonoBehaviour
    {
        List<T> prefabComponents = new List<T>();
#if UNITY_EDITOR
        if (path != "")
        {
            if (path.EndsWith("/"))
            {
                path = path.TrimEnd('/');
            }
        }

        DirectoryInfo dirInfo = new DirectoryInfo(path);
        foreach (var dir in dirInfo.GetDirectories())
        {
            prefabComponents = LoadAllPrefabsOfType<T>(dir.FullName);
        }
        FileInfo[] fileInf = dirInfo.GetFiles("*.prefab");

        //loop through directory loading the game object and checking if it has the component you want
        foreach (FileInfo fileInfo in fileInf)
        {
            string fullPath = fileInfo.FullName.Replace(@"\", "/");
            string assetPath = "Assets" + fullPath.Replace(Application.dataPath, "");
            GameObject prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;

            if (prefab != null)
            {
                T hasT = prefab.GetComponent<T>();
                if (hasT != null)
                {
                    prefabComponents.Add(hasT);
                }
            }
        }
#endif
        return prefabComponents;
    }

}
