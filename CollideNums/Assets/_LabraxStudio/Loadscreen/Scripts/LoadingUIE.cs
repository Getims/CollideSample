using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using LabraxStudio.Base;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace LabraxStudio.Loadscreen
{
    public class LoadingUIE : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private float _fadeTime = 0.5f;

        [Tooltip("Минимальное время загрузки")]
        [SerializeField, Min(0)]
        private float _onScreenMinTime = 0.5f;

        [SerializeField]
        private bool _loadOnStart = false;

        [SerializeField, ShowIf(nameof(_loadOnStart))]
        private bool _autoLoad = true;

        [SerializeField, ShowIf(nameof(_loadOnStart))]
        private Scenes _startScene = Scenes.Game;

        [SerializeField]
        private Image _sliderImg;

        [SerializeField]
        private bool _useFill = true;

        [SerializeField, HideIf(nameof(_useFill))]
        private float _sliderWidth = 500;

        [SerializeField]
        private bool _useCanvas = false;

        [SerializeField, ShowIf(nameof(_useCanvas))]
        private Canvas _canvas;

        [Title("Events")]
        [SerializeField]
        private UnityEvent _onLoadStarted;

        [SerializeField]
        private UnityEvent _onLoadFinished;

        // PROPERTIES: ----------------------------------------------------------------------------

        public float FadeTime => _fadeTime;

        private CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                    _canvasGroup = GetComponent<CanvasGroup>();

                return _canvasGroup;
            }
        }

        // FIELDS: -------------------------------------------------------------------

        private string _loadScene;
        private Tweener _fadeTwnr;
        private Tweener _moveTwnr;
        private CanvasGroup _canvasGroup;
        private float _nextX = 0;


        // GAME ENGINE METHODS: -------------------------------------------------------------------

        public void Start()
        {
            //DontDestroyOnLoad(this.gameObject);

            if (!_loadOnStart)
                return;

            DontDestroyOnLoad(this.gameObject);
            CanvasGroup.DOFade(1, 0);
            MoveSlider(0, 0);

            if (!_autoLoad)
                return;

            _loadScene = _startScene.ToString();
            StartCoroutine(LoadAsynchronously());
        }

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void LoadScene(Scenes sceneName)
        {
            _fadeTwnr.Complete();
            _loadScene = sceneName.ToString();
            MoveSlider(0, 0);
            StartCoroutine(LoadAsynchronously());
        }

        public void LoadScene(string _sceneName)
        {
            _fadeTwnr.Complete();
            _loadScene = _sceneName;
            MoveSlider(0, 0);
            StartCoroutine(LoadAsynchronously());
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private IEnumerator LoadAsynchronously()
        {
            LoadCounter.SetStartLoad();
            _onLoadStarted?.Invoke();
            float elapsedTime = 0;
            SetCanvasState(true);
            if (!_loadOnStart)
            {
                _fadeTwnr = CanvasGroup.DOFade(1, _fadeTime * 0.8f);
                yield return new WaitForSeconds(_fadeTime * 0.8f);
            }
            else
                _fadeTwnr = CanvasGroup.DOFade(1, 0);

            AsyncOperation loading = SceneManager.LoadSceneAsync(_loadScene);

            while (!loading.isDone || elapsedTime <= _onScreenMinTime)
            {
                float progress = loading.progress / 0.925f;
                float time = progress * Mathf.Lerp(0, 1, elapsedTime / _onScreenMinTime);
                elapsedTime += Time.deltaTime;
                if (_useFill)
                    MoveSlider(time);
                else
                    MoveSlider(time * _sliderWidth);
                yield return null;
            }

            if (_useFill)
                MoveSlider(1, 0);
            else
                MoveSlider(_sliderWidth, 0);

            yield return new WaitForSeconds(0.05f);

            _fadeTwnr.Complete();
            _fadeTwnr = CanvasGroup.DOFade(0, _fadeTime);
            yield return new WaitForSeconds(_fadeTime);

            SetCanvasState(false);
            if (_loadOnStart)
                Destroy();

            _onLoadFinished?.Invoke();
            LoadCounter.SetLoadComplete();
        }

        private void Destroy()
        {
            StopAllCoroutines();
            _fadeTwnr.Kill();
            _moveTwnr.Kill();
            Destroy(this.gameObject);
        }

        private void MoveSlider(float _value, float _time = 0.1f)
        {
            if (_value >= _sliderWidth)
                _value = _sliderWidth;
            _moveTwnr.Complete();
            if (_nextX != _value)
            {
                _nextX = _value;
                if (_useFill)
                    _moveTwnr = _sliderImg.DOFillAmount(_nextX, _time);
                else
                    _moveTwnr = _sliderImg.rectTransform.DOAnchorPosX(_nextX, _time);
            }
        }

        private void SetCanvasState(bool enabled)
        {
            if (!_useCanvas)
                return;

            _canvas.enabled = enabled;
        }
    }
}