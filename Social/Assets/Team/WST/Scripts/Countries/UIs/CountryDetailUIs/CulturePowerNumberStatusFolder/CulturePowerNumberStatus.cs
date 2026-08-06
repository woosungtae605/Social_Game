using System.Collections.Generic;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Informations;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs.CulturePowerNumberStatusFolder
{
    public class CulturePowerNumberStatus : MonoBehaviour
    {
        [Header("pooling")]
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private CulturePowerNumberStatusPrefab culturePowerNumberStatusPrefab;
        [SerializeField] private int initCount;
        
        private CountryManager _countryManager;
        private GenericObjectPool<CulturePowerNumberStatusPrefab> _pool;

        public void Init(CountryManager countryManager)
        {
            _countryManager = countryManager;
            _pool = new GenericObjectPool<CulturePowerNumberStatusPrefab>(culturePowerNumberStatusPrefab, spawnTransform, initCount);
        }
        
        public void Show(ICultureShowUI iCultureShowUI)
        {
            var countries =  iCultureShowUI.CulturePowerDict;
            _pool.Clear();
            
            foreach (CountryType country in countries.Keys)
            {
                if (!_countryManager.TryGetCountry(country, out var countryInfo))
                    continue;
                
                CulturePowerNumberStatusPrefab culturePrefab = _pool.Get();
                culturePrefab.SetCountryName(countryInfo.DisplayName);
                culturePrefab.SetCulturePower(countries[country]);
            }
        }
    }
}