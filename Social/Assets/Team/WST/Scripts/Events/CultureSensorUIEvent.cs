using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.UIs;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;

namespace Team.WST.Scripts.Events
{
    public struct CultureSensorUIEvent : IEvent
    {
        public readonly ICultureShowUI ShowUI;
        
        public CultureSensorUIEvent(ICultureShowUI showUI)
        {
            ShowUI = showUI;
        }
    }
}