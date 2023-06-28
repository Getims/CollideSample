using System;
using LabraxStudio.Meta.GameField.Rules;
using UnityEngine;

namespace LabraxStudio.Meta.GameField
{
    [Serializable]
    internal class SpriteAndRule
    {
        [SerializeField]
        public Sprite Sprite;

        [SerializeField]
        public SpriteGenerateRules Rules;
    }
}