using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.UIs.CountryDetailUIs;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs
{
    public class CountryCanvas : MonoBehaviour
    {
        [SerializeField] private CountryDetailCanvas countryDetailCanvas;
        [SerializeField] private CountryInformationCanvas countryInformationCanvas;

        private ICultureShowUI _current;
        
        private void Awake()
        {
            countryDetailCanvas.Init();
            countryInformationCanvas.Init();

            Bus<CultureSensorUIEvent>.OnEvent += HandleCountryClicked;
            countryDetailCanvas.OnExitBtnClick += HandleExitBtnClick;
            countryInformationCanvas.OnMoreViewBtnClick += HandleMoreViewBtnClick;
        }

        private void OnDestroy()
        {
            Bus<CultureSensorUIEvent>.OnEvent -= HandleCountryClicked;
            countryDetailCanvas.OnExitBtnClick -= HandleExitBtnClick;
            countryInformationCanvas.OnMoreViewBtnClick -= HandleMoreViewBtnClick;
        }
        
        private void HandleCountryClicked(CultureSensorUIEvent evt)
        {
            if (evt.ShowUI == null)
                return;
            
            _current = evt.ShowUI; 
            countryInformationCanvas.Show(_current);
        }

        private void HandleMoreViewBtnClick()
        {
            if (_current == null)
                return;
            
            countryDetailCanvas.Show(_current);
            countryInformationCanvas.Hide();
        }
        
        private void HandleExitBtnClick()
        {
            countryDetailCanvas.Hide();
            if (_current != null)
                countryInformationCanvas.Show(_current);
        }
    }
}