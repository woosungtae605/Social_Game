using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Histories;
using Team.WST.Scripts.Countries.Informations;

namespace Team.WST.Scripts.Events
{
    public struct AddHistoryEvent : IEvent
    {
        public readonly HistoryEventSO HistoryEvent;
        public readonly CountryType CountryType;

        
        public AddHistoryEvent(HistoryEventSO historyEvent, CountryType countryType)
        {
            HistoryEvent = historyEvent;
            CountryType = countryType;
        }
    }
}