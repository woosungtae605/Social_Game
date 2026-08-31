using System;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.UIs.CountryDetailUIs.CultureHistoryUIs;
using Team.WST.Scripts.Countries.UIs.CountryDetailUIs.CulturePowerNumberStatusFolder;
using Team.WST.Scripts.Countries.UIs.CountryDetailUIs.PieUIFolder;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using Team.WST.Scripts.Events;
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
        [SerializeField] private CultureHistoryStatus cultureHistoryStatus;
        [SerializeField] private PieGraphUI pieGraphUI;
        [SerializeField] private Image countryFlagImage;
        
        [Header("UIs")]
        [SerializeField] private TextMeshProUGUI countryName;
        [SerializeField] private Button exitBtn;

        public event Action OnExitBtnClick; 
        public void Init()
        {
            exitBtn.onClick.AddListener(HandleExitBtnClick);
            culturePowerNumberStatus.Init(countryManager);
            cultureHistoryStatus.Init();
            pieGraphUI.Init();
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
            Bus<CountryDetailVisibilityEvent>.RaiseEvent(new CountryDetailVisibilityEvent(false));
        }

        public void Show(ICultureShowUI iCultureShowUI) // not complete
        {
            countryName.text = iCultureShowUI.DisplayName;
            culturePowerNumberStatus.Show(iCultureShowUI);
            cultureHistoryStatus.Show(iCultureShowUI);
            pieGraphUI.Show(iCultureShowUI);
            countryFlagImage.sprite = iCultureShowUI.DisplaySprite;
            gameObject.SetActive(true);
            Bus<CountryDetailVisibilityEvent>.RaiseEvent(new CountryDetailVisibilityEvent(true));
        }
    }
}