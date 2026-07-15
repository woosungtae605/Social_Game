using System;
using Team.WST.Scripts.Countries.UIs.CountryDetailUIs;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs
{
    public class CountryCanvas : MonoBehaviour
    {
        [SerializeField] private CountryDetailCanvas countryDetailCanvas;
        [SerializeField] private CountryInformationCanvas countryInformationCanvas;

        private void Awake()
        {
            countryInformationCanvas.Init();
        }
    }
}