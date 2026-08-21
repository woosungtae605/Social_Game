using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations.Histories;
using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    public class ActiveHistoryController : MonoBehaviour
    {
        private List<HistoryEventSO> _activeCultureHistories = new();
        private List<HistoryEventSO> _cultureHistories = new();
        
        public List<HistoryEventSO> ActiveCultureHistories => _activeCultureHistories;
        public List<HistoryEventSO> CultureHistories => _cultureHistories;

        public void RaiseCultureHistoryEvent(HistoryEventSO historyEventSO)
        {
            _activeCultureHistories.Add(historyEventSO);
            _cultureHistories.Add(historyEventSO);
        }
    }
}