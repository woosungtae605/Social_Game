using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs.NewsUIs
{
    public class NewsItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleTxt;
        [SerializeField] private TextMeshProUGUI contentTxt;

        public void SetTitle(string title)
        {
            titleTxt.text = title;
        }

        public void SetContent(string content)
        {
            contentTxt.text = content;
        }

        public IEnumerator PlayTickers(float pixelsPerSecond)
        {
            yield return null;
            Canvas.ForceUpdateCanvases();

            PrepareTicker(titleTxt, out RectTransform titleRt, out float titleFrom, out float titleTo, out float titleDuration, pixelsPerSecond);
            PrepareTicker(contentTxt, out RectTransform contentRt, out float contentFrom, out float contentTo, out float contentDuration, pixelsPerSecond);

            float duration = Mathf.Max(titleDuration, contentDuration);
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                SetTickerX(titleRt, titleFrom, titleTo, elapsed, titleDuration);
                SetTickerX(contentRt, contentFrom, contentTo, elapsed, contentDuration);
                yield return null;
            }

            SetTickerX(titleRt, titleFrom, titleTo, titleDuration, titleDuration);
            SetTickerX(contentRt, contentFrom, contentTo, contentDuration, contentDuration);
        }

        private static void PrepareTicker(
            TextMeshProUGUI text,
            out RectTransform ticker,
            out float fromX,
            out float toX,
            out float duration,
            float pixelsPerSecond)
        {
            ticker = text.rectTransform;
            RectTransform viewport = (RectTransform)ticker.parent;

            if (ticker.TryGetComponent(out LayoutElement layoutElement))
                layoutElement.ignoreLayout = true;

            ticker.anchorMin = new Vector2(0f, 0.5f);
            ticker.anchorMax = new Vector2(0f, 0.5f);
            ticker.pivot = new Vector2(0f, 0.5f);

            text.ForceMeshUpdate();
            float textWidth = Mathf.Max(text.preferredWidth, 1f);
            ticker.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textWidth);

            fromX = viewport.rect.width;
            toX = -textWidth;
            duration = Mathf.Max(0.01f, (fromX - toX) / Mathf.Max(1f, pixelsPerSecond));

            Vector2 pos = ticker.anchoredPosition;
            pos.x = fromX;
            ticker.anchoredPosition = pos;
        }

        private static void SetTickerX(RectTransform ticker, float fromX, float toX, float elapsed, float duration)
        {
            Vector2 pos = ticker.anchoredPosition;
            pos.x = Mathf.Lerp(fromX, toX, Mathf.Clamp01(elapsed / duration));
            ticker.anchoredPosition = pos;
        }
    }
}
