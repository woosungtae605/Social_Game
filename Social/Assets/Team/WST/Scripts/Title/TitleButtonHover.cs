using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Team.WST.Scripts.Title
{
    public class TitleButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float restWidth = 420f;
        [SerializeField] private float hoverWidth = 500f;
        [SerializeField] private float duration = 0.18f;

        private Coroutine _tween;
        private float _currentWidth;

        private void Awake()
        {
            if (layoutElement == null)
                layoutElement = GetComponent<LayoutElement>();
            if (rectTransform == null)
                rectTransform = transform as RectTransform;

            _currentWidth = restWidth;
            ApplyWidth(_currentWidth);
        }

        private void OnDisable()
        {
            if (_tween != null)
            {
                StopCoroutine(_tween);
                _tween = null;
            }

            ApplyWidth(restWidth);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Play(hoverWidth);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Play(restWidth);
        }

        private void Play(float target)
        {
            if (!isActiveAndEnabled)
                return;

            if (_tween != null)
                StopCoroutine(_tween);

            _tween = StartCoroutine(TweenWidth(target));
        }

        private IEnumerator TweenWidth(float target)
        {
            float from = _currentWidth;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                t = 1f - (1f - t) * (1f - t);
                ApplyWidth(Mathf.Lerp(from, target, t));
                yield return null;
            }

            ApplyWidth(target);
            _tween = null;
        }

        private void ApplyWidth(float width)
        {
            _currentWidth = width;

            if (layoutElement != null)
            {
                layoutElement.minWidth = width;
                layoutElement.preferredWidth = width;
            }

            if (rectTransform != null)
            {
                Vector2 size = rectTransform.sizeDelta;
                size.x = width;
                rectTransform.sizeDelta = size;
            }
        }
    }
}
