using System;
using System.Collections;
using UnityEngine;

namespace LabraxStudio.Managers
{
    public abstract class SharedManager : MonoBehaviour
    {
        /// <summary>
        /// Init manager's internal state.
        /// </summary>
        public abstract void InitManager();

        /// <summary>
        /// Call on application pause or exit.
        /// </summary>
        public abstract void OnAppDeactivate();
    }

    public abstract class SharedManager<S> : SharedManager where S : SharedManager
    {
        protected virtual void Awake()
        {
            InitManager();
        }

        public override void InitManager()
        {
            if (Instance == null)
                Instance = this as S;
        }

        public override void OnAppDeactivate()
        {
        }

        public Coroutine PerformWithDelay(float delay, Action func)
        {
            return StartCoroutine(Perform(delay, func));
        }
        IEnumerator Perform(float seconds, Action func)
        {
            yield return new WaitForSeconds(seconds);
            func();
        }

        public static S Instance { get; private set; }
    }
}