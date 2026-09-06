using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Team.WST.Scripts.CoreSystem
{
    public class FadeManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup fadeGroup;
        [SerializeField] private float fadeDuration = 0.45f;
        [SerializeField] private Color fadeColor = Color.black;
        [SerializeField] private bool fadeInOnStart = true;

        public static FadeManager Instance { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap()
        {
            if (Instance != null)
                return;

            var go = new GameObject(nameof(FadeManager));
            go.AddComponent<FadeManager>();
        }

        private Coroutine _routine;
        private bool _isLoadingScene;

        public bool IsFading => _routine != null;

        private void Awake()
        {
            FadeManager[] managers = FindObjectsByType<FadeManager>(FindObjectsSortMode.None);
            if (managers.Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            EnsureOverlay();

            if (fadeInOnStart && fadeGroup != null)
            {
                fadeGroup.alpha = 1f;
                fadeGroup.blocksRaycasts = true;
            }
        }

        private void Start()
        {
            if (!fadeInOnStart || fadeGroup == null)
                return;

            Play(FadeTo(0f));
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        public void LoadScene(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName) || _isLoadingScene)
                return;

            Play(LoadSceneRoutine(sceneName));
        }

        public static void LoadSceneOrFallback(string sceneName)
        {
            if (Instance != null)
                Instance.LoadScene(sceneName);
            else
                SceneManager.LoadScene(sceneName);
        }

        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            _isLoadingScene = true;

            yield return FadeTo(1f);

            AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
            if (load == null)
            {
                yield return FadeTo(0f);
                _isLoadingScene = false;
                yield break;
            }

            while (!load.isDone)
                yield return null;

            yield return FadeTo(0f);
            _isLoadingScene = false;
        }

        private void Play(IEnumerator routine)
        {
            if (_routine != null)
                StopCoroutine(_routine);

            _routine = StartCoroutine(Wrap(routine));
        }

        private IEnumerator Wrap(IEnumerator routine)
        {
            yield return routine;
            _routine = null;
        }

        private IEnumerator FadeTo(float target)
        {
            EnsureOverlay();
            if (fadeGroup == null)
                yield break;

            float from = fadeGroup.alpha;
            float duration = Mathf.Max(0.01f, fadeDuration);
            float elapsed = 0f;
            fadeGroup.blocksRaycasts = true;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                t = t * t * (3f - 2f * t);
                fadeGroup.alpha = Mathf.Lerp(from, target, t);
                yield return null;
            }

            fadeGroup.alpha = target;
            fadeGroup.blocksRaycasts = target > 0.01f;
        }

        private void EnsureOverlay()
        {
            if (fadeGroup != null)
                return;

            var canvasGo = new GameObject("FadeCanvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvasGo.transform.SetParent(transform, false);
            canvasGo.layer = 5;

            var canvas = canvasGo.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 32767;

            var scaler = canvasGo.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;

            fadeGroup = canvasGo.AddComponent<CanvasGroup>();
            fadeGroup.alpha = 0f;
            fadeGroup.interactable = false;
            fadeGroup.blocksRaycasts = false;

            var imageGo = new GameObject("FadeImage", typeof(RectTransform), typeof(Image));
            imageGo.layer = 5;
            imageGo.transform.SetParent(canvasGo.transform, false);

            var rt = imageGo.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            var image = imageGo.GetComponent<Image>();
            image.color = fadeColor;
            image.raycastTarget = true;
        }
    }
}
