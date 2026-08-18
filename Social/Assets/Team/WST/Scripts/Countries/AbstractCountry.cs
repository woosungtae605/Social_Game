using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations;
using Team.WST.Scripts.Countries.Informations.Histories;
using Team.WST.Scripts.Countries.UIs;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    public abstract class AbstractCountry : MonoBehaviour, ICultureShowUI
    {
        [field: SerializeField] public CountrySO CountrySO { get; private set; }
        private Dictionary<CountryType, int> _countriesCulturePowerDict = new();
        private List<HistoryEventSO> _cultureHistories = new();
        private int _allCulturePower = 0;

        public string DisplayName => CountrySO.CountryName;
        public int AllCulturePower => _allCulturePower;
        public IReadOnlyDictionary<CountryType, int> CulturePowerDict => _countriesCulturePowerDict;
        public IReadOnlyList<HistoryEventSO> CultureHistory => _cultureHistories;
        public Sprite DisplaySprite => CountrySO.CountrySprite;
        public Color DisplayColor => CountrySO.CountryColor;
        public CountryType CountryType => CountrySO.CountryType;

        public virtual void Init()
        {
            AddCulturePower(CountrySO.CountryType, CountrySO.InitCulturalPower);
        }

        public virtual void AddCulturePower(CountryType countryType, int  power)
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

        public virtual void AddCultureHistory(HistoryEventSO historyHistorySo)
        {
            _cultureHistories.Add(historyHistorySo);
        }
    }
}