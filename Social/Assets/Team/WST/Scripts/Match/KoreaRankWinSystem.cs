using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries;
using Team.WST.Scripts.Countries.Informations;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.Match
{
    public class KoreaRankWinSystem : MonoBehaviour
    {
        [SerializeField] private CountryManager countryManager;
        [SerializeField] private CountryType playerCulture = CountryType.KOREA;

        private void Start()
        {
            Bus<CulturePowerChangedEvent>.OnEvent += HandleCulturePowerChanged;
            TryWin();
        }

        private void OnDestroy()
        {
            Bus<CulturePowerChangedEvent>.OnEvent -= HandleCulturePowerChanged;
        }

        private void HandleCulturePowerChanged(CulturePowerChangedEvent evt)
        {
            TryWin();
        }

        private void TryWin()
        {
            if (MatchOutcome.HasEnded)
                return;

            if (!CultureRanking.IsUniqueFirst(countryManager, playerCulture))
                return;

            MatchOutcome.TryEnd(MatchResult.Cleared);
        }
    }
}
