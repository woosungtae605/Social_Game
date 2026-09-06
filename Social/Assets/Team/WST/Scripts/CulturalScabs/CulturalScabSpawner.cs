using System.Collections;
using System.Collections.Generic;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    public class CulturalScabSpawner : MonoBehaviour
    {
        [SerializeField] private CulturalScabManager manager;
        [SerializeField] private int initialCount = 8;
        [SerializeField] private int maxAlive = 12;
        [SerializeField] private float minUniqueness = 50f;
        [SerializeField] private float maxUniqueness = 50f;
        [SerializeField] private float minInterval = 20f;
        [SerializeField] private float maxInterval = 35f;
        [SerializeField] [Range(0f, 1f)] private float mixChance = 0.35f;

        private static readonly CountryType[] AllCultures =
            (CountryType[])System.Enum.GetValues(typeof(CountryType));

        private void Start()
        {
            int spawnCount = Mathf.Max(0, initialCount);
            for (int i = 0; i < spawnCount; i++)
                TrySpawnOne();

            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (enabled)
            {
                float wait = Random.Range(minInterval, maxInterval);
                yield return new WaitForSeconds(wait);
                TrySpawnOne();
            }
        }

        private void TrySpawnOne()
        {
            if (manager == null)
                return;

            if (manager.Scabs.Count >= maxAlive)
                return;

            float uniqueness = Random.Range(minUniqueness, maxUniqueness);
            manager.Spawn(uniqueness, RollCultures());
        }

        private List<CulturePortion> RollCultures()
        {
            var portions = new List<CulturePortion>();
            if (AllCultures.Length == 0)
                return portions;

            CountryType first = AllCultures[Random.Range(0, AllCultures.Length)];
            bool mix = mixChance > 0f && AllCultures.Length > 1 && Random.value < mixChance;
            if (!mix)
            {
                portions.Add(new CulturePortion(first, 100f));
                return portions;
            }

            CountryType second = first;
            int guard = 0;
            while (second == first && guard < 8)
            {
                second = AllCultures[Random.Range(0, AllCultures.Length)];
                guard++;
            }

            float firstPercent = Random.Range(55f, 80f);
            portions.Add(new CulturePortion(first, firstPercent));
            portions.Add(new CulturePortion(second, 100f - firstPercent));
            return portions;
        }
    }
}
