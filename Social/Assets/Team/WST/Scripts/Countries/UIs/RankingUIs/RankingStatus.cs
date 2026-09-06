using System.Linq;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.RankingUIs
{
    public class RankingStatus : MonoBehaviour
    {
        [SerializeField] private CountryManager countryManager;

        [Header("pooling")]
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private RankingItemView rankingItemView;
        [SerializeField] private int initCount;

        private GenericObjectPool<RankingItemView> _pool;
        private bool _dirty;

        private void Awake()
        {
            _pool = new GenericObjectPool<RankingItemView>(rankingItemView, spawnTransform, initCount);
            Bus<CulturePowerChangedEvent>.OnEvent += HandleCulturePowerChanged;
        }

        private void OnDestroy()
        {
            Bus<CulturePowerChangedEvent>.OnEvent -= HandleCulturePowerChanged;
        }

        private void Start()
        {
            Refresh();
        }

        private void LateUpdate()
        {
            if (!_dirty)
                return;

            _dirty = false;
            Refresh();
        }

        private void HandleCulturePowerChanged(CulturePowerChangedEvent evt)
        {
            _dirty = true;
        }

        private void Refresh()
        {
            if (countryManager == null || countryManager.CountriesDict == null || _pool == null)
                return;

            _pool.Clear();

            var ranked = countryManager.CountriesDict.Values
                .OrderByDescending(country => CultureRanking.GetWorldPower(countryManager, country.CountryType));

            int rank = 1;
            foreach (AbstractCountry country in ranked)
            {
                RankingItemView item = _pool.Get();
                item.transform.SetSiblingIndex(rank - 1);
                item.SetRank(rank);
                item.SetCountryName(country.DisplayName);
                item.SetCulturePower(CultureRanking.GetWorldPower(countryManager, country.CountryType));
                rank++;
            }
        }
    }
}
