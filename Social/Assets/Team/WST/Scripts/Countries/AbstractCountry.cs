using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    public abstract class AbstractCountry : MonoBehaviour
    {
        [field: SerializeField] public CountrySO CountrySO { get; private set; }
        private Dictionary<CountryType, int> _countriesCulturePowerDict = new();

        public void Init()
        {
            AddCulturePower(CountrySO.CountryType, CountrySO.InitCulturalPower);
        }

        public void AddCulturePower(CountryType countryType, int  power)
        {
            if (_countriesCulturePowerDict.ContainsKey(countryType))
            {
                _countriesCulturePowerDict[countryType] += power;
            }
            else
            {
                _countriesCulturePowerDict[countryType] = power;
            }
        }
    }
}