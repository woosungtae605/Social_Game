using System;
using Team.WST.Scripts.Countries.UIs.CountryDetailUIs.CulturePowerNumberStatusFolder;
using Team.WST.Scripts.Countries.UIs.CountryDetailUIs.PieUIFolder;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs
{
    public class CountryDetailCanvas : MonoBehaviour
    {
        [SerializeField] private CountryManager countryManager;
            
        [Header("UI References")]
        [SerializeField] private CulturePowerNumberStatus culturePowerNumberStatus;
        [SerializeField] private PieGraphUI pieGraphUI;
        
        [Header("UIs")]
        [SerializeField] private TextMeshProUGUI countryName;
        [SerializeField] private Button exitBtn;

        public event Action OnExitBtnClick; 
        public void Init()
        {
            exitBtn.onClick.AddListener(HandleExitBtnClick);
            culturePowerNumberStatus.Init(countryManager);
            pieGraphUI.Init(countryManager);
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
            culturePowerNumberStatus.Show(iCultureShowUI);
            pieGraphUI.Show(iCultureShowUI);
            gameObject.SetActive(true);
        }
    }
}