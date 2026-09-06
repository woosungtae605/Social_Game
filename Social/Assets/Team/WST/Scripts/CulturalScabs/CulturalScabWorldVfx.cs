using JJM.Scripts.CoreSystem.Effect;
using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    public static class CulturalScabWorldVfx
    {
        private const float MinLifetime = 2f;

        public static void Play(
            PlayParticleVFX source,
            Canvas canvas,
            Vector2 screenPosition,
            Camera eventCamera)
        {
            if (source == null || canvas == null)
                return;

            PlayParticleVFX instance = Object.Instantiate(source, canvas.transform, false);
            PlaceOnScreen(instance.transform as RectTransform, canvas, screenPosition, eventCamera);
            instance.PlayVFX();
            Object.Destroy(instance.gameObject, Mathf.Max(MinLifetime, instance.VfxDuration));
        }

        private static void PlaceOnScreen(
            RectTransform rect,
            Canvas canvas,
            Vector2 screenPosition,
            Camera eventCamera)
        {
            if (rect == null)
                return;

            Camera uiCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : (canvas.worldCamera != null ? canvas.worldCamera : eventCamera);

            if (!RectTransformUtility.ScreenPointToWorldPointInRectangle(
                    canvas.transform as RectTransform,
                    screenPosition,
                    uiCamera,
                    out Vector3 world))
                return;

            rect.position = world;
        }
    }
}
