using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs
{
    public class ShowPercentUI : MonoBehaviour
    {
        [SerializeField] private GameObject showPercentUIPrefab;
        [SerializeField] private CountryManager countryManager;

        [Header("UI")]
        [SerializeField] private  TextMeshProUGUI allCulturePower;
        
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

                GameObject percentUI = Instantiate(showPercentUIPrefab, transform);

                LayoutElement layoutElement = percentUI.GetComponent<LayoutElement>();

                layoutElement.minWidth = 0f;
                layoutElement.preferredWidth = 0f;
                layoutElement.flexibleWidth = (float)pair.Value / totalPower;

                Image image = percentUI.GetComponent<Image>();
                if (image == null)
                    image = percentUI.AddComponent<Image>();

                image.color = GetCountryColor(pair.Key);
            }
        }

        private void Clear()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
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