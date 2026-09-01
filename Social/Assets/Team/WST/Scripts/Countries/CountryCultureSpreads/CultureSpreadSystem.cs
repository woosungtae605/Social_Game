using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Informations;
using Team.WST.Scripts.Countries.Interfaces;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.Countries.CountryCultureSpreads
{
    public class CultureSpreadSystem : MonoBehaviour
    {
        [SerializeField] private CountryManager countryManager;
        
        private void Awake()
        {
            Bus<CultureSpreadEvent>.OnEvent += HandleSpread;
        }
        private void OnDestroy()
        {
            Bus<CultureSpreadEvent>.OnEvent -= HandleSpread;
        }
        private void HandleSpread(CultureSpreadEvent evt)
        {
            if (evt.Source == null || countryManager == null)
                return;
            
            SpreadFrom(evt.Source, evt.Radius, evt.Amount);
        }
        private void SpreadFrom(AbstractCountry source, float radius, int amount)
        {
            if (amount <= 0 || radius <= 0f)
                return;
            
            Vector3 origin = source.transform.position;
            CountryType sourceType = source.CountryType;
            
            foreach (AbstractCountry other in countryManager.CountriesDict.Values)
            {
                if (other is not ICultureReceiver)
                    continue;

                float distance = Vector3.Distance(origin, other.transform.position);
                if (distance > radius)
                    continue;

                float ratio  = 1 - distance / radius;
                int appliedAmount = Mathf.RoundToInt(amount * ratio);
                if (appliedAmount <= 0)
                    continue;
                
                countryManager.AddCountryCulturePower(other.CountryType, sourceType, appliedAmount);
            }
        }
    }
}