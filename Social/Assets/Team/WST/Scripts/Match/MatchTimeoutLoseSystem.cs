using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries;
using Team.WST.Scripts.Countries.Informations;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.Match
{
    public class MatchTimeoutLoseSystem : MonoBehaviour
    {
        [SerializeField] private CountryManager countryManager;
        [SerializeField] private CountryType playerCulture = CountryType.KOREA;

        private void OnEnable()
        {
            Bus<MatchTimeExpiredEvent>.OnEvent += HandleTimeExpired;
        }

        private void OnDisable()
        {
            Bus<MatchTimeExpiredEvent>.OnEvent -= HandleTimeExpired;
        }

        private void HandleTimeExpired(MatchTimeExpiredEvent evt)
        {
            if (MatchOutcome.HasEnded)
                return;

            if (CultureRanking.IsUniqueFirst(countryManager, playerCulture))
                return;

            MatchOutcome.TryEnd(MatchResult.Failed);
        }
    }
}
