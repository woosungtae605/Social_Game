using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs.CulturePowerNumberStatusFolder
{
    public class CulturePowerNumberStatus : MonoBehaviour
    {
        [SerializeField] private CulturePowerNumberStatusPrefab culturePowerNumberStatusPrefab;
        public void Show(IReadOnlyDictionary<CountryType, int> countries)
        {
            
        }
    }
}