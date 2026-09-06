using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    public static class SharePostCultureRecipe
    {
        public static List<CulturePortion> Build(CountryType originCountry, int kind)
        {
            float koreaPercent = Mathf.Clamp(kind, 0, 100);
            float originPercent = 100f - koreaPercent;

            var portions = new List<CulturePortion>();
            if (originCountry == CountryType.KOREA || originPercent <= 0f)
            {
                portions.Add(new CulturePortion(CountryType.KOREA, 100f));
                return CulturePortion.Normalized(portions);
            }

            if (koreaPercent <= 0f)
            {
                portions.Add(new CulturePortion(originCountry, 100f));
                return CulturePortion.Normalized(portions);
            }

            portions.Add(new CulturePortion(originCountry, originPercent));
            portions.Add(new CulturePortion(CountryType.KOREA, koreaPercent));
            return CulturePortion.Normalized(portions);
        }
    }
}
