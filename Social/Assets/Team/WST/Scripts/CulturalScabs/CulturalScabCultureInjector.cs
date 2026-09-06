using Team.WST.Scripts.Countries;
using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    public static class CulturalScabCultureInjector
    {
        public static bool Inject(CountryManager countryManager, AbstractCountry target, CulturalScab scab)
        {
            if (countryManager == null || target == null || scab == null)
                return false;

            float uniqueness = scab.Uniqueness;
            var portions = scab.Cultures;
            for (int i = 0; i < portions.Count; i++)
            {
                int power = Mathf.RoundToInt(uniqueness * portions[i].Percent / 100f);
                if (power == 0)
                    continue;

                countryManager.AddCountryCulturePower(
                    target.CountryType,
                    portions[i].CultureType,
                    power);
            }

            return true;
        }
    }
}
