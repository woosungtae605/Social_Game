using System.Collections.Generic;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs.CulturePowerNumberStatusFolder
{
    public class CulturePowerNumberStatus : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CountryManager countryManager;

        [Header("pooling")]
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private CulturePowerNumberStatusPrefab culturePowerNumberStatusPrefab;
        [SerializeField] private int initCount;
        
        private GenericObjectPool<CulturePowerNumberStatusPrefab> _pool;

        public void Init()
        {
            _pool = new GenericObjectPool<CulturePowerNumberStatusPrefab>(culturePowerNumberStatusPrefab, spawnTransform, initCount);
        }
        
        public void Show(IReadOnlyDictionary<CountryType, int> countries)
        {
            _pool.Clear();
            
            foreach (CountryType country in countries.Keys)
            {
                if (!countryManager.TryGetCountry(country, out var countryInfo))
                    continue;
                
                CulturePowerNumberStatusPrefab culturePrefab = _pool.Get();
                culturePrefab.SetCountryName(countryInfo.DisplayName);
                culturePrefab.SetCulturePower(countries[country]);
            }
        }
    }
}