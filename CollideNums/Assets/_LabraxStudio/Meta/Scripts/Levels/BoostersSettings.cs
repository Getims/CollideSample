using System;
using UnityEngine;

namespace LabraxStudio.Meta.Levels
{
    [Serializable]
    public class BoostersSettings
    {
        [SerializeField]
        private BoosterMeta _boosterMeta;

        public BoosterMeta BoosterMeta => _boosterMeta;
    }
}