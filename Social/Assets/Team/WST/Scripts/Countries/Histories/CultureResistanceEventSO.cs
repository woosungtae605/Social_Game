using System.Collections;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.Countries.Histories
{
    [CreateAssetMenu(fileName = "CultureResistance", menuName = "SO/History/CultureResistance", order = 1)]
    public class CultureResistanceEventSO : HistoryEventSO
    {
        [field: SerializeField] public int ReduceAmount { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float ReduceFactor { get; private set; } = 1f;
        [field: SerializeField] public int DurationTicks { get; private set; }
        [field: SerializeField] public bool RequireForeignCulture { get; private set; }

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

            country.OnAddCulturePower += HandleAddCulturePower;
            _countdown = country.StartCoroutine(Countdown());
        }

        public override void Revert(AbstractCountry country)
        {
            if (!_isActive)
                return;

            country.OnAddCulturePower -= HandleAddCulturePower;

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

            if (!RequireForeignCulture)
                return true;

            foreach (var pair in country.CulturePowerDict)
            {
                if (pair.Key != country.CountryType && pair.Value > 0)
                    return true;
            }

            return false;
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

        private void HandleAddCulturePower(CountryType countryType, ref int power)
        {
            if (_country == null || countryType == _country.CountryType)
                return;

            power = Mathf.RoundToInt(power * ReduceFactor) - ReduceAmount;
            if (power < 0)
                power = 0;
        }
    }
}
