using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LabraxStudio.App.Services
{
    public class CoroutineRunner : MonoBehaviour
    {
        // FIELDS: -------------------------------------------------------------------

        private List<Coroutine> _coroutines = new List<Coroutine>();

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start()
        {
            ServicesProvider.CoroutineService.RegisterCoroutineRunner(this);
        }

        private void OnDestroy()
        {
            foreach (var coroutine in _coroutines)
            {
                if (coroutine != null)
                    StopCoroutine(coroutine);
            }
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void RunCoroutine(IEnumerator routine)
        {
            var coroutine = StartCoroutine(routine);
            _coroutines.Add(coroutine);
        }
    }
}