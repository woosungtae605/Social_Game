using System.Collections.Generic;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Informations;
using TMPro;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.CountryInformationUIs
{
    public class ShowPercentUI : MonoBehaviour
    {
        [SerializeField] private CountryManager countryManager;

        [Header("UI")] 
        [SerializeField] private  TextMeshProUGUI allCulturePower;

        [Header("pooling")] 
        [SerializeField] private CulturePercentElement showPercentUIPrefab;
        [SerializeField] private Transform spawnPos;
        [SerializeField] private int initCount;
        
        private GenericObjectPool<CulturePercentElement> _pool;

        public void Init()
        {
            _pool = new GenericObjectPool<CulturePercentElement>(showPercentUIPrefab, spawnPos, initCount);
        }
        
        public void Show(IReadOnlyDictionary<CountryType, int> culturePowerDict)
        {
            _pool.Clear();

            int totalPower = 0;

            foreach (int power in culturePowerDict.Values)
            {
                if (power > 0)
                    totalPower += power;
            }
            
            allCulturePower.text = totalPower.ToString();

            if (totalPower <= 0)
                return;

            foreach (var pair in culturePowerDict)
            {
                if (pair.Value <= 0)
                    continue;

                CulturePercentElement percentUI = _pool.Get(); 
                percentUI.SetData((float)pair.Value / totalPower, GetCountryColor(pair.Key));
            }
        }

        private Color GetCountryColor(CountryType countryType)
        {
            if (countryManager.TryGetCountry(countryType, out var country))
            {
                return country.DisplayColor;
            }
            return Color.white;
        }
    }
}