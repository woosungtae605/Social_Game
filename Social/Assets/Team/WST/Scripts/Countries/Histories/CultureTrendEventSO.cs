using System.Collections;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.Countries.Histories
{
    [CreateAssetMenu(fileName = "CultureEvent", menuName = "SO/History/CultureTrend", order = 0)]
    public class CultureTrendEventSO : HistoryEventSO
    {
        [field: SerializeField] public int Amount { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public int DurationTicks { get; private set; }
        
        private AbstractCountry _country;
        private int _remaining;
        private Coroutine _countdown;

        public override void Apply(AbstractCountry country)
        {
            _country = country;
            _remaining = DurationTicks;

            country.OnSpread += HandleSpread;
            _countdown = country.StartCoroutine(Countdown());
        }

        public override void Revert(AbstractCountry country)
        {
            country.OnSpread -= HandleSpread;
            
            if (_countdown != null)
            {
                country.StopCoroutine(_countdown);
                _countdown = null;
            }
        }

        public override bool CanApply(AbstractCountry country)
        {
            return true;
        }
        
        private IEnumerator Countdown()
        {
            while (_remaining > 0)
            {
                yield return new WaitForSeconds(1f);
                _remaining--;
            }
            Revert(_country);
        }
        
        private void HandleSpread(ref float radius, ref int amount)
        {
            radius += Radius;
            amount +=  Amount;
        }

    }
}