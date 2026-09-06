using TMPro;
using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    public class CulturalScabTooltipView : MonoBehaviour
    {
        [SerializeField] private RectTransform panel;
        [SerializeField] private TextMeshProUGUI body;
        [SerializeField] private RectTransform canvasRect;
        [SerializeField] private float gap = 10f;

        private readonly Vector3[] worldCorners = new Vector3[4];

        public bool IsVisible => gameObject.activeSelf;

        public void Show(string text)
        {
            if (body != null && body.text != text)
                body.text = text;

            if (!gameObject.activeSelf)
                gameObject.SetActive(true);

            if (panel != null)
                UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(panel);
        }

        public void Hide()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        public void Follow(RectTransform target)
        {
            if (panel == null || canvasRect == null || target == null)
                return;

            Canvas canvas = canvasRect.GetComponentInParent<Canvas>();
            if (canvas != null)
                canvas = canvas.rootCanvas;

            Camera eventCamera = null;
            if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
                eventCamera = canvas.worldCamera;

            target.GetWorldCorners(worldCorners);
            Vector3 worldTopCenter = (worldCorners[1] + worldCorners[2]) * 0.5f;
            Vector2 screen = RectTransformUtility.WorldToScreenPoint(eventCamera, worldTopCenter);

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRect,
                    screen,
                    eventCamera,
                    out Vector2 localPoint))
                return;

            Vector2 position = localPoint + new Vector2(0f, gap);
            panel.anchoredPosition = ClampToCanvas(position);
        }

        private Vector2 ClampToCanvas(Vector2 position)
        {
            Rect canvasArea = canvasRect.rect;
            Rect panelArea = panel.rect;
            Vector2 pivot = panel.pivot;

            float minX = canvasArea.xMin + panelArea.width * pivot.x;
            float maxX = canvasArea.xMax - panelArea.width * (1f - pivot.x);
            float minY = canvasArea.yMin + panelArea.height * pivot.y;
            float maxY = canvasArea.yMax - panelArea.height * (1f - pivot.y);

            if (minX > maxX)
                position.x = canvasArea.center.x;
            else
                position.x = Mathf.Clamp(position.x, minX, maxX);

            if (minY > maxY)
                position.y = canvasArea.center.y;
            else
                position.y = Mathf.Clamp(position.y, minY, maxY);

            return position;
        }
    }
}
