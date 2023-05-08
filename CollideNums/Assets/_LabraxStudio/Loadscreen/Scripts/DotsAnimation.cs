using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LabraxStudio.UI
{
    public class DotsAnimation : MonoBehaviour
    {
        [SerializeField] private List<UIPanel> _dots = new List<UIPanel>();

        private int lastDot = 0;
        private int currentDot = 0;
        private Coroutine _animationCO;

        private void Start()
        {
            if (_dots.Count == 0)
                return;

            _animationCO = StartCoroutine(Animation());
        }

        private IEnumerator Animation()
        {
            float time = _dots[0].FadeTime + 0.01f;
            _dots[0].Show();
            currentDot++;
            yield return new WaitForSeconds(time);

            while (true)
            {
                _dots[lastDot].Hide();
                _dots[currentDot].Show();
                lastDot = currentDot;
                currentDot++;
                if (currentDot >= _dots.Count)
                    currentDot = 0;
                yield return new WaitForSeconds(time);
            }
        }

        private void OnDestroy()
        {
            if (_animationCO != null)
                StopCoroutine(_animationCO);
        }
    }
}