using Team.WST.Scripts.Countries;
using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    public static class CountryAtScreenPoint
    {
        private const float SnapRadius = 0.2f;

        public static bool TryGet(Vector2 screenPosition, out AbstractCountry country)
        {
            country = null;
            Camera camera = Camera.main;
            if (camera == null)
                return false;

            if (!TryMapPoint(camera, screenPosition, out Vector2 mapPoint))
                return false;

            Collider2D hit = Physics2D.OverlapPoint(mapPoint);
            if (hit != null)
            {
                country = hit.GetComponentInParent<AbstractCountry>();
                if (country != null)
                    return true;
            }

            return TryGetNearest(mapPoint, out country);
        }

        private static bool TryMapPoint(Camera camera, Vector2 screenPosition, out Vector2 mapPoint)
        {
            mapPoint = default;
            Ray ray = camera.ScreenPointToRay(screenPosition);
            var mapPlane = new Plane(Vector3.forward, Vector3.zero);
            if (!mapPlane.Raycast(ray, out float distance))
                return false;

            Vector3 world = ray.GetPoint(distance);
            mapPoint = world;
            return true;
        }

        private static bool TryGetNearest(Vector2 mapPoint, out AbstractCountry country)
        {
            country = null;
            Collider2D[] hits = Physics2D.OverlapCircleAll(mapPoint, SnapRadius);
            if (hits == null || hits.Length == 0)
                return false;

            float bestSqr = float.MaxValue;
            for (int i = 0; i < hits.Length; i++)
            {
                Collider2D hit = hits[i];
                if (hit == null)
                    continue;

                AbstractCountry candidate = hit.GetComponentInParent<AbstractCountry>();
                if (candidate == null)
                    continue;

                float sqr = ((Vector2)hit.ClosestPoint(mapPoint) - mapPoint).sqrMagnitude;
                if (sqr >= bestSqr)
                    continue;

                bestSqr = sqr;
                country = candidate;
            }

            return country != null;
        }
    }
}
