using System.Collections.Generic;
using System.Linq;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Histories;
using Team.WST.Scripts.Countries.Informations;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    public class CountryManager : MonoBehaviour
    {
        private Dictionary<CountryType, AbstractCountry> _countriesDict;
        public IReadOnlyDictionary<CountryType, AbstractCountry> CountriesDict => _countriesDict;
        
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
        
        public void AddCountryCulturePower(CountryType targetCountryType, CountryType addCountryType, int power)
        {
            if (TryGetCountry(targetCountryType, out AbstractCountry country))
            {
                country.AddCulturePower(addCountryType, power);
            }
        }

        public void AddCountryHistory(CountryType countryType, HistoryEventSO history)
        {
            if (TryGetCountry(countryType, out AbstractCountry country))
            {
                Bus<AddHistoryEvent>.RaiseEvent(new AddHistoryEvent(history));
                country.AddCultureHistory(history);
            }
        }
        
        public bool TryGetCountry(CountryType countryType, out AbstractCountry abstractCountry)
        {
            abstractCountry = null;

            if (!_countriesDict.TryGetValue(countryType, out AbstractCountry country))
                return false;

            abstractCountry = country;
            return abstractCountry != null;
        }
    }
}