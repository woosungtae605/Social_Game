using Team.KYR.Scripts;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs
{
    public class HarmfulKoreanCulturePenalty : MonoBehaviour
    {
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private BoardSo targetBoard;
        [SerializeField] private CountryManager countryManager;
        [SerializeField] private CountryType koreanCulture = CountryType.KOREA;
        [SerializeField] private float graceSeconds = 20f;
        [SerializeField] private float tickSeconds = 3f;
        [SerializeField] private int nonConceptPenalty = 10;
        [SerializeField] private int conceptPenalty = 50;

        private float tickTimer;

        private void Start()
        {
            if (boardManager != null)
                boardManager.EnsureInitialized();
        }

        private void Update()
        {
            if (tickSeconds <= 0f)
                return;

            tickTimer += Time.deltaTime;
            if (tickTimer < tickSeconds)
                return;

            tickTimer -= tickSeconds;
            int drain = ComputeDrain();
            if (drain <= 0)
                return;

            ApplyDrain(drain);
        }

        private int ComputeDrain()
        {
            if (boardManager == null || targetBoard == null)
                return 0;

            int drain = 0;
            var posts = boardManager.Posts;
            for (int i = 0; i < posts.Count; i++)
            {
                BoardPostData post = posts[i];
                if (post == null || post.Board != targetBoard || !post.IsHarmful)
                    continue;

                if (Time.time - post.SpawnedAt < graceSeconds)
                    continue;

                drain += post.IsConcept ? conceptPenalty : nonConceptPenalty;
            }

            return drain;
        }

        private void ApplyDrain(int drain)
        {
            if (countryManager == null || countryManager.CountriesDict == null)
                return;

            foreach (AbstractCountry country in countryManager.CountriesDict.Values)
            {
                if (country == null || country.CountryType == koreanCulture)
                    continue;

                countryManager.AddCountryCulturePower(country.CountryType, koreanCulture, -drain);
            }
        }
    }
}
