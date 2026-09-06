using JJM.Scripts;
using JJM.Scripts.CoreSystem.Effect;
using Team.WST.Scripts.Countries;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Team.WST.Scripts.CulturalScabs
{
    public class CulturalScabCountryDrop : MonoBehaviour
    {
        [SerializeField] private CulturalScab scab;
        [SerializeField] private PlayParticleVFX successVfx;

        private CountryManager countryManager;
        private RectTransform dragArea;
        private UIDraggable view;

        private void Awake()
        {
            if (scab == null)
                scab = GetComponent<CulturalScab>();

            view = GetComponent<UIDraggable>();

            if (successVfx == null)
                successVfx = FindNamedVfx("GreenParticle");
        }

        public void Bind(CountryManager manager, RectTransform area)
        {
            countryManager = manager;
            dragArea = area;
        }

        public bool TryHandleRelease(PointerEventData eventData)
        {
            if (IsInsideDragArea(eventData))
                return false;

            if (CountryAtScreenPoint.TryGet(eventData.position, out AbstractCountry country)
                && CulturalScabCultureInjector.Inject(countryManager, country, scab))
            {
                PlaySuccessAt(eventData.position, eventData.pressEventCamera);
                Destroy(gameObject);
                return true;
            }

            if (view != null)
                view.ClampToDragArea();

            return true;
        }

        private void PlaySuccessAt(Vector2 screenPosition, Camera eventCamera)
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null)
                canvas = canvas.rootCanvas;

            CulturalScabWorldVfx.Play(successVfx, canvas, screenPosition, eventCamera);
        }

        private PlayParticleVFX FindNamedVfx(string objectName)
        {
            PlayParticleVFX[] particles = GetComponentsInChildren<PlayParticleVFX>(true);
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i] != null && particles[i].gameObject.name == objectName)
                    return particles[i];
            }

            return null;
        }

        private bool IsInsideDragArea(PointerEventData eventData)
        {
            if (dragArea == null || eventData == null)
                return true;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    dragArea,
                    eventData.position,
                    eventData.pressEventCamera,
                    out Vector2 localPoint))
                return false;

            return dragArea.rect.Contains(localPoint);
        }
    }
}
