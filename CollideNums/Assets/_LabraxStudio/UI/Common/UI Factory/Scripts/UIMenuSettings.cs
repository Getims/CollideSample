using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.UI.Common.Factory
{
    [Serializable]
    public class UIMenuSettings
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private MenuType _menuType;

        [SerializeField, Required, AssetsOnly]
        private GameObject _menuPrefab;

        [SerializeField]
        private Transform _parent;

        // PROPERTIES: ----------------------------------------------------------------------------

        public MenuType MenuType => _menuType;
        public GameObject MenuPrefab => _menuPrefab;
        public Transform Parent => _parent;
    }
}