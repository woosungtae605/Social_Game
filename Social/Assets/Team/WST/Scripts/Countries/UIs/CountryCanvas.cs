using System;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Events;
using TMPro;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs
{
    public class CountryCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countryName;
        [SerializeField] private TextMeshProUGUI allCulturePower;
        [SerializeField] private ShowPercentUI showPercent;
        private void Awake()
        {
            Bus<CultureSensorUIEvent>.OnEvent += HandleCultureSensorUI;
            showPercent.Init();
        }
        
        private void OnDestroy()
        {
            Bus<CultureSensorUIEvent>.OnEvent -= HandleCultureSensorUI;
        }

        private void HandleCultureSensorUI(CultureSensorUIEvent evt)
        {
            if (evt.ShowUI == null)
                return;

            allCulturePower.text = evt.ShowUI.AllCulturePower.ToString();
            countryName.text = evt.ShowUI.DisplayName;
            showPercent.Show(evt.ShowUI.CulturePowerDict);
        }
    }
}