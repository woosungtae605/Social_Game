namespace Team.WST.Scripts.Countries
{
    public class BaseCountry : AbstractCountry
    {
        public override void Init()
        {
            AddCulturePower(CountrySO.CountryType, CountrySO.InitCulturalPower);
        }
    }
}