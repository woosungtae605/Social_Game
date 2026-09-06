using Team.WST.Scripts.Countries.Informations;

namespace Team.WST.Scripts.Countries
{
    public static class CultureRanking
    {
        public static int GetWorldPower(CountryManager manager, CountryType cultureType)
        {
            if (manager == null || manager.CountriesDict == null)
                return 0;

            int total = 0;
            foreach (AbstractCountry other in manager.CountriesDict.Values)
            {
                if (other == null || other.CulturePowerDict == null)
                    continue;

                if (other.CulturePowerDict.TryGetValue(cultureType, out int power))
                    total += power;
            }

            return total;
        }

        public static bool IsUniqueFirst(CountryManager manager, CountryType cultureType)
        {
            if (manager == null || manager.CountriesDict == null)
                return false;

            if (!manager.CountriesDict.ContainsKey(cultureType))
                return false;

            int selfPower = GetWorldPower(manager, cultureType);
            foreach (AbstractCountry other in manager.CountriesDict.Values)
            {
                if (other == null || other.CountryType == cultureType)
                    continue;

                if (GetWorldPower(manager, other.CountryType) >= selfPower)
                    return false;
            }

            return true;
        }
    }
}
