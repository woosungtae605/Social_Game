using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations;
using Team.WST.Scripts.Countries.UIs;
using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    public abstract class AbstractCountry : MonoBehaviour, ICultureShowUI
    {
        [field: SerializeField] public CountrySO CountrySO { get; private set; }
        private Dictionary<CountryType, int> _countriesCulturePowerDict = new();
        private int _allCulturePower = 0;

        public string DisplayName => CountrySO.CountryName;
        public int AllCulturePower => _allCulturePower;
        public IReadOnlyDictionary<CountryType, int> CulturePowerDict => _countriesCulturePowerDict;
        public Sprite DisplaySprite => CountrySO.CountrySprite;
        public Color DisplayColor => CountrySO.CountryColor;

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
            _allCulturePower += power;
        }
    }
}