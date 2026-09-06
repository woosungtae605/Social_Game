using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Team.WST.Scripts.CulturalScabs
{
    public class CulturalScabHoverSensor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CulturalScab scab;

        private void Awake()
        {
            if (scab == null)
                scab = GetComponent<CulturalScab>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (scab == null)
                return;

            Bus<CulturalScabHoveredEvent>.RaiseEvent(new CulturalScabHoveredEvent(scab));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (scab == null)
                return;

            Bus<CulturalScabUnhoveredEvent>.RaiseEvent(new CulturalScabUnhoveredEvent(scab));
        }
    }
}
