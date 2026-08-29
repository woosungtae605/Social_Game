using System;
using System.Collections.Generic;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Histories;
using Team.WST.Scripts.Countries.Informations;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    public abstract class AbstractCountry : MonoBehaviour, ICultureShowUI
    {
        [field: SerializeField] public CountrySO CountrySO { get; private set; }
        
        private Dictionary<CountryType, int> _countriesCulturePowerDict = new();
        private List<HistoryEventSO> _cultureHistories = new();
        
        private int _allCulturePower = 0;
        
        public delegate void AddCulturePowerHandler(CountryType countryType, ref int power);
        public delegate void SpreadHandler(ref float radius, ref int amount);
        
        public event AddCulturePowerHandler OnAddCulturePower;
        public event SpreadHandler OnSpread;
        #region interface ICultureShowUI

        public string DisplayName => CountrySO.CountryName;
        public int AllCulturePower => _allCulturePower;
        public IReadOnlyDictionary<CountryType, int> CulturePowerDict => _countriesCulturePowerDict;
        public IReadOnlyList<HistoryEventSO> CultureHistory => _cultureHistories;
        public Sprite DisplaySprite => CountrySO.CountrySprite;
        public Color DisplayColor => CountrySO.CountryColor;
        public CountryType CountryType => CountrySO.CountryType;
        
        #endregion

        public void Init()
        {
            AddCulturePower(CountrySO.CountryType, CountrySO.InitCulturalPower);
        }

        public void AddCulturePower(CountryType countryType, int  power)
        {
            OnAddCulturePower?.Invoke(countryType, ref power);
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

        public void AddCultureHistory(HistoryEventSO historyHistorySo)
        {
            _cultureHistories.Add(historyHistorySo);
            historyHistorySo.Apply(this);
        }
        
        public void Spread()
        {
            Spread(CountrySO.SpreadRadius, CountrySO.SpreadAmount);
        }
        
        public void Spread(float radius, int amount)
        {
            OnSpread?.Invoke(ref radius, ref amount);
            Bus<CultureSpreadEvent>.RaiseEvent(new CultureSpreadEvent(this, radius, amount));
        }
    }
}