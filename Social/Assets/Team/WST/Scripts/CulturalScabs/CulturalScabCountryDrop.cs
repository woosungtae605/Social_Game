using JJM.Scripts;
using Team.WST.Scripts.Countries;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Team.WST.Scripts.CulturalScabs
{
    public class CulturalScabCountryDrop : MonoBehaviour
    {
        [SerializeField] private CulturalScab scab;

        private CountryManager countryManager;
        private RectTransform dragArea;
        private UIDraggable view;

        private void Awake()
        {
            if (scab == null)
                scab = GetComponent<CulturalScab>();

            view = GetComponent<UIDraggable>();
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
                Destroy(gameObject);
                return true;
            }

            if (view != null)
                view.ClampToDragArea();

            return true;
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
