using System.Collections;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.Countries.Histories
{
    [CreateAssetMenu(fileName = "CultureIsolation", menuName = "SO/History/CultureIsolation", order = 2)]
    public class CultureIsolationEventSO : HistoryEventSO
    {
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }
        [field: SerializeField] public int DurationTicks { get; private set; }
        [field: SerializeField] public int MinOwnCulturePower { get; private set; }

        private AbstractCountry _country;
        private int _remaining;
        private Coroutine _countdown;
        private bool _isActive;

        public override bool IsActive => _isActive;

        public override void Apply(AbstractCountry country)
        {
            _country = country;
            _remaining = DurationTicks;
            _isActive = true;

            country.OnSpread += HandleSpread;
            _countdown = country.StartCoroutine(Countdown());
        }

        public override void Revert(AbstractCountry country)
        {
            if (!_isActive)
                return;

            country.OnSpread -= HandleSpread;

            if (_countdown != null)
            {
                country.StopCoroutine(_countdown);
                _countdown = null;
            }

            _isActive = false;
            _country = null;
        }

        public override bool CanApply(AbstractCountry country)
        {
            if (IsAlreadyActiveOn(country))
                return false;

            if (MinOwnCulturePower <= 0)
                return true;

            return country.CulturePowerDict.TryGetValue(country.CountryType, out int ownPower)
                   && ownPower >= MinOwnCulturePower;
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
            radius = Mathf.Max(0f, radius - Radius);
            amount = Mathf.Max(0, amount - Amount);
        }
    }
}
