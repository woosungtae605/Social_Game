using System;
using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs
{
    public class ShowPercentUI : MonoBehaviour
    {
        [SerializeField] private CulturePercentElement showPercentUIPrefab;
        [SerializeField] private CountryManager countryManager;

        [Header("UI")] 
        [SerializeField] private GameObject showPercentUI;
        [SerializeField] private  TextMeshProUGUI allCulturePower;

        [Header("pooling")] 
        [SerializeField] private int initCount;

        private readonly Stack<CulturePercentElement> _poolStack = new();
        private readonly List<CulturePercentElement> _activeList = new();

        public void Init()
        {
            for (int i = 0; i < initCount; i++)
            {
                CreatePoolObject();
            }
        }

        private CulturePercentElement CreatePoolObject()
        {
            CulturePercentElement percentUI = Instantiate(showPercentUIPrefab, showPercentUI.transform);
            percentUI.gameObject.SetActive(false);

            _poolStack.Push(percentUI);
            return percentUI;
        }
        
        private CulturePercentElement GetPoolObject()
        {
            if (_poolStack.Count <= 0)
                CreatePoolObject();

            CulturePercentElement percentUI = _poolStack.Pop();
            percentUI.gameObject.SetActive(true);
            
            _activeList.Add(percentUI);

            return percentUI;
        }
        
        public void ReturnPoolObject(CulturePercentElement percentUI)
        {
            percentUI.gameObject.SetActive(false);
            _poolStack.Push(percentUI);
        }
        
        public void Show(IReadOnlyDictionary<CountryType, int> culturePowerDict)
        {
            Clear();

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

                CulturePercentElement percentUI = GetPoolObject();
                percentUI.SetData((float)pair.Value / totalPower, GetCountryColor(pair.Key));
            }
        }

        private void Clear()
        {
            for (int i = _activeList.Count - 1; i >= 0; i--)
            {
                ReturnPoolObject(_activeList[i]);
            }

            _activeList.Clear();
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