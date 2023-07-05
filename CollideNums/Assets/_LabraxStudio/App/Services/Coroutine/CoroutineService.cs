using System.Collections;

namespace LabraxStudio.App.Services
{
    public class CoroutineService
    {
        // FIELDS: -------------------------------------------------------------------

        private CoroutineRunner _coroutineRunner;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void RegisterCoroutineRunner(CoroutineRunner runner)
        {
            _coroutineRunner = runner;
        }

        public void RunCoroutine(IEnumerator routine)
        {
            if (_coroutineRunner != null)
                _coroutineRunner.RunCoroutine(routine);
        }
    }
}