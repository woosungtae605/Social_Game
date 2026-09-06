using System.Collections.Generic;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    public class CulturalScabManager : MonoBehaviour
    {
        [SerializeField] private CulturalScab prefab;
        [SerializeField] private RectTransform spawnParent;
        [SerializeField] private RectTransform dragArea;
        [SerializeField] private Transform yesTextBundle;
        [SerializeField] private CountryManager countryManager;
        [SerializeField] private bool clearPlacedScabsOnAwake = true;

        private readonly List<CulturalScab> living = new List<CulturalScab>();

        public IReadOnlyList<CulturalScab> Scabs => living;

        private void Awake()
        {
            if (clearPlacedScabsOnAwake)
                ClearPlacedChildren();
        }

        private void OnEnable()
        {
            Bus<CulturalScabDespawnedEvent>.OnEvent += HandleDespawned;
        }

        private void OnDisable()
        {
            Bus<CulturalScabDespawnedEvent>.OnEvent -= HandleDespawned;
        }

        public CulturalScab Spawn(float uniqueness, IReadOnlyList<CulturePortion> cultures)
        {
            return Spawn(uniqueness, cultures, RandomAnchoredPosition());
        }

        public CulturalScab Spawn(float uniqueness, IReadOnlyList<CulturePortion> cultures, Vector2 anchoredPosition)
        {
            if (prefab == null || spawnParent == null)
                return null;

            CulturalScab scab = Instantiate(prefab, spawnParent);
            var view = scab.GetComponent<JJM.Scripts.UIDraggable>();
            if (view != null)
                view.BindRuntime(dragArea, yesTextBundle);

            var drop = scab.GetComponent<CulturalScabCountryDrop>();
            if (drop != null)
                drop.Bind(countryManager, dragArea);

            scab.Initialize(uniqueness, cultures);

            var rect = scab.GetComponent<RectTransform>();
            if (rect != null)
                rect.anchoredPosition = anchoredPosition;

            living.Add(scab);
            Bus<CulturalScabSpawnedEvent>.RaiseEvent(new CulturalScabSpawnedEvent(scab));
            return scab;
        }

        public void Despawn(CulturalScab scab)
        {
            if (scab == null)
                return;

            living.Remove(scab);
            Destroy(scab.gameObject);
        }

        public void DespawnAll()
        {
            for (int i = living.Count - 1; i >= 0; i--)
            {
                CulturalScab scab = living[i];
                if (scab != null)
                    Destroy(scab.gameObject);
            }

            living.Clear();
        }

        private void HandleDespawned(CulturalScabDespawnedEvent evt)
        {
            if (evt.Scab != null)
                living.Remove(evt.Scab);
        }

        private void ClearPlacedChildren()
        {
            if (spawnParent == null)
                return;

            for (int i = spawnParent.childCount - 1; i >= 0; i--)
                Destroy(spawnParent.GetChild(i).gameObject);
        }

        private Vector2 RandomAnchoredPosition()
        {
            RectTransform area = dragArea != null ? dragArea : spawnParent;
            if (spawnParent == null)
                return Vector2.zero;

            if (area == null)
                return Vector2.zero;

            Vector2 half = PrefabHalfSize();
            Vector2 localInArea = RandomInRect(area.rect, half);
            return ToSpawnParentLocal(area, localInArea);
        }

        private Vector2 ToSpawnParentLocal(RectTransform area, Vector2 localInArea)
        {
            if (area == spawnParent)
                return localInArea;

            // Overlay canvas is often inactive (scale 0) when first spawning.
            // Sibling local math stays valid; world TransformPoint would collapse to 0.
            if (area.parent == spawnParent.parent)
                return area.anchoredPosition + localInArea - spawnParent.anchoredPosition;

            Vector3 world = area.TransformPoint(localInArea);
            return spawnParent.InverseTransformPoint(world);
        }

        private Vector2 PrefabHalfSize()
        {
            if (prefab == null)
                return new Vector2(50f, 50f);

            var prefabRect = prefab.GetComponent<RectTransform>();
            if (prefabRect == null)
                return new Vector2(50f, 50f);

            Vector2 size = prefabRect.rect.size;
            if (size.x <= 0f || size.y <= 0f)
                size = prefabRect.sizeDelta;

            return size * 0.5f;
        }

        private static Vector2 RandomInRect(Rect rect, Vector2 padding)
        {
            float xMin = rect.xMin + padding.x;
            float xMax = rect.xMax - padding.x;
            float yMin = rect.yMin + padding.y;
            float yMax = rect.yMax - padding.y;

            if (xMin > xMax)
            {
                xMin = rect.center.x;
                xMax = xMin;
            }

            if (yMin > yMax)
            {
                yMin = rect.center.y;
                yMax = yMin;
            }

            return new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        }
    }
}
