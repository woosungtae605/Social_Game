using System;
using System.Collections.Generic;
using System.Text;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    public static class CulturePortionTextBuilder
    {
        public static string Build(IReadOnlyList<CulturePortion> portions, Func<CountryType, string> countryName)
        {
            if (portions == null || portions.Count == 0)
                return "문화 정보 없음";

            var ordered = new List<CulturePortion>(portions.Count);
            for (int i = 0; i < portions.Count; i++)
            {
                if (portions[i].Percent <= 0f)
                    continue;

                ordered.Add(portions[i]);
            }

            if (ordered.Count == 0)
                return "문화 정보 없음";

            ordered.Sort((a, b) => b.Percent.CompareTo(a.Percent));

            var builder = new StringBuilder();
            for (int i = 0; i < ordered.Count; i++)
            {
                if (i > 0)
                    builder.Append('\n');

                string name = ResolveName(ordered[i].CultureType, countryName);
                builder.Append(name);
                builder.Append(" 문화 ");
                builder.Append(Mathf.RoundToInt(ordered[i].Percent));
                builder.Append('%');
            }

            return builder.ToString();
        }

        private static string ResolveName(CountryType cultureType, Func<CountryType, string> countryName)
        {
            if (countryName != null)
            {
                string resolved = countryName(cultureType);
                if (!string.IsNullOrEmpty(resolved))
                    return resolved;
            }

            return cultureType.ToString();
        }
    }
}
