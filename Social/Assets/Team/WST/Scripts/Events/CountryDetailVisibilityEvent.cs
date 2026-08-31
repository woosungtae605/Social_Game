using Team.WST.Scripts.CoreSystem;

namespace Team.WST.Scripts.Events
{
    public struct CountryDetailVisibilityEvent : IEvent
    {
        public readonly bool IsVisible;
        public CountryDetailVisibilityEvent(bool isVisible)
        {
            IsVisible = isVisible;
        }
    }
}