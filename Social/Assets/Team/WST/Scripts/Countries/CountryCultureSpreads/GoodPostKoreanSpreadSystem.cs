using Team.KYR.Scripts;
using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.WST.Scripts.Countries.CountryCultureSpreads
{
    public class GoodPostKoreanSpreadSystem : MonoBehaviour
    {
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private BoardSo targetBoard;
        [SerializeField] private CountryManager countryManager;
        [SerializeField] private CountryType sourceCulture = CountryType.KOREA;
        [SerializeField] private float tickSeconds = 10f;

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
            int goodRecommendCount = CountGoodRecommendedPosts();
            if (goodRecommendCount <= 0)
                return;

            if (countryManager == null)
                return;

            if (!countryManager.TryGetCountry(sourceCulture, out AbstractCountry source))
                return;

            for (int i = 0; i < goodRecommendCount; i++)
                source.Spread();
        }

        private int CountGoodRecommendedPosts()
        {
            if (boardManager == null || targetBoard == null)
                return 0;

            int count = 0;
            var posts = boardManager.Posts;
            for (int i = 0; i < posts.Count; i++)
            {
                BoardPostData post = posts[i];
                if (post == null || post.Board != targetBoard || !post.IsGood || !post.IsConcept)
                    continue;

                if (post.OriginCountry != sourceCulture)
                    continue;

                count++;
            }

            return count;
        }
    }
}
