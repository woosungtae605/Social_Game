using UnityEngine;

namespace Team.WST.Scripts.Countries.Informations.Histories
{
    [CreateAssetMenu(fileName = "CultureEvent", menuName = "SO/History/CultureTrend", order = 0)]
    public class CultureTrendEventSO : HistoryEventSO
    {
        [field: SerializeField] public int Amount { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
        
        public override void Apply(AbstractCountry abstractCountry)
        {
            
        }

        public override bool CanApply(AbstractCountry abstractCountry)
        {
            return true;
        }
    }
}