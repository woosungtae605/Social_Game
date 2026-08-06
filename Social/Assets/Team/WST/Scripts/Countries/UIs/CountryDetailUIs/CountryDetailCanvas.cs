using System;
using Team.WST.Scripts.Countries.UIs.CountryDetailUIs.CulturePowerNumberStatusFolder;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs
{
    public class CountryDetailCanvas : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private CulturePowerNumberStatus culturePowerNumberStatus;
        
        [Header("UIs")]
        [SerializeField] private TextMeshProUGUI countryName;
        [SerializeField] private Button exitBtn;

        public event Action OnExitBtnClick; 
        public void Init()
        {
            exitBtn.onClick.AddListener(HandleExitBtnClick);
            culturePowerNumberStatus.Init();
        }

        private void OnDestroy()
        {
            exitBtn.onClick.RemoveListener(HandleExitBtnClick);
        }

        private void HandleExitBtnClick()
        {
            OnExitBtnClick?.Invoke();
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show(ICultureShowUI iCultureShowUI) // not complete
        {
            countryName.text = iCultureShowUI.DisplayName;
            culturePowerNumberStatus.Show(iCultureShowUI.CulturePowerDict);
            gameObject.SetActive(true);
        }
    }
}