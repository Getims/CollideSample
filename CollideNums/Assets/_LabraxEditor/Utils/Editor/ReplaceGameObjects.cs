using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LabraxStudio.Tools
{
    public class ReplaceGameObjects : ScriptableWizard
    {
        [SerializeField] private bool _copyPosition = true;
        [SerializeField] private bool _copyRotation = true;
#pragma warning disable 0414
        [SerializeField] private bool _copyScale = true;
#pragma warning restore 0414
        [SerializeField] private bool _copyParent = true;
        [SerializeField] private bool _destroyOldObject = true;
        [SerializeField] private GameObject _newObject;
        [SerializeField] private GameObject[] oldObjects;

        [MenuItem("Tools/Replace GameObjects")]


        static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard("Replace GameObjects", typeof(ReplaceGameObjects), "Replace");
        }

        void OnWizardCreate()
        {
            GameObject newObject;
            if (_newObject == null)
                return;
            foreach (GameObject go in oldObjects)
            {
                if (go == null)
                    continue;
                newObject = (GameObject)PrefabUtility.InstantiatePrefab(_newObject);

                if (_copyParent)
                    newObject.transform.parent = go.transform.parent;
                if (_copyPosition)
                    newObject.transform.position = go.transform.position;
                if (_copyRotation)
                    newObject.transform.rotation = go.transform.rotation;
                if (_copyRotation)
                    newObject.transform.localScale = go.transform.localScale;

                if (_destroyOldObject)
                    DestroyImmediate(go);
            }
        }
    }
}