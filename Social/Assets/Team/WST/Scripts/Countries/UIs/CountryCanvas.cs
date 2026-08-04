using Team.WST.Scripts.Countries.UIs.CountryDetailUIs;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs
{
    public class CountryCanvas : MonoBehaviour
    {
        [SerializeField] private CountryDetailCanvas countryDetailCanvas;
        [SerializeField] private CountryInformationCanvas countryInformationCanvas;
        [SerializeField] private GameObject countryDetailObject;
        [SerializeField] private GameObject countryInformationObject;

        private void Awake()
        {
            countryDetailCanvas.Init();
            countryInformationCanvas.Init();

            countryDetailCanvas.OnExitBtnClick += HandleExitBtnClick;
            countryInformationCanvas.OnMoreViewBtnClick += HandleMoreViewBtnClick;
        }

        private void OnDestroy()
        {
            countryInformationCanvas.OnMoreViewBtnClick -= HandleMoreViewBtnClick;
            countryDetailCanvas.OnExitBtnClick -= HandleExitBtnClick;
        }

        private void HandleMoreViewBtnClick(ICultureShowUI obj)
        {
            countryDetailObject.gameObject.SetActive(true);
            countryInformationObject.gameObject.SetActive(false);
        }
        
        private void HandleExitBtnClick()
        {
            countryDetailObject.gameObject.SetActive(false);
            countryInformationObject.gameObject.SetActive(true);
        }
    }
}