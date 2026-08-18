using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    public class BaseCountry : AbstractCountry
    {
        [SerializeField] private CountriesPolicy countriesPolicy;
        public override void AddCulturePower(CountryType countryType, int power)
        {
            float receptiveness = 1f;
            if (countriesPolicy != null)
                receptiveness = countriesPolicy.GetReceptiveness(countryType);
            int applied = Mathf.RoundToInt(power * receptiveness);
            if (applied == 0)
                return;
            
            base.AddCulturePower(countryType, applied);
        }
    }
}