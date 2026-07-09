using System;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs
{
    public class CountryCanvas : MonoBehaviour
    {
        private void Awake()
        {
            Bus<CultureSensorUIEvent>.OnEvent += HandleCultureSensorUI;
        }

        private void OnDestroy()
        {
            Bus<CultureSensorUIEvent>.OnEvent -= HandleCultureSensorUI;
        }

        private void HandleCultureSensorUI(CultureSensorUIEvent evt)
        {
            
        }
    }
}