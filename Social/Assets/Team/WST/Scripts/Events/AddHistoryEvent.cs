using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Histories;

namespace Team.WST.Scripts.Events
{
    public struct AddHistoryEvent : IEvent
    {
        public readonly HistoryEventSO HistoryEvent;
        
        public AddHistoryEvent(HistoryEventSO historyEvent)
        {
            HistoryEvent = historyEvent;
        }
    }
}