using System.Collections.Generic;
using System.Linq;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    public class CountryManager : MonoBehaviour
    {
        private Dictionary<CountryType, AbstractCountry> _countriesDict;
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _countriesDict = GetComponentsInChildren<AbstractCountry>().ToDictionary(country => country.CountrySO.CountryType);

            foreach (AbstractCountry country in _countriesDict.Values)
            {
                country.Init();
            }
        }
        
        public bool TryGetCountry(CountryType countryType, out AbstractCountry abtractCountry)
        {
            abtractCountry = null;

            if (!_countriesDict.TryGetValue(countryType, out AbstractCountry country))
                return false;

            abtractCountry = country;
            return abtractCountry != null;
        }
    }
}