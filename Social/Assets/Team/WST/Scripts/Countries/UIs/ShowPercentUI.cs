using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs
{
    public class ShowPercentUI : MonoBehaviour
    {
        [SerializeField] private GameObject showPercentUIPrefab;
        [SerializeField] private CountrySO[] countrySOs;

        public void Show(IReadOnlyDictionary<CountryType, int> culturePowerDict)
        {
            Clear();

            if (showPercentUIPrefab == null || culturePowerDict == null)
                return;

            int totalPower = 0;

            foreach (int power in culturePowerDict.Values)
            {
                if (power > 0)
                    totalPower += power;
            }

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
            foreach (CountrySO countrySO in countrySOs)
            {
                if (countrySO != null && countrySO.CountryType == countryType)
                    return countrySO.CountryColor;
            }

            return Color.white;
        }
    }
}