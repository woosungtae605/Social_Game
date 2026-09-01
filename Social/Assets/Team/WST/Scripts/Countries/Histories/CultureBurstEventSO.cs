using UnityEngine;

namespace Team.WST.Scripts.Countries.Histories
{
    [CreateAssetMenu(fileName = "CultureBurst", menuName = "SO/History/CultureBurst", order = 3)]
    public class CultureBurstEventSO : HistoryEventSO
    {
        [field: SerializeField] public int Amount { get; private set; }

        public override void Apply(AbstractCountry country)
        {
            country.AddCulturePower(country.CountryType, Amount);
        }

        public override void Revert(AbstractCountry country)
        {
        }

        public override bool CanApply(AbstractCountry country)
        {
            return Amount != 0;
        }
    }
}
