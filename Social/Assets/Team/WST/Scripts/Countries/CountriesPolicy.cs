using System;
using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    public class CountriesPolicy : MonoBehaviour
    {
        private Dictionary<CountryType, float>  _countriesPolicyDict = new();
        
        public IReadOnlyDictionary<CountryType, float> CountriesPolicyDictDict => _countriesPolicyDict;
        
        public float GetReceptiveness(CountryType senderType)
        {
            if (_countriesPolicyDict.TryGetValue(senderType, out float value))
                return value;
            
            return 1f;
        }
        
        public void AddReceptiveness(CountryType senderType, float amount)
        {
            float next = GetReceptiveness(senderType) + amount;
            _countriesPolicyDict[senderType] = Mathf.Max(0f, next);
        }
    }
}