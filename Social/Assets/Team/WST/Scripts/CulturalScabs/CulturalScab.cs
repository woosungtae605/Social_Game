using System.Collections.Generic;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Informations;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    public class CulturalScab : MonoBehaviour
    {
        [field: SerializeField]
        public float Uniqueness { get; private set; } = 50f;

        [SerializeField] private List<CulturePortion> cultures = new List<CulturePortion>();

        public IReadOnlyList<CulturePortion> Cultures => cultures;

        public void Initialize(float uniqueness, IReadOnlyList<CulturePortion> culturePortions)
        {
            Uniqueness = uniqueness;
            cultures = CulturePortion.Normalized(culturePortions);
            SyncView();
        }

        public void SetUniqueness(float uniqueness)
        {
            Uniqueness = uniqueness;
            SyncView();
        }

        public void Absorb(CulturalScab other, float newUniqueness)
        {
            if (other == null)
                return;

            cultures = CulturePortion.Blend(cultures, Uniqueness, other.Cultures, other.Uniqueness);
            Uniqueness = newUniqueness;
            SyncView();
        }

        public float PercentOf(CountryType cultureType)
        {
            return CulturePortion.PercentOf(cultures, cultureType);
        }

        private void SyncView()
        {
            var view = GetComponent<JJM.Scripts.UIDraggable>();
            if (view != null)
                view.ApplyUniqueness(Uniqueness);
        }

        private void OnDestroy()
        {
            Bus<CulturalScabDespawnedEvent>.RaiseEvent(new CulturalScabDespawnedEvent(this));
        }
    }
}
