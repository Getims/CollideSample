using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace LabraxStudio.Meta.GameField.Rules
{
    [Serializable]
    public class SpriteRule
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [FoldoutGroup(Matrix)]
        [HorizontalGroup(Group)]
        [VerticalGroup(Left)]
        [SerializeField, HideLabel, OnValueChanged(nameof(CalculateResult))]
        private bool _t2, _t1, _t128;

        [VerticalGroup(Middle)]
        [SerializeField, HideLabel, OnValueChanged(nameof(CalculateResult))]
        private bool _t4;

        [VerticalGroup(Middle)]
        [SerializeField, HideLabel, ReadOnly]
        private int _result;

        [VerticalGroup(Middle)]
        [SerializeField, HideLabel, OnValueChanged(nameof(CalculateResult))]
        private bool _t64;

        [VerticalGroup(Right)]
        [SerializeField, HideLabel, OnValueChanged(nameof(CalculateResult))]
        private bool _t8, _t16, _t32;

        [SerializeField]
        private int _needNumber = 0;

        // PROPERTIES: ----------------------------------------------------------------------------

        public int Rule => _result;

        // FIELDS: -------------------------------------------------------------------

        private const string Matrix = "Matrix";
        private const string Group = Matrix + "/Group";
        private const string Left = Group + "/Left Cells";
        private const string Middle = Group + "/Middle Cells";
        private const string Right = Group + "/Right Cells";

        // PRIVATE METHODS: -----------------------------------------------------------------------

        [Button]
        private void ConvertNumberToMatrix()
        {
            int[] elements = new[] {1, 2, 4, 8, 16, 32, 64, 128};
            int cycles = 10000;
            int summ = 0;

            for (int c = 0; c < cycles; c++)
            {
                summ = 0;
                ResetValues();
                for (int i = 0; i < elements.Length; i++)
                {
                    if (Utils.RandomBool())
                    {
                        summ += elements[i];
                        SetValue(i, true);
                    }

                    if (summ == _needNumber)
                        break;
                    if (elements[i] > _needNumber)
                        break;
                }

                if (summ == _needNumber)
                    break;
            }

            if (summ != _needNumber)
                ResetValues();
            CalculateResult();
        }

        private void ResetValues()
        {
            for (int i = 0; i < 8; i++)
                SetValue(i, false);
        }

        private void SetValue(int index, bool value)
        {
            switch (index)
            {
                case 0:
                    _t1 = value;
                    break;
                case 1:
                    _t2 = value;
                    break;
                case 2:
                    _t4 = value;
                    break;
                case 3:
                    _t8 = value;
                    break;
                case 4:
                    _t16 = value;
                    break;
                case 5:
                    _t32 = value;
                    break;
                case 6:
                    _t64 = value;
                    break;
                case 7:
                    _t128 = value;
                    break;
            }
        }

        private void CalculateResult()
        {
            _result = 0;
            if (_t1)
                _result += 1;
            if (_t2)
                _result += 2;
            if (_t4)
                _result += 4;
            if (_t8)
                _result += 8;
            if (_t16)
                _result += 16;
            if (_t32)
                _result += 32;
            if (_t64)
                _result += 64;
            if (_t128)
                _result += 128;
        }
    }
}