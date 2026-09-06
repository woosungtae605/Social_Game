using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    [System.Serializable]
    public struct CulturePortion
    {
        [SerializeField] private CountryType cultureType;
        [SerializeField] private float percent;

        public CountryType CultureType => cultureType;
        public float Percent => percent;

        public CulturePortion(CountryType cultureType, float percent)
        {
            this.cultureType = cultureType;
            this.percent = Mathf.Max(0f, percent);
        }

        public static List<CulturePortion> Normalized(IReadOnlyList<CulturePortion> source)
        {
            var result = new List<CulturePortion>();
            if (source == null || source.Count == 0)
                return result;

            float total = 0f;
            for (int i = 0; i < source.Count; i++)
                total += Mathf.Max(0f, source[i].Percent);

            if (total <= 0f)
            {
                float even = 100f / source.Count;
                for (int i = 0; i < source.Count; i++)
                    AddOrMerge(result, source[i].CultureType, even);
                return result;
            }

            for (int i = 0; i < source.Count; i++)
            {
                float share = Mathf.Max(0f, source[i].Percent) / total * 100f;
                AddOrMerge(result, source[i].CultureType, share);
            }

            return result;
        }

        public static List<CulturePortion> Blend(
            IReadOnlyList<CulturePortion> first,
            float firstWeight,
            IReadOnlyList<CulturePortion> second,
            float secondWeight)
        {
            var mixed = new List<CulturePortion>();
            Accumulate(mixed, first, Mathf.Max(0f, firstWeight));
            Accumulate(mixed, second, Mathf.Max(0f, secondWeight));
            return Normalized(mixed);
        }

        public static float PercentOf(IReadOnlyList<CulturePortion> portions, CountryType cultureType)
        {
            if (portions == null)
                return 0f;

            for (int i = 0; i < portions.Count; i++)
            {
                if (portions[i].CultureType == cultureType)
                    return portions[i].Percent;
            }

            return 0f;
        }

        private static void Accumulate(List<CulturePortion> target, IReadOnlyList<CulturePortion> source, float weight)
        {
            if (source == null || weight <= 0f)
                return;

            for (int i = 0; i < source.Count; i++)
                AddOrMerge(target, source[i].CultureType, source[i].Percent * weight);
        }

        private static void AddOrMerge(List<CulturePortion> target, CountryType cultureType, float percent)
        {
            for (int i = 0; i < target.Count; i++)
            {
                if (target[i].CultureType != cultureType)
                    continue;

                target[i] = new CulturePortion(cultureType, target[i].Percent + percent);
                return;
            }

            target.Add(new CulturePortion(cultureType, percent));
        }
    }
}
