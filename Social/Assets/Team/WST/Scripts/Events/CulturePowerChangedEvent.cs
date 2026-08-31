using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Informations;

namespace Team.WST.Scripts.Events
{
    public struct CulturePowerChangedEvent : IEvent
    {
        public readonly CountryType TargetCountryType;
        public readonly CountryType AddedCultureType;
        public readonly int Power;

        public CulturePowerChangedEvent(CountryType targetCountryType, CountryType addedCultureType, int power)
        {
            TargetCountryType = targetCountryType;
            AddedCultureType = addedCultureType;
            Power = power;
        }
    }
}
