using System;
using System.Collections.Generic;
using Team.WST.Scripts.Countries.Histories;
using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    public class ActiveHistoryController : MonoBehaviour
    {
        private List<HistoryEventSO> _activeCultureHistories = new();
        private List<HistoryEventSO> _cultureHistories = new();
        
        public List<HistoryEventSO> ActiveCultureHistories => _activeCultureHistories;
        public List<HistoryEventSO> CultureHistories => _cultureHistories;
        
        private Dictionary<HistoryEventSO, int> _historiesEventDurationDict = new();
        
        public void RaiseCultureHistoryEvent(HistoryEventSO historyEventSO)
        {
            _activeCultureHistories.Add(historyEventSO);
            _cultureHistories.Add(historyEventSO);
            
            if(_historiesEventDurationDict.ContainsKey(historyEventSO))
                _historiesEventDurationDict[historyEventSO] = 1;
        }

        private void Update()
        {
            
        }
    }
}