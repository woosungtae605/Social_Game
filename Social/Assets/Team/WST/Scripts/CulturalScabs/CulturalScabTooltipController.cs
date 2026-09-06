using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries;
using Team.WST.Scripts.Countries.Informations;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    public class CulturalScabTooltipController : MonoBehaviour
    {
        [SerializeField] private CulturalScabTooltipView view;
        [SerializeField] private CountryManager countryManager;

        private CulturalScab current;

        private void OnEnable()
        {
            Bus<CulturalScabHoveredEvent>.OnEvent += HandleHovered;
            Bus<CulturalScabUnhoveredEvent>.OnEvent += HandleUnhovered;
            Bus<CulturalScabDespawnedEvent>.OnEvent += HandleDespawned;
        }

        private void OnDisable()
        {
            Bus<CulturalScabHoveredEvent>.OnEvent -= HandleHovered;
            Bus<CulturalScabUnhoveredEvent>.OnEvent -= HandleUnhovered;
            Bus<CulturalScabDespawnedEvent>.OnEvent -= HandleDespawned;
            Clear();
        }

        private void LateUpdate()
        {
            if (current == null)
                return;

            if (!current.isActiveAndEnabled || !current.gameObject.activeInHierarchy)
            {
                Clear();
                return;
            }

            BindCurrent();
        }

        private void HandleHovered(CulturalScabHoveredEvent evt)
        {
            current = evt.Scab;
            BindCurrent();
        }

        private void HandleUnhovered(CulturalScabUnhoveredEvent evt)
        {
            if (evt.Scab != current)
                return;

            Clear();
        }

        private void HandleDespawned(CulturalScabDespawnedEvent evt)
        {
            if (evt.Scab != current)
                return;

            Clear();
        }

        private void BindCurrent()
        {
            if (view == null || current == null)
                return;

            view.Show(CulturePortionTextBuilder.Build(current.Cultures, ResolveName));
            view.Follow(current.transform as RectTransform);
        }

        private void Clear()
        {
            current = null;
            if (view != null)
                view.Hide();
        }

        private string ResolveName(CountryType cultureType)
        {
            if (countryManager != null && countryManager.TryGetCountry(cultureType, out AbstractCountry country))
                return country.DisplayName;

            return cultureType.ToString();
        }
    }
}
