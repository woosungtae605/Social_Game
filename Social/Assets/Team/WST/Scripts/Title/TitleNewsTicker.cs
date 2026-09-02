using System.Collections;
using TMPro;
using UnityEngine;

namespace Team.WST.Scripts.Title
{
    public class TitleNewsTicker : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tickerText;
        [SerializeField] private float pixelsPerSecond = 90f;
        [SerializeField] private string[] headlines =
        {
            "속보 · 한국 문화, 북미 상륙. 현지 취향 감염률 급상승",
            "CULTURE CORP. 분기 보고 · 동아시아 전선 안정, 서구 확산을 권고합니다",
            "경고 · 자국 문화 저항 감지. 격리하지 말고 트렌드를 투입하세요",
            "LIVE · 일본에서 K-콘텐츠 대확산. 문화력 프로토콜 가동",
            "인사말 · 오늘도 세계를 취향으로 물들일 준비 되셨습니까"
        };

        private void OnEnable()
        {
            if (tickerText == null || headlines == null || headlines.Length == 0)
                return;

            StopAllCoroutines();
            StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            RectTransform ticker = tickerText.rectTransform;
            RectTransform viewport = (RectTransform)ticker.parent;
            int index = 0;

            while (enabled)
            {
                tickerText.text = headlines[index];
                tickerText.ForceMeshUpdate();

                float width = Mathf.Max(tickerText.preferredWidth, 1f);
                ticker.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

                float fromX = viewport.rect.width;
                float toX = -width;
                float duration = Mathf.Max(0.01f, (fromX - toX) / Mathf.Max(1f, pixelsPerSecond));
                float elapsed = 0f;

                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    Vector2 pos = ticker.anchoredPosition;
                    pos.x = Mathf.Lerp(fromX, toX, Mathf.Clamp01(elapsed / duration));
                    ticker.anchoredPosition = pos;
                    yield return null;
                }

                index = (index + 1) % headlines.Length;
            }
        }
    }
}
