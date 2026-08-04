using System;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs.CountryInformationUIs
{
    public class CountryInformationCanvas : MonoBehaviour
    {
        [Header("UIs")]
        [SerializeField] private GameObject panelRoot;
        [SerializeField] private TextMeshProUGUI countryName;
        [SerializeField] private TextMeshProUGUI allCulturePower;
        [SerializeField] private Button moreViewBtn;
        
        [SerializeField] private ShowPercentUI showPercent;
        
        private ICultureShowUI _currentCultureShowUI;

        public event Action<ICultureShowUI> OnMoreViewBtnClick; 
        public void Init()
        {
            Bus<CultureSensorUIEvent>.OnEvent += HandleCultureSensorUI;
            moreViewBtn.onClick.AddListener(HandleMoreView);
            showPercent.Init();
        }
        
        private void OnDestroy()
        {
            Bus<CultureSensorUIEvent>.OnEvent -= HandleCultureSensorUI;
            moreViewBtn.onClick.RemoveListener(HandleMoreView);
        }

        private void HandleCultureSensorUI(CultureSensorUIEvent evt)
        {
            if (evt.ShowUI == null)
                return;

            _currentCultureShowUI = evt.ShowUI;
            allCulturePower.text = evt.ShowUI.AllCulturePower.ToString();
            countryName.text = evt.ShowUI.DisplayName;
            showPercent.Show(evt.ShowUI.CulturePowerDict);
        }
        
        private void HandleMoreView()
        {
            if(_currentCultureShowUI != null)
                OnMoreViewBtnClick?.Invoke(_currentCultureShowUI);
        }

        public void Show()
        {
            panelRoot.SetActive(true);
        }

        public void Hide()
        {
            panelRoot.SetActive(false);
        }
    }
}