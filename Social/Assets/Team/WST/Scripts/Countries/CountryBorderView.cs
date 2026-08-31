using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(LineRenderer))]
    public class CountryBorderView : MonoBehaviour
    {
        [SerializeField] private PolygonCollider2D borderCollider;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private float width = 0.03f;
        [SerializeField] private int sortingOrder = 5;

        private void Reset()
        {
            CacheRefs();
        }

        private void Awake()
        {
            CacheRefs();
            Rebuild();
        }

        private void OnValidate()
        {
            CacheRefs();
            if (borderCollider != null && lineRenderer != null)
                Rebuild();
        }

        public void Rebuild()
        {
            CacheRefs();
            if (borderCollider == null || lineRenderer == null)
                return;

            lineRenderer.useWorldSpace = false;
            lineRenderer.loop = true;
            lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineRenderer.receiveShadows = false;
            lineRenderer.allowOcclusionWhenDynamic = false;
            lineRenderer.textureMode = LineTextureMode.Stretch;
            lineRenderer.alignment = LineAlignment.View;
            lineRenderer.numCapVertices = 2;
            lineRenderer.numCornerVertices = 2;
            lineRenderer.widthMultiplier = 1f;
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            lineRenderer.sortingOrder = sortingOrder;

            Color color = Color.white;
            color.a = 1f;
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;

            Vector2[] path = borderCollider.GetPath(0);
            if (path == null || path.Length < 2)
            {
                lineRenderer.positionCount = 0;
                return;
            }

            lineRenderer.positionCount = path.Length;
            for (int i = 0; i < path.Length; i++)
                lineRenderer.SetPosition(i, path[i]);
        }

        private void CacheRefs()
        {
            if (borderCollider == null)
                borderCollider = GetComponent<PolygonCollider2D>();
            if (lineRenderer == null)
                lineRenderer = GetComponent<LineRenderer>();
        }
    }
}
