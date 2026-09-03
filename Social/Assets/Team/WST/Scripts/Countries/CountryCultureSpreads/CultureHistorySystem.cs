using System.Collections.Generic;
using Team.WST.Scripts.Countries.Histories;
using UnityEngine;

namespace Team.WST.Scripts.Countries.CountryCultureSpreads
{
    public class CultureHistorySystem : MonoBehaviour
    {
        [SerializeField] private CountryManager countryManager;
        [SerializeField] private float interval = 10f;
        
        private float _elapsed;
        
        private readonly List<AbstractCountry> _countryCandidates = new();
        private readonly List<HistoryEventSO> _eventCandidates = new();
        
        private void Update()
        {
            _elapsed += Time.deltaTime;
            if (_elapsed < interval)
                return;
            
            _elapsed = 0f;
            
            if (!TryPickCountry(out AbstractCountry country))
                return;
            
            if (!TryPickEvent(country, out HistoryEventSO history))
                return;
            
            countryManager.AddCountryHistory(country.CountryType, history);
        }
        
        private bool TryPickCountry(out AbstractCountry country)
        {
            country = null;
            _countryCandidates.Clear();
            
            var dict = countryManager.CountriesDict;
            if (dict == null || dict.Count == 0)
                return false;
            
            foreach (AbstractCountry c in dict.Values)
            {
                if (HasApplicableEvent(c))
                    _countryCandidates.Add(c);
            }
            
            if (_countryCandidates.Count == 0)
                return false;
            
            country = _countryCandidates[Random.Range(0, _countryCandidates.Count)];
            return true;
        }
        
        private bool TryPickEvent(AbstractCountry country, out HistoryEventSO history)
        {
            history = null;
            _eventCandidates.Clear();
            
            HistoryEventSO[] pool = country.CountrySO.CultureHistoryEvents;
            
            if (pool == null || pool.Length == 0)
                return false;
            
            foreach (HistoryEventSO candidate in pool)
            {
                if (candidate != null && candidate.CanApply(country))
                    _eventCandidates.Add(candidate);
            }
            
            if (_eventCandidates.Count == 0)
                return false;
            
            history = _eventCandidates[Random.Range(0, _eventCandidates.Count)];
            return true;
        }
        
        private static bool HasApplicableEvent(AbstractCountry country)
        {
            HistoryEventSO[] pool = country.CountrySO.CultureHistoryEvents;
            
            if (pool == null || pool.Length == 0)
                return false;
            
            foreach (HistoryEventSO candidate in pool)
            {
                if (candidate != null && candidate.CanApply(country))
                    return true;
            }
            return false;
        }
    }
}