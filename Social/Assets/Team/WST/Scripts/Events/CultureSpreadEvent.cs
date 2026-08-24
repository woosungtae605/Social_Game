using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries;

namespace Team.WST.Scripts.Events
{
    public struct CultureSpreadEvent : IEvent
    {
        public readonly AbstractCountry Source;
        public readonly float Radius;
        public readonly int Amount;
        
        public CultureSpreadEvent(AbstractCountry source, float radius, int amount)
        {
            Source = source;
            Radius = radius;
            Amount = amount;
        }
    }
}