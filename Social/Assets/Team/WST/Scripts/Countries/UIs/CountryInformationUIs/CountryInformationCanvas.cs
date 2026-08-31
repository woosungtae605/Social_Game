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
        [SerializeField] private TextMeshProUGUI countryName;
        [SerializeField] private TextMeshProUGUI allCulturePower;
        [SerializeField] private Button moreViewBtn;
        
        [SerializeField] private ShowPercentUI showPercent;
        public event Action OnMoreViewBtnClick;

        private ICultureShowUI _current;
        
        public void Init()
        {
            moreViewBtn.onClick.AddListener(HandleMoreView);
            showPercent.Init();
            Bus<CulturePowerChangedEvent>.OnEvent += HandleCulturePowerChanged;
        }
        
        private void OnDestroy()
        {
            moreViewBtn.onClick.RemoveListener(HandleMoreView);
            Bus<CulturePowerChangedEvent>.OnEvent -= HandleCulturePowerChanged;
        }
        
        private void HandleMoreView()
        { 
            OnMoreViewBtnClick?.Invoke();
        }

        public void Show(ICultureShowUI data)
        {
            _current = data;
            countryName.text = data.DisplayName;
            allCulturePower.text = data.AllCulturePower.ToString();
            showPercent.Show(data.CulturePowerDict);
            gameObject.SetActive(true);
        }

        public void Hide() => gameObject.SetActive(false);

        private void HandleCulturePowerChanged(CulturePowerChangedEvent evt)
        {
            if (!gameObject.activeSelf || _current == null)
                return;
            if (evt.TargetCountryType != _current.CountryType)
                return;

            Show(_current);
        }
    }
}