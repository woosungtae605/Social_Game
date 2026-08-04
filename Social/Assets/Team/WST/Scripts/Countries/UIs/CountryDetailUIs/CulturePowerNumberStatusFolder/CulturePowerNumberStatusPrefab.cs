using TMPro;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs.CulturePowerNumberStatusFolder
{
    public class CulturePowerNumberStatusPrefab : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countryNameTxt;
        [SerializeField] private TextMeshProUGUI culturePowerTxt;
        
        public void SetCountryName(string countryName)
        {
            countryNameTxt.text = countryName;
        }
        
        public void SetCulturePower(int culturePower)
        {
            culturePowerTxt.text = culturePower.ToString();
        }
    }
}