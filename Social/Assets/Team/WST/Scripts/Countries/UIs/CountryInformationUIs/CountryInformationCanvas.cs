using System;
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
        
        public void Init()
        {
            moreViewBtn.onClick.AddListener(HandleMoreView);
            showPercent.Init();
        }
        
        private void OnDestroy()
        {
            moreViewBtn.onClick.RemoveListener(HandleMoreView);
        }
        
        private void HandleMoreView()
        { 
            OnMoreViewBtnClick?.Invoke();
        }

        public void Show(ICultureShowUI data)
        {
            countryName.text = data.DisplayName;
            allCulturePower.text = data.AllCulturePower.ToString();
            showPercent.Show(data.CulturePowerDict);
            gameObject.SetActive(true);
        }

        public void Hide() => gameObject.SetActive(false);
    }
}